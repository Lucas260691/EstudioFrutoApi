using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class Turma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechouMatricula",
                table: "AulasExperimentais",
                newName: "FechouContrato");

            migrationBuilder.RenameColumn(
                name: "DisponibilidadeAluno",
                table: "AulasExperimentais",
                newName: "NomeAluno");

            migrationBuilder.RenameColumn(
                name: "DiasSemanaPreferencia",
                table: "AulasExperimentais",
                newName: "Contato");

            migrationBuilder.AddColumn<string>(
                name: "MotivoNaoFechamento",
                table: "AulasExperimentais",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiasFixos",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrequenciaSemanal",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoPlano",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoNaoFechamento",
                table: "AulasExperimentais");

            migrationBuilder.DropColumn(
                name: "DiasFixos",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "FrequenciaSemanal",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "TipoPlano",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "NomeAluno",
                table: "AulasExperimentais",
                newName: "DisponibilidadeAluno");

            migrationBuilder.RenameColumn(
                name: "FechouContrato",
                table: "AulasExperimentais",
                newName: "FechouMatricula");

            migrationBuilder.RenameColumn(
                name: "Contato",
                table: "AulasExperimentais",
                newName: "DiasSemanaPreferencia");
        }
    }
}
