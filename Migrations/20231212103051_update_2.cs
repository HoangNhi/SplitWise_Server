using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_WiseWallet.Migrations
{
    /// <inheritdoc />
    public partial class update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
