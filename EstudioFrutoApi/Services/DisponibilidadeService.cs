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

        public bool VerificarDisponibilidade(int instrutorID, string diaSemana, string horario)
        {
            var diaTrabalho = _context.DiasTrabalho
                .FirstOrDefault(d => d.InstrutorID == instrutorID && d.DiaSemana == diaSemana);

            if (diaTrabalho == null) return false;

            return diaTrabalho.HorariosDisponiveis.Split(',')
                .Any(h => h.Trim() == horario);
        }
    }

}
