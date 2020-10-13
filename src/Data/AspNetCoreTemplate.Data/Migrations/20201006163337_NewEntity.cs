namespace AspNetCoreTemplate.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class NewEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveWorkingArea",
                table: "WorkingAreas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkingAreas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Apartament",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Areas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingAreas_UserId",
                table: "WorkingAreas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingAreas_AspNetUsers_UserId",
                table: "WorkingAreas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingAreas_AspNetUsers_UserId",
                table: "WorkingAreas");

            migrationBuilder.DropIndex(
                name: "IX_WorkingAreas_UserId",
                table: "WorkingAreas");

            migrationBuilder.DropColumn(
                name: "ActiveWorkingArea",
                table: "WorkingAreas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkingAreas");

            migrationBuilder.DropColumn(
                name: "Apartament",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Areas");
        }
    }
}
