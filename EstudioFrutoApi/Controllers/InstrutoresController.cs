using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginRequest = EstudioFrutoApi.Models.LoginRequest;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.Email == request.Email);

            if (instrutor == null)
            {
                return Unauthorized("Email não encontrado.");
            }

            // Validação de senha (usar uma senha padrão se ainda não implementada)
            if (request.Senha != "senhaPadrao")
            {
                return Unauthorized("Senha incorreta.");
            }

            // Retorna o instrutor e dados necessários
            return Ok(new
            {
                mensagem = "Login bem-sucedido",
                instrutorID = instrutor.InstrutorID,
                nome = instrutor.Nome,
                email = instrutor.Email
            });
        }


        // PUT: api/Instrutores/AlterarSenha/5
        [HttpPut("AlterarSenha/{id}")]
        public async Task<IActionResult> AlterarSenha(int id, [FromBody] AlterarSenhaRequest request)
        {
            var instrutor = await _context.Instrutores.FindAsync(id);

            if (instrutor == null)
            {
                return NotFound("Instrutor não encontrado.");
            }

            // Validação da senha antiga (se necessário)
            if (request.SenhaAntiga != "senhaPadrao")
            {
                return Unauthorized("Senha antiga incorreta.");
            }

            // Atualiza a senha para a nova
            instrutor.Senha = request.NovaSenha; // Se o campo Senha não existir, adicione-o ao modelo
            await _context.SaveChangesAsync();

            return Ok("Senha alterada com sucesso.");
        }


        // GET: api/Instrutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrutor>>> GetInstrutores()
        {
            // Retorna todos os instrutores
            return await _context.Instrutores
                .Include(i => i.DiasTrabalho)
                .ToListAsync();
        }

        // GET: api/Instrutores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrutor>> GetInstrutor(int id)
        {
            // Busca um instrutor específico pelo ID
            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(i => i.InstrutorID == id);

            if (instrutor == null)
            {
                return NotFound();
            }

            return instrutor;
        }

        // POST: api/Instrutores
        [HttpPost("cadastrar")]
        public async Task<ActionResult<Instrutor>> PostInstrutor(Instrutor instrutor)
        {
            // Adiciona um novo instrutor

            if (_context.Instrutores.Any(i => i.Email == instrutor.Email))
            {
                return BadRequest("Já existe um instrutor com este email.");
            }


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
