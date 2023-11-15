using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorizationTask.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catagories_CatagoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatagoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Catagories",
                newName: "CatagoryName");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Catagories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Catagories_ProductsId",
                table: "Catagories",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catagories_Products_ProductsId",
                table: "Catagories",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catagories_Products_ProductsId",
                table: "Catagories");

            migrationBuilder.DropIndex(
                name: "IX_Catagories_ProductsId",
                table: "Catagories");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Catagories");

            migrationBuilder.RenameColumn(
                name: "CatagoryName",
                table: "Catagories",
                newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatagoryId",
                table: "Products",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catagories_CatagoryId",
                table: "Products",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
