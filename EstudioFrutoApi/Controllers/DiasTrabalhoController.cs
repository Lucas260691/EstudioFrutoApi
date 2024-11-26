using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiasTrabalhoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public DiasTrabalhoController(AgendaContext context)
        {
            _context = context;
        }

        // GET: api/DiasTrabalho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaTrabalho>>> GetDiasTrabalho()
        {
            // Retorna todos os dias de trabalho com os instrutores associados
            return await _context.DiasTrabalho
                .Include(d => d.Instrutor)
                .ToListAsync();
        }

        // GET: api/DiasTrabalho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaTrabalho>> GetDiaTrabalho(int id)
        {
            // Busca um dia de trabalho específico pelo ID
            var diaTrabalho = await _context.DiasTrabalho
                .Include(d => d.Instrutor)
                .FirstOrDefaultAsync(d => d.DiaTrabalhoID == id);

            if (diaTrabalho == null)
            {
                return NotFound();
            }

            return diaTrabalho;
        }

        // GET: api/DiasTrabalho/instrutor/1
        [HttpGet("instrutor/{instrutorID}")]
        public async Task<ActionResult<IEnumerable<DiaTrabalho>>> GetDiasTrabalhoPorInstrutor(int instrutorID)
        {
            // Retorna os dias de trabalho associados a um instrutor específico
            var diasTrabalho = await _context.DiasTrabalho
                .Where(d => d.InstrutorID == instrutorID)
                .ToListAsync();

            return Ok(diasTrabalho);
        }

        // POST: api/DiasTrabalho
        [HttpPost]
        public async Task<ActionResult<DiaTrabalho>> PostDiaTrabalho(DiaTrabalho diaTrabalho)
        {
            // Busca o instrutor pelo nome fornecido
            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.Nome.ToLower() == diaTrabalho.NomeInstrutor.ToLower());

            if (instrutor == null)
            {
                return BadRequest("Instrutor não encontrado. Verifique o nome informado.");
            }

            // Associa o ID do instrutor ao dia de trabalho
            diaTrabalho.InstrutorID = instrutor.InstrutorID;

            _context.DiasTrabalho.Add(diaTrabalho);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDiaTrabalho), new { id = diaTrabalho.DiaTrabalhoID }, diaTrabalho);
        }

        // PUT: api/DiasTrabalho/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaTrabalho(int id, DiaTrabalho diaTrabalho)
        {
            if (id != diaTrabalho.DiaTrabalhoID)
            {
                return BadRequest();
            }

            // Atualiza o dia de trabalho
            _context.Entry(diaTrabalho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaTrabalhoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/DiasTrabalho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaTrabalho(int id)
        {
            // Busca o dia de trabalho pelo ID
            var diaTrabalho = await _context.DiasTrabalho.FindAsync(id);

            if (diaTrabalho == null)
            {
                return NotFound();
            }

            // Remove o dia de trabalho
            _context.DiasTrabalho.Remove(diaTrabalho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper: Verifica se o dia de trabalho existe
        private bool DiaTrabalhoExists(int id)
        {
            return _context.DiasTrabalho.Any(e => e.DiaTrabalhoID == id);
        }
    }
}
