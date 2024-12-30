using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace EstudioFrutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly AgendaContext _context;

        public LoginController(AgendaContext context)
        {
            _context = context;
        }

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
    }
}
