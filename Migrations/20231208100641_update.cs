using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_WiseWallet.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "InviteLink",
                table: "Teams",
                newName: "LinkInvite");

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "Teams",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "ApplicationUserTeam",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "text", nullable: false),
                    TeamsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTeam", x => new { x.MembersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTeam_TeamsId",
                table: "ApplicationUserTeam",
                column: "TeamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "ApplicationUserTeam");

            migrationBuilder.RenameColumn(
                name: "LinkInvite",
                table: "Teams",
                newName: "InviteLink");

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "Teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
