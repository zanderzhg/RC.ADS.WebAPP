using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ArticleName = table.Column<string>(nullable: true),
                    ArticleContent = table.Column<string>(nullable: true),
                    ArticleIco = table.Column<string>(nullable: true),
                    ArticleImage = table.Column<string>(nullable: true),
                    ArticleIndex = table.Column<int>(nullable: false),
                    ArticleTypeEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTypes_ArticleTypeEntityId",
                        column: x => x.ArticleTypeEntityId,
                        principalTable: "ArticleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeEntityId",
                table: "Articles",
                column: "ArticleTypeEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleTypes");
        }
    }
}
