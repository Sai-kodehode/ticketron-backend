using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketron.Migrations
{
    /// <inheritdoc />
    public partial class bookingImageUrlAndUpdateIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bookings");
        }
    }
}
