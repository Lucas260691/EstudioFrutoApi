using EstudioFrutoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Services
{
    public class DisponibilidadeService
    {
        private readonly AgendaContext _context;

        public DisponibilidadeService(AgendaContext context)
        {
            _context = context;
        }

        // Verifica se um instrutor está disponível para uma aula experimental
        public bool VerificarDisponibilidadeInstrutor(int instrutorID, DateTime dataHora)
        {
            var diaSemana = dataHora.DayOfWeek.ToString();

            var instrutor = _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .FirstOrDefault(i => i.InstrutorID == instrutorID);

            if (instrutor == null) return false;

            return instrutor.DiasTrabalho.Any(d =>
                d.DiaSemana == diaSemana &&
                d.HorariosDisponiveis.Contains(dataHora.ToString("HH:mm")));
        }

        // Verifica a disponibilidade de horários com base no nível e preferências do aluno
        public async Task<string> VerificarDisponibilidadePorNivel(string nivel, string diasPreferencia, string horarioPreferencia)
        {
            var dias = diasPreferencia.Split(',').Select(d => d.Trim()).ToList();

            var disponibilidade = await _context.DiasTrabalho
                .Where(d => dias.Contains(d.DiaSemana) &&
                            d.HorariosDisponiveis.Contains(horarioPreferencia))
                .Include(d => d.Instrutor)
                .ToListAsync();

            var resultado = disponibilidade
                .Where(d => d.Instrutor.Nome.Contains(nivel, StringComparison.OrdinalIgnoreCase))
                .Select(d => $"{d.DiaSemana}, {horarioPreferencia} com {d.NomeInstrutor}")
                .FirstOrDefault();

            return resultado ?? "Nenhuma disponibilidade encontrada para o horário e nível informados.";
        }
    }


}
