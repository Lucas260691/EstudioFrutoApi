using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class AulaExperimental
    {
        [Key]
        public int AulaExperimentalID { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public int InstrutorID { get; set; }
        public Instrutor Instrutor { get; set; }

        public string NomeInstrutor { get; set; } // Nome do instrutor (persistido)

        [Required]
        public string NivelAluno { get; set; } // Básico, Intermediário, Avançado

        public string DisponibilidadeAluno { get; set; } // Ex.: "08:00-10:00"

        public string DiasSemanaPreferencia { get; set; } // Ex.: "Segunda-feira, Quarta-feira"

        public bool FechouMatricula { get; set; } // Indica se o aluno decidiu se matricular
    }
}
