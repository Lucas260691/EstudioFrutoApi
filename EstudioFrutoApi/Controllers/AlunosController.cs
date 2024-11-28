using EstudioFrutoApi.Data;
using EstudioFrutoApi.Models;
using EstudioFrutoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // POST: api/Alunos/cadastrar-existente
        [HttpPost("cadastrar-existente")]
        public async Task<ActionResult<Aluno>> CadastrarAlunoExistente(Aluno aluno)
        {
            // Valida duplicidade de email ou contato
            var alunoExistente = await _context.Alunos
                .FirstOrDefaultAsync(a => a.Email == aluno.Email || a.Contato == aluno.Contato);

            if (alunoExistente != null)
            {
                return BadRequest("Já existe um aluno com o mesmo email ou contato.");
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.AlunoID }, aluno);
        }

        // POST: api/Alunos/cadastrar-pos-aula/{aulaId}
        [HttpPost("cadastrar-pos-aula/{aulaId}")]
        public async Task<ActionResult<Aluno>> CadastrarPosAulaExperimental(int aulaId, [FromBody] DadosAluno dados)
        {
            // Busca a aula experimental
            var aulaExperimental = await _context.AulasExperimentais.FindAsync(aulaId);

            if (aulaExperimental == null || !aulaExperimental.FechouContrato)
            {
                return BadRequest("Aula experimental não encontrada ou contrato não fechado.");
            }

            // Valida disponibilidade na agenda
            var disponibilidadeService = new DisponibilidadeService(_context);
            var horarioDisponivel = await disponibilidadeService.ValidarDisponibilidade(
                dados.DiasFixos, dados.FrequenciaSemanal);

            if (!horarioDisponivel.Sucesso)
            {
                return BadRequest(horarioDisponivel.Mensagem);
            }

            // Cria o aluno com base nos dados da aula experimental
            var aluno = new Aluno
            {
                Nome = aulaExperimental.NomeAluno,
                Contato = aulaExperimental.Contato,
                Email = dados.Email,
                Nivel = aulaExperimental.NivelAluno,
                TipoPlano = dados.TipoPlano,
                FrequenciaSemanal = dados.FrequenciaSemanal,
                DiasFixos = string.Join(", ", horarioDisponivel.DiasConfirmados), // Corrigido
                CodAlune = dados.CodAlune
            };

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

    // Classe auxiliar para os dados complementares no cadastro pós aula experimental
    public class DadosAluno
    {
        public string Email { get; set; }
        public string TipoPlano { get; set; }
        public string FrequenciaSemanal { get; set; }
        public string DiasFixos { get; set; }
        public string CodAlune { get; set; }
    }
}
