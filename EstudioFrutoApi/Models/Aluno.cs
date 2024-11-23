﻿using System.ComponentModel.DataAnnotations;

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
    }
}