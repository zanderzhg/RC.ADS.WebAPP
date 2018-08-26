using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class sss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeChatOpenId",
                table: "Menbers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeChatOpenId",
                table: "Menbers");
        }
    }
}
