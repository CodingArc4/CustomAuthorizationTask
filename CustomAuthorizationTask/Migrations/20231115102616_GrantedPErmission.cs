using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorizationTask.Migrations
{
    /// <inheritdoc />
    public partial class GrantedPErmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 4, "Permission", "Permissions.Products.Delete", "c8627e22-e1b9-406d-9f54-39c801e2d1df" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
