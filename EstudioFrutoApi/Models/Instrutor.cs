﻿using System.ComponentModel.DataAnnotations;

namespace EstudioFrutoApi.Models
{
    public class Instrutor
    {
        [Key] // Define que InstrutorID é a chave primária
        public int InstrutorID { get; set; }

        [Required]
        public string Nome { get; set; }

        [EmailAddress] // Validação de email
        public string Email { get; set; }

        [Phone] // Validação de número de telefone
        public string Contato { get; set; }

        [Required]
        public string Senha { get; set; } // Novo campo

        public ICollection<DiaTrabalho> DiasTrabalho { get; set; } = new List<DiaTrabalho>();

        
    }
}
