using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class Turma
    {
        [Key]
        public int TurmaID { get; set; }

        [Required]
        public string Nome { get; set; } // Nome identificador da turma

        [Required]
        public int InstrutorID { get; set; } // Instrutor responsável pela turma

        public string NomeInstrutor { get; set; } // Nome do instrutor

        public string Nivel { get; set; } // Nível dos alunos na turma

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>(); // Lista de alunos da turma

        [Required]
        public string Sala { get; set; } // Sala atual (ex.: Cadillac, Reformer)

        public string DiasSemana { get; set; } // Dias da semana da turma

        public TimeSpan HoraInicio { get; set; } // Horário de início da aula

        public TimeSpan HoraFim { get; set; } // Horário de término da aula
    }
}

