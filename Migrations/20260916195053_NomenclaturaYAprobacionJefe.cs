using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class NomenclaturaYAprobacionJefe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AprobadoPorJefe",
                table: "Oficios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoValidado",
                table: "Oficios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprobadoPorJefe",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "DocumentoValidado",
                table: "Oficios");
        }
    }
}
