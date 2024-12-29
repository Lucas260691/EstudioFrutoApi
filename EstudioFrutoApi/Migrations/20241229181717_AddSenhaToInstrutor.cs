﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudioFrutoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSenhaToInstrutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Instrutores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Instrutores");
        }
    }
}