using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class updateAllModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfos_Accounts_OwnerAccountId",
                table: "AccountInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegralInfos_Integrals_OwnerIntegralId",
                table: "IntegralInfos");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Integrals");

            migrationBuilder.RenameColumn(
                name: "OwnerIntegralId",
                table: "IntegralInfos",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_IntegralInfos_OwnerIntegralId",
                table: "IntegralInfos",
                newName: "IX_IntegralInfos_OwnerId");

            migrationBuilder.RenameColumn(
                name: "OwnerAccountId",
                table: "AccountInfos",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountInfos_OwnerAccountId",
                table: "AccountInfos",
                newName: "IX_AccountInfos_OwnerId");

            migrationBuilder.AddColumn<decimal>(
                name: "AccountSum",
                table: "Menbers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IntegralSum",
                table: "Menbers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfos_Menbers_OwnerId",
                table: "AccountInfos",
                column: "OwnerId",
                principalTable: "Menbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegralInfos_Menbers_OwnerId",
                table: "IntegralInfos",
                column: "OwnerId",
                principalTable: "Menbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfos_Menbers_OwnerId",
                table: "AccountInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegralInfos_Menbers_OwnerId",
                table: "IntegralInfos");

            migrationBuilder.DropColumn(
                name: "AccountSum",
                table: "Menbers");

            migrationBuilder.DropColumn(
                name: "IntegralSum",
                table: "Menbers");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "IntegralInfos",
                newName: "OwnerIntegralId");

            migrationBuilder.RenameIndex(
                name: "IX_IntegralInfos_OwnerId",
                table: "IntegralInfos",
                newName: "IX_IntegralInfos_OwnerIntegralId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "AccountInfos",
                newName: "OwnerAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountInfos_OwnerId",
                table: "AccountInfos",
                newName: "IX_AccountInfos_OwnerAccountId");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountSum = table.Column<decimal>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Menbers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Menbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Integrals",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IntegralSum = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrals_Menbers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Menbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Integrals_OwnerId",
                table: "Integrals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfos_Accounts_OwnerAccountId",
                table: "AccountInfos",
                column: "OwnerAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegralInfos_Integrals_OwnerIntegralId",
                table: "IntegralInfos",
                column: "OwnerIntegralId",
                principalTable: "Integrals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
