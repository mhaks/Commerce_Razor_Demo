using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceRazorDemo.Migrations
{
    public partial class remove_subcats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSubCategory_ProductSubCategoryId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductSubCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductSubCategoryId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductSubCategoryId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubCategory_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubCategoryId",
                table: "Product",
                column: "ProductSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategory_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubCategory_ProductSubCategoryId",
                table: "Product",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategory",
                principalColumn: "Id");
        }
    }
}
