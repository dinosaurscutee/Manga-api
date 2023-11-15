using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinoManga.Migrations
{
    /// <inheritdoc />
    public partial class tokenv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 11, 12, 16, 39, 41, 825, DateTimeKind.Local).AddTicks(5636));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 11, 12, 16, 17, 4, 437, DateTimeKind.Local).AddTicks(3972));
        }
    }
}
