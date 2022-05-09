using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankStartWeb.Data.Migrations
{
    public partial class indexing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Givenname_Surname_City",
                table: "Customers",
                columns: new[] { "Givenname", "Surname", "City" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Givenname_Surname_City",
                table: "Customers");
        }
    }
}
