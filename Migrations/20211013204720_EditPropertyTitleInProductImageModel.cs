using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicBookStoreAPI.Migrations
{
    public partial class EditPropertyTitleInProductImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "ProductImages",
                newName: "ImageTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageTitle",
                table: "ProductImages",
                newName: "ImageURL");
        }
    }
}
