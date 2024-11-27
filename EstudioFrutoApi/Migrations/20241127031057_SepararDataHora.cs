using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class SepararDataHora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AulasExperimentais_Instrutores_InstrutorID",
                table: "AulasExperimentais");

            migrationBuilder.DropIndex(
                name: "IX_AulasExperimentais_InstrutorID",
                table: "AulasExperimentais");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "AulasExperimentais",
                newName: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "NivelAluno",
                table: "AulasExperimentais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora",
                table: "AulasExperimentais",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Origem",
                table: "AulasExperimentais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora",
                table: "AulasExperimentais");

            migrationBuilder.DropColumn(
                name: "Origem",
                table: "AulasExperimentais");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "AulasExperimentais",
                newName: "DataHora");

            migrationBuilder.AlterColumn<string>(
                name: "NivelAluno",
                table: "AulasExperimentais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AulasExperimentais_InstrutorID",
                table: "AulasExperimentais",
                column: "InstrutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_AulasExperimentais_Instrutores_InstrutorID",
                table: "AulasExperimentais",
                column: "InstrutorID",
                principalTable: "Instrutores",
                principalColumn: "InstrutorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
