using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.DAL.Migrations
{
    public partial class mig_changed_namesurname_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "Employees",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "Customers",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Employees",
                newName: "NameSurname");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Customers",
                newName: "NameSurname");
        }
    }
}
