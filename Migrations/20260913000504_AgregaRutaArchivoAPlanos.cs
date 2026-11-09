using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaRutaArchivoAPlanos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Procedimientos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Planos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Oficios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Procedimientos");

            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Oficios");
        }
    }
}
