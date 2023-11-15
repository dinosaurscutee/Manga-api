using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DinoManga.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Author 1" },
                    { 2, "Author 2" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[] { 1, "Password1", "User", "User1" });

            migrationBuilder.InsertData(
                table: "Mangas",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreateDate", "Description", "ThumbnailImage", "Title", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 1", "thumbnail1.jpg", "Manga 1", 0 },
                    { 2, 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 2", "thumbnail2.jpg", "Manga 2", 0 }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "DisplayOrder", "MangaId", "PdfUrl", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "url1", "Chapter 1" },
                    { 2, 2, 2, "url2", "Chapter 2" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "DateCreated", "MangaId", "UserId" },
                values: new object[] { 1, "Comment 1", new DateTime(2023, 11, 9, 22, 24, 58, 269, DateTimeKind.Local).AddTicks(8098), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mangas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mangas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
