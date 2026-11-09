using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaModuloProyectos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "Procedimientos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "Planos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "Oficios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clave = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedimientos_ProyectoId",
                table: "Procedimientos",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_ProyectoId",
                table: "Planos",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Oficios_ProyectoId",
                table: "Oficios",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oficios_Proyectos_ProyectoId",
                table: "Oficios",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Planos_Proyectos_ProyectoId",
                table: "Planos",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimientos_Proyectos_ProyectoId",
                table: "Procedimientos",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oficios_Proyectos_ProyectoId",
                table: "Oficios");

            migrationBuilder.DropForeignKey(
                name: "FK_Planos_Proyectos_ProyectoId",
                table: "Planos");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimientos_Proyectos_ProyectoId",
                table: "Procedimientos");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Procedimientos_ProyectoId",
                table: "Procedimientos");

            migrationBuilder.DropIndex(
                name: "IX_Planos_ProyectoId",
                table: "Planos");

            migrationBuilder.DropIndex(
                name: "IX_Oficios_ProyectoId",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "Procedimientos");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "Oficios");
        }
    }
}
