using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class addemployeeactivationentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeActivations",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeActivations", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "EmploymentDate", "FirstName", "IsActive", "LastName", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1L, new DateOnly(2024, 12, 19), "test.test@email.com", new DateOnly(2024, 12, 19), "Admin", true, "Dev", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeActivations");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
