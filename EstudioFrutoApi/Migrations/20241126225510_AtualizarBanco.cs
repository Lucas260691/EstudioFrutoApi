using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AulasExperimentais",
                columns: table => new
                {
                    AulaExperimentalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstrutorID = table.Column<int>(type: "int", nullable: false),
                    NivelAluno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisponibilidadeAluno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiasSemanaPreferencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechouMatricula = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulasExperimentais", x => x.AulaExperimentalID);
                    table.ForeignKey(
                        name: "FK_AulasExperimentais_Instrutores_InstrutorID",
                        column: x => x.InstrutorID,
                        principalTable: "Instrutores",
                        principalColumn: "InstrutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiasTrabalho",
                columns: table => new
                {
                    DiaTrabalhoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorariosDisponiveis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrutorID = table.Column<int>(type: "int", nullable: false),
                    NomeInstrutor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasTrabalho", x => x.DiaTrabalhoID);
                    table.ForeignKey(
                        name: "FK_DiasTrabalho_Instrutores_InstrutorID",
                        column: x => x.InstrutorID,
                        principalTable: "Instrutores",
                        principalColumn: "InstrutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AulasExperimentais_InstrutorID",
                table: "AulasExperimentais",
                column: "InstrutorID");

            migrationBuilder.CreateIndex(
                name: "IX_DiasTrabalho_InstrutorID",
                table: "DiasTrabalho",
                column: "InstrutorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AulasExperimentais");

            migrationBuilder.DropTable(
                name: "DiasTrabalho");
        }
    }
}
