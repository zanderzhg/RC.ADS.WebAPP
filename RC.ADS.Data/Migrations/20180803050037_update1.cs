using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderStatusChanges");

            migrationBuilder.DropColumn(
                name: "orderStatu",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IntegralInfoChangeType",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "AccountInfoChangeTpye",
                table: "AccountInfos");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatusId",
                table: "OrderStatusChanges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatusId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountInfoChangeTpyeId",
                table: "AccountInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusChanges_OrderStatusId",
                table: "OrderStatusChanges",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegralInfos_IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                column: "IntegralInfoChangeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfos_AccountInfoChangeTpyeId",
                table: "AccountInfos",
                column: "AccountInfoChangeTpyeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfos_AccountInfoChangeTpyes_AccountInfoChangeTpyeId",
                table: "AccountInfos",
                column: "AccountInfoChangeTpyeId",
                principalTable: "AccountInfoChangeTpyes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegralInfos_IntegralInfoChangeType_IntegralInfoChangeTypeId",
                table: "IntegralInfos",
                column: "IntegralInfoChangeTypeId",
                principalTable: "IntegralInfoChangeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatusChanges_OrderStatus_OrderStatusId",
                table: "OrderStatusChanges",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfos_AccountInfoChangeTpyes_AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegralInfos_IntegralInfoChangeType_IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatusChanges_OrderStatus_OrderStatusId",
                table: "OrderStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatusChanges_OrderStatusId",
                table: "OrderStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_IntegralInfos_IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfos_AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "OrderStatusChanges");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IntegralInfoChangeTypeId",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderStatusChanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "orderStatu",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntegralInfoChangeType",
                table: "IntegralInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountInfoChangeTpye",
                table: "AccountInfos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
