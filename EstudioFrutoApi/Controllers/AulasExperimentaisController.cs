using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using EstudioFrutoApi.Services;
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

        // GET: api/AulasExperimentais
        [HttpGet]
        public async Task<IActionResult> ListarAulasExperimentais()
        {
            var aulas = await _context.AulasExperimentais
                .Include(a => a.Instrutor)
                .ToListAsync();

            return Ok(aulas);
        }

        // GET: api/AulasExperimentais/verificar-disponibilidade/{id}
        [HttpGet("verificar-disponibilidade/{id}")]
        public async Task<IActionResult> VerificarDisponibilidade(int id)
        {
            var aulaExperimental = await _context.AulasExperimentais.FindAsync(id);

            if (aulaExperimental == null)
            {
                return NotFound("Aula experimental não encontrada.");
            }

            var disponibilidadeService = new DisponibilidadeService(_context);

            var disponibilidade = await disponibilidadeService.VerificarDisponibilidadePorNivel(
                aulaExperimental.NivelAluno,
                aulaExperimental.DiasSemanaPreferencia,
                aulaExperimental.DisponibilidadeAluno);

            return Ok(disponibilidade);
        }

        // POST: api/AulasExperimentais
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

            // Verifica se o instrutor está disponível no horário
            var disponibilidadeService = new DisponibilidadeService(_context);

            var instrutorDisponivel = disponibilidadeService.VerificarDisponibilidadeInstrutor(
                instrutor.InstrutorID, aulaExperimental.DataHora);

            if (!instrutorDisponivel)
            {
                return BadRequest("Instrutor indisponível para o horário selecionado.");
            }

            aulaExperimental.InstrutorID = instrutor.InstrutorID;
            aulaExperimental.NomeInstrutor = instrutor.Nome;

            _context.AulasExperimentais.Add(aulaExperimental);
            await _context.SaveChangesAsync();

            return Ok(aulaExperimental);
        }

        // PUT: api/AulasExperimentais/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAulaExperimental(int id, AulaExperimental aulaExperimental)
        {
            if (id != aulaExperimental.AulaExperimentalID)
            {
                return BadRequest("ID da aula experimental não corresponde ao registro.");
            }

            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.Nome.ToLower() == aulaExperimental.NomeInstrutor.ToLower());

            if (instrutor == null)
            {
                return BadRequest("Instrutor não encontrado. Verifique o nome informado.");
            }

            aulaExperimental.InstrutorID = instrutor.InstrutorID;

            _context.Entry(aulaExperimental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AulaExperimentalExists(id))
                {
                    return NotFound("Aula experimental não encontrada.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AulasExperimentais/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAulaExperimental(int id)
        {
            var aulaExperimental = await _context.AulasExperimentais.FindAsync(id);

            if (aulaExperimental == null)
            {
                return NotFound("Aula experimental não encontrada.");
            }

            _context.AulasExperimentais.Remove(aulaExperimental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verifica se a aula experimental existe no banco
        private bool AulaExperimentalExists(int id)
        {
            return _context.AulasExperimentais.Any(a => a.AulaExperimentalID == id);
        }
    }
}

