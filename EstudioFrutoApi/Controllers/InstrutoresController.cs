using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrutoresController : ControllerBase
    {
        private readonly AgendaContext _context;

        public InstrutoresController(AgendaContext context)
        {
            _context = context;
        }

        // GET: api/Instrutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrutor>>> GetInstrutores()
        {
            // Retorna todos os instrutores com os dias de trabalho associados
            return await _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .ToListAsync();
        }

        // GET: api/Instrutores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrutor>> GetInstrutor(int id)
        {
            // Busca um instrutor específico pelo ID, incluindo os dias de trabalho
            var instrutor = await _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .FirstOrDefaultAsync(i => i.InstrutorID == id);

            if (instrutor == null)
            {
                return NotFound();
            }

            return instrutor;
        }

        // POST: api/Instrutores
        [HttpPost]
        public async Task<ActionResult<Instrutor>> PostInstrutor(Instrutor instrutor)
        {
            // Adiciona um novo instrutor
            _context.Instrutores.Add(instrutor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInstrutor), new { id = instrutor.InstrutorID }, instrutor);
        }

        // PUT: api/Instrutores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrutor(int id, Instrutor instrutor)
        {
            if (id != instrutor.InstrutorID)
            {
                return BadRequest();
            }

            // Atualiza os dados do instrutor
            _context.Entry(instrutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrutorExists(id))
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

        // DELETE: api/Instrutores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrutor(int id)
        {
            // Busca o instrutor pelo ID
            var instrutor = await _context.Instrutores.FindAsync(id);

            if (instrutor == null)
            {
                return NotFound();
            }

            // Remove o instrutor e seus dias de trabalho
            var diasTrabalho = _context.DiasTrabalho.Where(d => d.InstrutorID == id);
            _context.DiasTrabalho.RemoveRange(diasTrabalho);
            _context.Instrutores.Remove(instrutor);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper: Verifica se o instrutor existe
        private bool InstrutorExists(int id)
        {
            return _context.Instrutores.Any(e => e.InstrutorID == id);
        }
    }  
}
