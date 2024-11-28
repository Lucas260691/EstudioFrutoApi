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

        // GET: api/AulasExperimentais
        [HttpGet]
        public async Task<IActionResult> ListarAulasExperimentais()
        {
            var aulas = await _context.AulasExperimentais.ToListAsync();
            return Ok(aulas);
        }

        // POST: api/AulasExperimentais
        [HttpPost]
        public async Task<IActionResult> AgendarAulaExperimental(AulaExperimental aulaExperimental)
        {
            // Busca o instrutor responsável
            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.Nome.ToLower() == aulaExperimental.NomeInstrutor.ToLower());

            if (instrutor == null)
            {
                return BadRequest("Instrutor não encontrado.");
            }

            aulaExperimental.InstrutorID = instrutor.InstrutorID;

            // Registra a aula experimental
            _context.AulasExperimentais.Add(aulaExperimental);
            await _context.SaveChangesAsync();

            return Ok(aulaExperimental);
        }

        // PATCH: api/AulasExperimentais/{id}/fechar-contrato
        [HttpPatch("{id}/fechar-contrato")]
        public async Task<IActionResult> FecharContrato(int id, [FromBody] bool fechouContrato)
        {
            var aulaExperimental = await _context.AulasExperimentais.FindAsync(id);

            if (aulaExperimental == null)
            {
                return NotFound("Aula experimental não encontrada.");
            }

            aulaExperimental.FechouContrato = fechouContrato;

            if (!fechouContrato)
            {
                // Se não fechou, espera receber o motivo e origem
                if (string.IsNullOrEmpty(aulaExperimental.MotivoNaoFechamento))
                {
                    return BadRequest("Motivo do não fechamento é obrigatório.");
                }
            }

            _context.Entry(aulaExperimental).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(aulaExperimental);
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
    }
}
