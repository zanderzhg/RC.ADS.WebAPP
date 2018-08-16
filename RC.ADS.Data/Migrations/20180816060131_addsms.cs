using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class addsms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SendSMSLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Appid = table.Column<string>(nullable: true),
                    SMSAppTemplateId = table.Column<string>(nullable: true),
                    QuantityBeforeSend = table.Column<int>(nullable: false),
                    phoneNumbers = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    QuantityAfterSend = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendSMSLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSApp",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    appid = table.Column<string>(nullable: true),
                    appkey = table.Column<string>(nullable: true),
                    SMSAppName = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSApp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSAppTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Appid = table.Column<string>(nullable: true),
                    AppName = table.Column<string>(nullable: true),
                    Appkey = table.Column<string>(nullable: true),
                    TemplateId = table.Column<string>(nullable: true),
                    TemplateType = table.Column<int>(nullable: false),
                    SMSAppTemplateName = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSAppTemplates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendSMSLogs");

            migrationBuilder.DropTable(
                name: "SMSApp");

            migrationBuilder.DropTable(
                name: "SMSAppTemplates");
        }
    }
}
