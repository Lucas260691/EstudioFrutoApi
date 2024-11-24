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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrutor>>> GetInstrutores()
        {
            return await _context.Instrutores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instrutor>> GetInstrutor(int id)
        {
            var instrutor = await _context.Instrutores.FindAsync(id);

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
            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor == null)
            {
                return NotFound();
            }

            _context.Instrutores.Remove(instrutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstrutorExists(int id)
        {
            return _context.Instrutores.Any(e => e.InstrutorID == id);
        }
    }
}
