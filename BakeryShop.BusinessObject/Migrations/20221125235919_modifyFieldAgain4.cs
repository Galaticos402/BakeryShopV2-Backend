using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryShop.BusinessObject.Migrations
{
    public partial class modifyFieldAgain4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddForeignKey(
                name: "FK_Order_User",
                table: "Order",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer",
                table: "Order",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
