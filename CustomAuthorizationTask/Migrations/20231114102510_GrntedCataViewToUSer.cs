using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomAuthorizationTask.Migrations
{
    /// <inheritdoc />
    public partial class GrntedCataViewToUSer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 3, "Permission", "Permissions.Catagory.View", "8dd18f55-c196-4835-8fba-c8892d35a242" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
