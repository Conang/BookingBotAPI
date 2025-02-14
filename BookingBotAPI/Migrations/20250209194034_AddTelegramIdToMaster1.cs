using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingBotAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramIdToMaster1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramId",
                table: "Masters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "Masters");
        }
    }
}
