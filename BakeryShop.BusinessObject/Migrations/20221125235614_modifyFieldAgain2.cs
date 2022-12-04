using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryShop.BusinessObject.Migrations
{
    public partial class modifyFieldAgain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Order",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                newName: "IX_Order_UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Order",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserID",
                table: "Order",
                newName: "IX_Order_CustomerID");
        }
    }
}
