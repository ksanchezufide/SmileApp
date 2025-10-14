using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileApp.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCorreoPacienteACita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorreoPaciente",
                table: "Citas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorreoPaciente",
                table: "Citas");
        }
    }
}
