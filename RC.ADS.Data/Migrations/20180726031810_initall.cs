using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class initall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menbers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ManberName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ReferrerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menbers_Menbers_ReferrerId",
                        column: x => x.ReferrerId,
                        principalTable: "Menbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    orderStatu = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Menbers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Menbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OwnerAccountId = table.Column<string>(nullable: true),
                    Money = table.Column<decimal>(nullable: false),
                    AccountInfoChangeTpye = table.Column<int>(nullable: false),
                    Describe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountInfos_Accounts_OwnerAccountId",
                        column: x => x.OwnerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntegralInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OwnerIntegralId = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    IntegralInfoChangeType = table.Column<int>(nullable: false),
                    Describe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegralInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegralInfos_Integrals_OwnerIntegralId",
                        column: x => x.OwnerIntegralId,
                        principalTable: "Integrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountInfos_OwnerAccountId",
                table: "AccountInfos",
                column: "OwnerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegralInfos_OwnerIntegralId",
                table: "IntegralInfos",
                column: "OwnerIntegralId");

            migrationBuilder.CreateIndex(
                name: "IX_Integrals_OwnerId",
                table: "Integrals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Menbers_ReferrerId",
                table: "Menbers",
                column: "ReferrerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OwnerId",
                table: "Orders",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountInfos");

            migrationBuilder.DropTable(
                name: "IntegralInfos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Integrals");

            migrationBuilder.DropTable(
                name: "Menbers");
        }
    }
}
