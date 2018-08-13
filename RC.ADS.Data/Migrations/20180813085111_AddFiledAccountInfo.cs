using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class AddFiledAccountInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AfterScore",
                table: "IntegralInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeforeScore",
                table: "IntegralInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "IntegralInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "AfterMoney",
                table: "AccountInfos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BeforeMoney",
                table: "AccountInfos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "AccountInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterScore",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "BeforeScore",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "AfterMoney",
                table: "AccountInfos");

            migrationBuilder.DropColumn(
                name: "BeforeMoney",
                table: "AccountInfos");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "AccountInfos");
        }
    }
}
