using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_WiseWallet.Migrations
{
    /// <inheritdoc />
    public partial class update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams");

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "Teams",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
