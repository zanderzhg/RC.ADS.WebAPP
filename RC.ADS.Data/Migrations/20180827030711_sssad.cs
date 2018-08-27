using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class sssad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountSum",
                table: "Menbers",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "TradeNo",
                table: "AccountInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeNo",
                table: "AccountInfos");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountSum",
                table: "Menbers",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
