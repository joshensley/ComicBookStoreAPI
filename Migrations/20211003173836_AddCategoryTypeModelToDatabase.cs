using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicBookStoreAPI.Migrations
{
    public partial class AddCategoryTypeModelToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryTypeID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryTypeID",
                table: "Products",
                column: "CategoryTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryType_CategoryTypeID",
                table: "Products",
                column: "CategoryTypeID",
                principalTable: "CategoryType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryType_CategoryTypeID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CategoryType");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryTypeID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryTypeID",
                table: "Products");
        }
    }
}
