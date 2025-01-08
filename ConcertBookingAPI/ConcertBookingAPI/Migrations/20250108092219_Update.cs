using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 1, 9, 10, 22, 18, 886, DateTimeKind.Local).AddTicks(5880));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2025, 1, 10, 10, 22, 18, 902, DateTimeKind.Local).AddTicks(9412));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 1, 8, 10, 39, 59, 34, DateTimeKind.Local).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2025, 1, 9, 10, 39, 59, 36, DateTimeKind.Local).AddTicks(5005));
        }
    }
}
