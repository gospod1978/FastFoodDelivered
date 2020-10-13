using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class MoreEntityLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationObjects_AspNetUsers_ApplicationUserId",
                table: "LocationObjects");

            migrationBuilder.DropIndex(
                name: "IX_LocationObjects_ApplicationUserId",
                table: "LocationObjects");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "LocationObjects");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LocationObjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationObjects_UserId",
                table: "LocationObjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationObjects_AspNetUsers_UserId",
                table: "LocationObjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationObjects_AspNetUsers_UserId",
                table: "LocationObjects");

            migrationBuilder.DropIndex(
                name: "IX_LocationObjects_UserId",
                table: "LocationObjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LocationObjects");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "LocationObjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationObjects_ApplicationUserId",
                table: "LocationObjects",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationObjects_AspNetUsers_ApplicationUserId",
                table: "LocationObjects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
