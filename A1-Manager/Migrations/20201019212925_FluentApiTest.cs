using Microsoft.EntityFrameworkCore.Migrations;

namespace A1_Manager.Migrations
{
    public partial class FluentApiTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Money_CurrencyId",
                table: "Money");

            migrationBuilder.CreateIndex(
                name: "IX_Money_CurrencyId",
                table: "Money",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Money_CurrencyId",
                table: "Money");

            migrationBuilder.CreateIndex(
                name: "IX_Money_CurrencyId",
                table: "Money",
                column: "CurrencyId",
                unique: true);
        }
    }
}
