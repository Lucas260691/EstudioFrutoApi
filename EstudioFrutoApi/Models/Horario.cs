using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class Horario
    {
        [Key] // Define que HorarioID é a chave primária
        public int HorarioID { get; set; }

        public DateTime DataHora { get; set; }

        // Chave estrangeira para Sala
        [ForeignKey("Sala")]
        public int SalaID { get; set; }
        public Sala Sala { get; set; }

        // Chave estrangeira para Instrutor
        [ForeignKey("Instrutor")]
        public int InstrutorID { get; set; }
        public Instrutor Instrutor { get; set; }

        // Chave estrangeira para Aluno
        [ForeignKey("Aluno")]
        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }
    }
}
