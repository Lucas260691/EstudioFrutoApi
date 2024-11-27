using System;
using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class AulaExperimental
    {
        [Key]
        public int AulaExperimentalID { get; set; }

        [Required]
        public DateTime Data { get; set; } // Apenas a data da aula experimental

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; } // Apenas a hora da aula experimental

        [Required]
        public int InstrutorID { get; set; }

        public string NomeInstrutor { get; set; }

        public string? NivelAluno { get; set; } // O nível será definido após a aula experimental

        public string DisponibilidadeAluno { get; set; }

        public string DiasSemanaPreferencia { get; set; }

        public bool FechouMatricula { get; set; }

        [Required]
        public string Origem { get; set; } // Indica como o aluno chegou
    }
}
