using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDuctos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposOficios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirigidoAtencion",
                table: "Oficios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Oficios",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSubida",
                table: "Oficios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OficioSolicitudNo",
                table: "Oficios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "Oficios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TamanoBytes",
                table: "Oficios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoOficio",
                table: "Oficios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirigidoAtencion",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "FechaSubida",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "OficioSolicitudNo",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "TamanoBytes",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "TipoOficio",
                table: "Oficios");
        }
    }
}
