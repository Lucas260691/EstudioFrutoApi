using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AgendaContext _context;

        public LoginController(AgendaContext context)
        {
            _context = context;
        }

        // ✅ POST para realizar login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Busca o instrutor pelo email
            var login = await _context.Logins
                .Include(l => l.Instrutor)
                .FirstOrDefaultAsync(l => l.Email == request.Email);

            if (login == null || login.Senha != request.Senha)
            {
                return Unauthorized("Email ou senha incorretos.");
            }

            return Ok(new
            {
                mensagem = "Login bem-sucedido",
                instrutorID = login.Instrutor.InstrutorID,
                nome = login.Instrutor.Nome,
                email = login.Email
            });
        }

        // ✅ GET para buscar informações do usuário pelo e-mail (sem senha)
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var login = await _context.Logins
                .Include(l => l.Instrutor)
                .FirstOrDefaultAsync(l => l.Email == email);

            if (login == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(new
            {
                instrutorID = login.Instrutor.InstrutorID,
                nome = login.Instrutor.Nome,
                email = login.Email
            });
        }
    }
}
