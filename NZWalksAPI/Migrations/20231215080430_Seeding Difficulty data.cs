using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDifficultydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10f7ebf0-f4d7-45e8-ad9f-b9144890495b"), "Medium" },
                    { new Guid("1a96c68c-56e1-479c-ba18-71f92617fcd9"), "Hard" },
                    { new Guid("6ed1c932-3a83-4155-b992-2777a9551335"), "Easy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("10f7ebf0-f4d7-45e8-ad9f-b9144890495b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1a96c68c-56e1-479c-ba18-71f92617fcd9"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6ed1c932-3a83-4155-b992-2777a9551335"));
        }
    }
}
