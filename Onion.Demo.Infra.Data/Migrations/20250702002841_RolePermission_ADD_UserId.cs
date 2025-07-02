using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Demo.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolePermission_ADD_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RolePermission",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_UserId",
                table: "RolePermission",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_AspNetUsers_UserId",
                table: "RolePermission",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_AspNetUsers_UserId",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_UserId",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RolePermission");
        }
    }
}
