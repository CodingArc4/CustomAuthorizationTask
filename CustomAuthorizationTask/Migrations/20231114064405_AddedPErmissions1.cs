using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorizationTask.Migrations
{
    /// <inheritdoc />
    public partial class AddedPErmissions1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission", "Permissions.Products.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permission", "Permissions.Products.Create" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permissions.Products.View", "true" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "Permissions.Products.Create", "true" });
        }
    }
}
