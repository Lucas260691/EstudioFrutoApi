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

        public async Task<(bool Sucesso, string Mensagem, List<string> DiasConfirmados)> ValidarDisponibilidade(string diasFixos, string frequenciaSemanal)
        {
            // Pré-processamento em memória
            var dias = diasFixos.Split(',').Select(d => d.Trim()).ToList();
            var horarios = frequenciaSemanal.Split(',').Select(h => h.Trim()).ToList();

            var diasConfirmados = new List<string>();

            // Verifica conflitos com alunos cadastrados
            var alunos = await _context.Alunos.ToListAsync(); // Carrega alunos em memória
            var conflitoAlunos = alunos.Any(a =>
                dias.Any(dia => a.DiasFixos.Split(',').Select(d => d.Trim()).Contains(dia)) &&
                horarios.Any(horario => a.FrequenciaSemanal.Split(',').Select(h => h.Trim()).Contains(horario)));

            if (conflitoAlunos)
            {
                return (false, "Conflito de horário com alunos já cadastrados.", diasConfirmados);
            }

            // Valida instrutores no banco
            var diasTrabalho = await _context.DiasTrabalho
                .Where(d => dias.Contains(d.DiaSemana))
                .ToListAsync(); // Carrega dados do banco

            var instrutoresDisponiveis = diasTrabalho.Where(d =>
                d.HorariosDisponiveis.Split(',').Any(horario => horarios.Contains(horario)));

            if (!instrutoresDisponiveis.Any())
            {
                return (false, "Nenhum instrutor disponível para os dias e horários solicitados.", diasConfirmados);
            }

            // Adiciona dias confirmados
            diasConfirmados = instrutoresDisponiveis.Select(d => d.DiaSemana).Distinct().ToList();

            return (true, "Disponibilidade validada com sucesso.", diasConfirmados);
        }



        // Verifica disponibilidade com base no nível, dias preferidos e horários
        public async Task<string> VerificarDisponibilidade(string diasPreferencia, string horarioPreferencia, string nivel = null)
        {
            var dias = diasPreferencia.Split(',').Select(d => d.Trim()).ToList();

            // Busca dias e horários disponíveis no contexto de trabalho
            var disponibilidadeInstrutores = await _context.DiasTrabalho
                .Where(d => dias.Contains(d.DiaSemana) &&
                            d.HorariosDisponiveis.Contains(horarioPreferencia))
                .Include(d => d.Instrutor)
                .ToListAsync();

            // Busca alunos que já estão alocados na agenda
            var alunosOcupados = await _context.Alunos
                .Where(a => a.DiasFixos.Contains(diasPreferencia) &&
                            a.FrequenciaSemanal.Contains(horarioPreferencia))
                .ToListAsync();

            // Verifica se há conflito com os alunos já cadastrados
            var conflito = alunosOcupados.Any();
            if (conflito)
            {
                return "Conflito de horário com alunos já cadastrados.";
            }

            // Retorna disponibilidade com base no nível, se informado
            var disponibilidade = disponibilidadeInstrutores
                .Where(d => string.IsNullOrEmpty(nivel) ||
                            d.Instrutor.Nome.Contains(nivel, StringComparison.OrdinalIgnoreCase))
                .Select(d => $"{d.DiaSemana}, {horarioPreferencia} com {d.NomeInstrutor}")
                .FirstOrDefault();

            return disponibilidade ?? "Nenhuma disponibilidade encontrada para o horário e nível informados.";
        }

        // Verifica se um horário está livre para um aluno novo
        public async Task<bool> ValidarHorarioLivre(string diasFixos, string horario, int? alunoId = null)
        {
            var dias = diasFixos.Split(',').Select(d => d.Trim()).ToList();

            // Verifica se o horário solicitado está ocupado por outro aluno
            var diasNormalizados = dias.Select(d => d.Trim()).ToList();

            var conflito = await _context.Alunos
            .Where(a => dias.Any(dia => a.DiasFixos.Contains(dia)))
            .AnyAsync(); // Permite execução assíncrona no banco

            return !conflito; // Retorna true se não houver conflito
        }
    }
}




