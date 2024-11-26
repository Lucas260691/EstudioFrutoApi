using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCodAluneToAlunos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodAlune",
                table: "Alunos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodAlune",
                table: "Alunos");
        }
    }
}
