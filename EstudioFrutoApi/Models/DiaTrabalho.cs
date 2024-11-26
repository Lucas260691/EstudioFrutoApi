using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class DiaTrabalho
    {
        [Key]
        public int DiaTrabalhoID { get; set; }

        [Required]
        public string DiaSemana { get; set; } // Ex.: "Segunda-feira", "Terça-feira"

        [Required]
        public string HorariosDisponiveis { get; set; } // Ex.: "08:00-10:00, 14:00-16:00"

        [Required]
        public int InstrutorID { get; set; }
        public Instrutor Instrutor { get; set; }

        [Required]
        public string NomeInstrutor { get; set; } // Nome do instrutor, persistido no banco
    }
}

