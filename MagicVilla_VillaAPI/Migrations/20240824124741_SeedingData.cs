using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdateAt" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 8, 24, 18, 32, 41, 232, DateTimeKind.Local).AddTicks(5129), "Royal Villa Details", "https://image.com/v21/nice.png", "Royal Villa", 5, 5.0, 550, null },
                    { 2, "", new DateTime(2024, 8, 24, 18, 32, 41, 232, DateTimeKind.Local).AddTicks(5145), "New Villa Details", "https://image.com/v21/nice.png", "New Villa", 45, 500.0, 550, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
