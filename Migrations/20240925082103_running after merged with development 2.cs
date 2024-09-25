using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketron.Migrations
{
    /// <inheritdoc />
    public partial class runningaftermergedwithdevelopment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Participants_ParticipantId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipantId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Participants_ParticipantId",
                table: "Tickets",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Participants_ParticipantId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipantId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Participants_ParticipantId",
                table: "Tickets",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
