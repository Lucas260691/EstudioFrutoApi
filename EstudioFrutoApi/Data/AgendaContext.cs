﻿using EstudioFrutoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudioFrutoApi.Data
{
    public class AgendaContext: DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        // DbSets representam as tabelas no banco de dados
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<DiaTrabalho> DiasTrabalho { get; set; }
        public DbSet<AulaExperimental> AulasExperimentais { get; set; }
        public DbSet<Turma> Turmas {  get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>()
                .HasOne(l => l.Instrutor)
                .WithOne(i => i.Login)
                .HasForeignKey<Login>(l => l.InstrutorID);
        }
    }
}
