using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class addtableTopItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopupItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TopupItemName = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    IsDalete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopupItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopupItems");
        }
    }
}
