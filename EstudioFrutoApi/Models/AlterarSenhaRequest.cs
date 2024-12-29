using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class AlterarSenhaRequest
    {
        [Required]
        public string SenhaAntiga { get; set; }

        [Required]
        public string NovaSenha { get; set; }
    }
}
