using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 5, 6, 4, 5, 36, 502, DateTimeKind.Local).AddTicks(4690));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 5, 17, 5, 5, 36, 502, DateTimeKind.Local).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 5, 26, 10, 5, 36, 502, DateTimeKind.Local).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 5, 24, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4753));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 5, 28, 22, 5, 36, 502, DateTimeKind.Local).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 29, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4958), new DateTime(2024, 6, 29, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4961) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 26, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4969), new DateTime(2024, 5, 29, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4972) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 14, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4978), new DateTime(2024, 6, 14, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 23, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4984), new DateTime(2024, 6, 23, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4986), new DateTime(2024, 6, 29, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4988) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 27, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4993), new DateTime(2024, 6, 27, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4995), new DateTime(2024, 6, 18, 1, 5, 36, 502, DateTimeKind.Local).AddTicks(4997) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 5, 5, 18, 19, 28, 883, DateTimeKind.Local).AddTicks(2023));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 5, 16, 19, 19, 28, 883, DateTimeKind.Local).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 5, 26, 0, 19, 28, 883, DateTimeKind.Local).AddTicks(2070));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 5, 23, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2073));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 5, 28, 12, 19, 28, 883, DateTimeKind.Local).AddTicks(2076));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 28, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2246), new DateTime(2024, 6, 28, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 25, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2255), new DateTime(2024, 5, 28, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2257) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 5, 13, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2263), new DateTime(2024, 6, 13, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2265) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2269), new DateTime(2024, 6, 22, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2271), new DateTime(2024, 6, 28, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2273) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 5, 26, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2277), new DateTime(2024, 6, 26, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2279), new DateTime(2024, 6, 17, 15, 19, 28, 883, DateTimeKind.Local).AddTicks(2281) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
