using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentApp.Migrations
{
    /// <inheritdoc />
    public partial class doctor_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
