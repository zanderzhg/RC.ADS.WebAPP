using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class editOrderAudits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAudit_Orders_AuditEntityId",
                table: "OrderAudit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderAudit",
                table: "OrderAudit");

            migrationBuilder.RenameTable(
                name: "OrderAudit",
                newName: "OrderAudits");

            migrationBuilder.RenameIndex(
                name: "IX_OrderAudit_AuditEntityId",
                table: "OrderAudits",
                newName: "IX_OrderAudits_AuditEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderAudits",
                table: "OrderAudits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAudits_Orders_AuditEntityId",
                table: "OrderAudits",
                column: "AuditEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAudits_Orders_AuditEntityId",
                table: "OrderAudits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderAudits",
                table: "OrderAudits");

            migrationBuilder.RenameTable(
                name: "OrderAudits",
                newName: "OrderAudit");

            migrationBuilder.RenameIndex(
                name: "IX_OrderAudits_AuditEntityId",
                table: "OrderAudit",
                newName: "IX_OrderAudit_AuditEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderAudit",
                table: "OrderAudit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAudit_Orders_AuditEntityId",
                table: "OrderAudit",
                column: "AuditEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
