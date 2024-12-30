using System.ComponentModel.DataAnnotations;


namespace EstudioFrutoApi.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        // Relacionamento com Instrutor
        public int InstrutorID { get; set; }
        public Instrutor Instrutor { get; set; }
    }
}
