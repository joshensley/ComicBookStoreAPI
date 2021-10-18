using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicBookStoreAPI.Migrations
{
    public partial class Add_ProductTypes_ProductSpecifications_ProductSpecificationValues_ModelsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypeID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationValues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ProductSpecificationID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationValues_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationValues_ProductSpecifications_ProductSpecificationID",
                        column: x => x.ProductSpecificationID,
                        principalTable: "ProductSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_ProductTypeID",
                table: "ProductSpecifications",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationValues_ProductID",
                table: "ProductSpecificationValues",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationValues_ProductSpecificationID",
                table: "ProductSpecificationValues",
                column: "ProductSpecificationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeID",
                table: "Products",
                column: "ProductTypeID",
                principalTable: "ProductTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductSpecificationValues");

            migrationBuilder.DropTable(
                name: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeID",
                table: "Products");
        }
    }
}
