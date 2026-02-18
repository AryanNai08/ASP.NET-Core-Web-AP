using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStidentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "Studentname" },
                values: new object[,]
                {
                    { 1, "India", new DateTime(2003, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "aryan@gmail.com", "Aryan" },
                    { 2, "India", new DateTime(2002, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "k@gmail.com", "kartik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
