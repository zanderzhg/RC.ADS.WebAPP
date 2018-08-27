using Microsoft.EntityFrameworkCore.Migrations;

namespace RC.ADS.Data.Migrations
{
    public partial class assd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfos_AccountInfoChangeTpyes_AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.DropTable(
                name: "AccountInfoChangeTpyes");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfos_AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.DropColumn(
                name: "AccountInfoChangeTpyeId",
                table: "AccountInfos");

            migrationBuilder.AddColumn<int>(
                name: "AccountInfoChangeTpye",
                table: "AccountInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountInfoChangeTpye",
                table: "AccountInfos");

            migrationBuilder.AddColumn<string>(
                name: "AccountInfoChangeTpyeId",
                table: "AccountInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountInfoChangeTpyes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlusOrMinus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfoChangeTpyes", x => x.Id);
                });

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
        }
    }
}
