using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class AlunosController : ControllerBase
        {
            private readonly AgendaContext _context;

            public AlunosController(AgendaContext context)
            {
                _context = context;
            }

            // GET: api/Alunos
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
            {
                return await _context.Alunos.ToListAsync();
            }

            // GET: api/Alunos/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Aluno>> GetAluno(int id)
            {
                var aluno = await _context.Alunos.FindAsync(id);

                if (aluno == null)
                {
                    return NotFound();
                }

                return aluno;
            }

            // POST: api/Alunos
            [HttpPost]
            public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAluno), new { id = aluno.AlunoID }, aluno);
            }

            // PUT: api/Alunos/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutAluno(int id, Aluno aluno)
            {
                if (id != aluno.AlunoID)
                {
                    return BadRequest();
                }

                _context.Entry(aluno).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(id))
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

            // DELETE: api/Alunos/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAluno(int id)
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null)
                {
                    return NotFound();
                }

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool AlunoExists(int id)
            {
                return _context.Alunos.Any(e => e.AlunoID == id);
            }
        
    }
}
