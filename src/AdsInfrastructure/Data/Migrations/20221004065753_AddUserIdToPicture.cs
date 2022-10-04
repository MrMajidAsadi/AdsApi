using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMigrations
{
    public partial class AddUserIdToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementPictures_pictures_PictureId",
                table: "AdvertisementPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pictures",
                table: "pictures");

            migrationBuilder.RenameTable(
                name: "pictures",
                newName: "Pictures");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementPictures_Pictures_PictureId",
                table: "AdvertisementPictures",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementPictures_Pictures_PictureId",
                table: "AdvertisementPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pictures");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "pictures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pictures",
                table: "pictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementPictures_pictures_PictureId",
                table: "AdvertisementPictures",
                column: "PictureId",
                principalTable: "pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
