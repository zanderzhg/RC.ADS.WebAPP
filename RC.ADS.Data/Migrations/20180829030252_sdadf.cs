using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class sdadf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegralInfos_IntegralInfoChangeType_IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropTable(
                name: "ArticleTypes");

            migrationBuilder.DropTable(
                name: "IntegralInfoChangeType");

            migrationBuilder.DropIndex(
                name: "IX_IntegralInfos_IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "ArticleTypeId",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "IntegralInfoChangeType",
                table: "IntegralInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleType",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntegralInfoChangeType",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "ArticleType",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleTypeId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegralInfoChangeType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlusOrMinus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegralInfoChangeType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntegralInfos_IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                column: "IntegralInfoChangeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegralInfos_IntegralInfoChangeType_IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                column: "IntegralInfoChangeTypeId",
                principalTable: "IntegralInfoChangeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
