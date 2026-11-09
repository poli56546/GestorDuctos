using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaCategoriaAsBuilt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Procedimientos");

            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Oficios");

            migrationBuilder.AddColumn<bool>(
                name: "EsAsBuilt",
                table: "Planos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsAsBuilt",
                table: "Planos");

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Procedimientos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Oficios",
                type: "TEXT",
                nullable: true);
        }
    }
}
