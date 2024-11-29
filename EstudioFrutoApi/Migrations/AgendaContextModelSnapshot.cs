﻿// <auto-generated />
using System;
using EstudioFrutoApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    [DbContext(typeof(AgendaContext))]
    partial class AgendaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EstudioFrutoApi.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlunoID"));

                    b.Property<string>("CodAlune")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiasFixos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FrequenciaSemanal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPlano")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TurmaID")
                        .HasColumnType("int");

                    b.HasKey("AlunoID");

                    b.HasIndex("TurmaID");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.AulaExperimental", b =>
                {
                    b.Property<int>("AulaExperimentalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AulaExperimentalID"));

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FechouContrato")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<int>("InstrutorID")
                        .HasColumnType("int");

                    b.Property<string>("MotivoNaoFechamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NivelAluno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeAluno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeInstrutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AulaExperimentalID");

                    b.ToTable("AulasExperimentais");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.DiaTrabalho", b =>
                {
                    b.Property<int>("DiaTrabalhoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiaTrabalhoID"));

                    b.Property<string>("DiaSemana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HorariosDisponiveis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstrutorID")
                        .HasColumnType("int");

                    b.Property<string>("NomeInstrutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiaTrabalhoID");

                    b.HasIndex("InstrutorID");

                    b.ToTable("DiasTrabalho");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Horario", b =>
                {
                    b.Property<int>("HorarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HorarioID"));

                    b.Property<int>("AlunoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstrutorID")
                        .HasColumnType("int");

                    b.Property<int>("SalaID")
                        .HasColumnType("int");

                    b.HasKey("HorarioID");

                    b.HasIndex("AlunoID");

                    b.HasIndex("InstrutorID");

                    b.HasIndex("SalaID");

                    b.ToTable("Horarios");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Instrutor", b =>
                {
                    b.Property<int>("InstrutorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstrutorID"));

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisponibilidadeHoraria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstrutorID");

                    b.ToTable("Instrutores");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Sala", b =>
                {
                    b.Property<int>("SalaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaID"));

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalaID");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Turma", b =>
                {
                    b.Property<int>("TurmaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurmaID"));

                    b.Property<string>("DiasSemana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HoraFim")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<int>("InstrutorID")
                        .HasColumnType("int");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeInstrutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sala")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TurmaID");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Aluno", b =>
                {
                    b.HasOne("EstudioFrutoApi.Models.Turma", null)
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaID");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.DiaTrabalho", b =>
                {
                    b.HasOne("EstudioFrutoApi.Models.Instrutor", "Instrutor")
                        .WithMany("DiasTrabalho")
                        .HasForeignKey("InstrutorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrutor");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Horario", b =>
                {
                    b.HasOne("EstudioFrutoApi.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstudioFrutoApi.Models.Instrutor", "Instrutor")
                        .WithMany()
                        .HasForeignKey("InstrutorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstudioFrutoApi.Models.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Instrutor");

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Instrutor", b =>
                {
                    b.Navigation("DiasTrabalho");
                });

            modelBuilder.Entity("EstudioFrutoApi.Models.Turma", b =>
                {
                    b.Navigation("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
