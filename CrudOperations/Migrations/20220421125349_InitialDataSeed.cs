using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudOperations.Migrations
{
    public partial class InitialDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "Address", "Country", "Email", "Name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "354 St.Avenue", "kenya", "johndoe@info.co", "John Doe" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "AccountType", "CreatedAt", "Name", "OwnerId" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Savings", new DateTime(2022, 4, 21, 15, 53, 49, 318, DateTimeKind.Local).AddTicks(1525), "Account One", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));
        }
    }
}
