using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroDatingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AboutMe",
                table: "Users",
                newName: "Introduction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "Users",
                newName: "AboutMe");
        }
    }
}
