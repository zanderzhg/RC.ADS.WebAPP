using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class AddOrderAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderAudit",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuditEntityId = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    Creatime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAudit_Orders_AuditEntityId",
                        column: x => x.AuditEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAudit_AuditEntityId",
                table: "OrderAudit",
                column: "AuditEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAudit");
        }
    }
}
