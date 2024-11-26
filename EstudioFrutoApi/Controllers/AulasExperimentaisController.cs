using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulasExperimentaisController : ControllerBase
    {
        private readonly AgendaContext _context;

        public AulasExperimentaisController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AgendarAulaExperimental(AulaExperimental aulaExperimental)
        {
            // Busca o instrutor pelo nome fornecido
            var instrutor = await _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .FirstOrDefaultAsync(i => i.Nome.ToLower() == aulaExperimental.NomeInstrutor.ToLower());

            if (instrutor == null)
            {
                return BadRequest("Instrutor não encontrado. Verifique o nome informado.");
            }

            // Pré-processa os dias de preferência do aluno
            var diasPreferencia = aulaExperimental.DiasSemanaPreferencia
                .Split(',')
                .Select(d => d.Trim())
                .ToList();

            // Verifica a disponibilidade do instrutor com base nos dias e horários preferidos
            var instrutorDisponivel = await _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .Where(i => i.InstrutorID == aulaExperimental.InstrutorID)
                .AnyAsync(i => i.DiasTrabalho.Any(d =>
                    diasPreferencia.Contains(d.DiaSemana) &&
                    d.HorariosDisponiveis.Contains(aulaExperimental.DataHora.ToString("HH:mm"))));

            if (!instrutorDisponivel)
            {
                return BadRequest("Instrutor indisponível para o horário e dias preferidos.");
            }

            // Registra a aula experimental
            _context.AulasExperimentais.Add(aulaExperimental);
            await _context.SaveChangesAsync();

            return Ok(aulaExperimental);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAulasExperimentais()
        {
            var aulas = await _context.AulasExperimentais
                .Include(a => a.Instrutor)
                .ToListAsync();

            return Ok(aulas);
        }
    }
}
