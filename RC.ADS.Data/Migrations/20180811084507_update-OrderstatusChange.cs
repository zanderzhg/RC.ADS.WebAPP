using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class updateOrderstatusChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creatime",
                table: "OrderStatusChanges",
                newName: "CreateTime");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderStatusChanges",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderStatusChanges",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderStatusChanges");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderStatusChanges");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "OrderStatusChanges",
                newName: "Creatime");
        }
    }
}
