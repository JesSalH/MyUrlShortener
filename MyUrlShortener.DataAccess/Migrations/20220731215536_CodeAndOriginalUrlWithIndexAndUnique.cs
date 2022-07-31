using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyUrlShortener.DataAccess.Migrations
{
    public partial class CodeAndOriginalUrlWithIndexAndUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "ShortenedUrls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_Code",
                table: "ShortenedUrls",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_OriginalUrl",
                table: "ShortenedUrls",
                column: "OriginalUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShortenedUrls_Code",
                table: "ShortenedUrls");

            migrationBuilder.DropIndex(
                name: "IX_ShortenedUrls_OriginalUrl",
                table: "ShortenedUrls");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "ShortenedUrls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
