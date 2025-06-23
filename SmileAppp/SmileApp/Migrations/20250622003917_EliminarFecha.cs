using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileApp.Migrations
{
    /// <inheritdoc />
    public partial class EliminarFecha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSubida",
                table: "ArchivosPacientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSubida",
                table: "ArchivosPacientes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }
    }
}
