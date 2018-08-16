using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class addfiledlastlogincode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginCode",
                table: "Menbers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginCode",
                table: "Menbers");
        }
    }
}
