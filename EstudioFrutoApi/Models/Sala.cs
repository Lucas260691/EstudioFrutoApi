using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class Sala
    {
        [Key] // Define que SalaID é a chave primária
        public int SalaID { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Capacidade { get; set; } = 4; // Capacidade padrão
    }
}
