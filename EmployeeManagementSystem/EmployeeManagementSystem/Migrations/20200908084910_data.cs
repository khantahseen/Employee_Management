using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementSystem.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "DepartmentID", "Name" },
                values: new object[] { 1, ".NET" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "Address", "ContactNumber", "DepartmentID", "Email", "Name", "Qualification", "Surname" },
                values: new object[] { 1, "Vadodara", 343434343L, 1, "tahseenk211@gmail.com", "Tahseen", "BE", "Khan" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "Address", "ContactNumber", "DepartmentID", "Email", "Name", "Qualification", "Surname" },
                values: new object[] { 2, "Vadodara", 56565656L, 1, "tahira@gmail.com", "Tahira", "BE", "Khan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "departments",
                keyColumn: "DepartmentID",
                keyValue: 1);
        }
    }
}
