using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class audiobook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "audiobookData",
                table: "Book",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 1m,
                column: "audiobookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 2m,
                column: "audiobookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 3m,
                column: "audiobookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 4m,
                column: "audiobookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 5m,
                column: "audiobookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 4, 21, 12, 29, 1, 844, DateTimeKind.Local).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 5, 2, 13, 29, 1, 844, DateTimeKind.Local).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 5, 11, 18, 29, 1, 844, DateTimeKind.Local).AddTicks(3108));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 5, 9, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 5, 14, 6, 29, 1, 844, DateTimeKind.Local).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 14, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3252), new DateTime(2024, 6, 14, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3256) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 11, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3262), new DateTime(2024, 5, 14, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3264) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 29, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3268), new DateTime(2024, 5, 30, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 8, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3272), new DateTime(2024, 6, 8, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3274), new DateTime(2024, 6, 14, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3276) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 12, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3279), new DateTime(2024, 6, 12, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3281), new DateTime(2024, 6, 3, 9, 29, 1, 844, DateTimeKind.Local).AddTicks(3282) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "audiobookData",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 4, 14, 16, 49, 23, 878, DateTimeKind.Local).AddTicks(9642));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 4, 25, 17, 49, 23, 878, DateTimeKind.Local).AddTicks(9708));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 5, 4, 22, 49, 23, 878, DateTimeKind.Local).AddTicks(9715));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 5, 2, 13, 49, 23, 878, DateTimeKind.Local).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 5, 7, 10, 49, 23, 878, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 7, 13, 49, 23, 878, DateTimeKind.Local).AddTicks(9998), new DateTime(2024, 6, 7, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 4, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(18), new DateTime(2024, 5, 7, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(21) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 22, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(27), new DateTime(2024, 5, 23, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(32) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 1, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(36), new DateTime(2024, 6, 1, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(39), new DateTime(2024, 6, 7, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(41) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 5, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(46), new DateTime(2024, 6, 5, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(75), new DateTime(2024, 5, 27, 13, 49, 23, 879, DateTimeKind.Local).AddTicks(78) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
