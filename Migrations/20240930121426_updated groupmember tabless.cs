using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketron.Migrations
{
    /// <inheritdoc />
    public partial class updatedgroupmembertabless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnregUserId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UnregUserId",
                table: "Groups",
                column: "UnregUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_UnregUsers_UnregUserId",
                table: "Groups",
                column: "UnregUserId",
                principalTable: "UnregUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_UnregUsers_UnregUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UnregUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UnregUserId",
                table: "Groups");
        }
    }
}
