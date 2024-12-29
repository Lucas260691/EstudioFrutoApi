using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDisponibilidadeHorariaFromInstrutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisponibilidadeHoraria",
                table: "Instrutores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisponibilidadeHoraria",
                table: "Instrutores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
