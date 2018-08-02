using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class updateArticleid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeEntityId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ArticleTypeEntityId",
                table: "Articles",
                newName: "ArticleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ArticleTypeEntityId",
                table: "Articles",
                newName: "IX_Articles_ArticleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ArticleTypeId",
                table: "Articles",
                newName: "ArticleTypeEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles",
                newName: "IX_Articles_ArticleTypeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeEntityId",
                table: "Articles",
                column: "ArticleTypeEntityId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
