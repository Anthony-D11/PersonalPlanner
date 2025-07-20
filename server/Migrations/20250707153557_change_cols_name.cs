using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class change_cols_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "activities",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "activities",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "activities",
                newName: "completed");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "activities",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TimeAssigned",
                table: "activities",
                newName: "time_assigned");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "details",
                table: "activities",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "activities",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "completed",
                table: "activities",
                newName: "Completed");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "activities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "time_assigned",
                table: "activities",
                newName: "TimeAssigned");
        }
    }
}
