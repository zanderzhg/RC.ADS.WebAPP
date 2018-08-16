using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class addfiledlastlo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginCode",
                table: "Menbers",
                newName: "LastLoginGuidCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginGuidCode",
                table: "Menbers",
                newName: "LastLoginCode");
        }
    }
}
