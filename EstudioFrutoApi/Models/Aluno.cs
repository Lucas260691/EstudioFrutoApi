using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class Aluno
    {
        [Key] // Define que AlunoID é a chave primária
        public int AlunoID { get; set; }

        [Required] // Campo obrigatório
        public string Nome { get; set; }

        [Required] // Campo obrigatório
        public string Nivel { get; set; } // Básico, Intermediário ou Avançado

        [EmailAddress] // Validação de email
        public string Email { get; set; }

        [Phone] // Validação de número de telefone
        public string Contato { get; set; }

        [Required]
        [MaxLength(50)] // Opcional, define o tamanho máximo do apelido
        public string CodAlune { get; set; }

        [Required]
        public string TipoPlano { get; set; } // Plano: Mês teste, semestral, trimestral

        [Required]
        public string FrequenciaSemanal { get; set; } // Frequência: 1x, 2x, 3x, 4x

        public string DiasFixos { get; set; } // Dias fixos do aluno

    }
}
