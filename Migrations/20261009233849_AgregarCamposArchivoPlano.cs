using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposArchivoPlano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSubida",
                table: "Planos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TamanoBytes",
                table: "Planos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSubida",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "TamanoBytes",
                table: "Planos");
        }
    }
}
