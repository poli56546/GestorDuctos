using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaContratoYRespuestas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContratoCliente",
                table: "Proyectos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OficioRespuesta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OficioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: false),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficioRespuesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OficioRespuesta_Oficios_OficioId",
                        column: x => x.OficioId,
                        principalTable: "Oficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OficioRespuesta_OficioId",
                table: "OficioRespuesta",
                column: "OficioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OficioRespuesta");

            migrationBuilder.DropColumn(
                name: "ContratoCliente",
                table: "Proyectos");
        }
    }
}
