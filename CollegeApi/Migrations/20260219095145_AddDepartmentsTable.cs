using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "students");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "CE", "Computer department" },
                    { 2, "IT", "IT department" }
                });

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "DOB", "DepartmentId" },
                values: new object[] { null, new DateTime(2004, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "DOB", "DepartmentId" },
                values: new object[] { null, new DateTime(2004, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_students_DepartmentId",
                table: "students",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department",
                table: "students",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department",
                table: "students");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_DepartmentId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "students");

            migrationBuilder.RenameTable(
                name: "students",
                newName: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "DOB" },
                values: new object[] { "India", new DateTime(2003, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "DOB" },
                values: new object[] { "India", new DateTime(2002, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
