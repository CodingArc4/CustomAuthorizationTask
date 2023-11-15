using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorizationTask.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Catagories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Catagories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
