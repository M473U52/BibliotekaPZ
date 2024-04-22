using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ebookData",
                table: "Book",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 1m,
                column: "ebookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 2m,
                column: "ebookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 3m,
                column: "ebookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 4m,
                column: "ebookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "bookId",
                keyValue: 5m,
                column: "ebookData",
                value: null);

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 3, 31, 1, 12, 21, 271, DateTimeKind.Local).AddTicks(2905));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 4, 11, 2, 12, 21, 271, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 4, 20, 7, 12, 21, 271, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 4, 17, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(2961));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 4, 22, 19, 12, 21, 271, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 22, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3104), new DateTime(2024, 5, 22, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3106) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 3, 19, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3111), new DateTime(2024, 4, 22, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3113) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 7, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3118), new DateTime(2024, 5, 7, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3119) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 16, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3122), new DateTime(2024, 5, 16, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3124), new DateTime(2024, 5, 22, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3126) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 20, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3129), new DateTime(2024, 5, 20, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3131), new DateTime(2024, 5, 11, 22, 12, 21, 271, DateTimeKind.Local).AddTicks(3133) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 4, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ebookData",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 3, 18, 3, 48, 51, 580, DateTimeKind.Local).AddTicks(803));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 3, 29, 4, 48, 51, 580, DateTimeKind.Local).AddTicks(861));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 4, 7, 9, 48, 51, 580, DateTimeKind.Local).AddTicks(864));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 4, 5, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(866));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 4, 9, 21, 48, 51, 580, DateTimeKind.Local).AddTicks(868));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1023), new DateTime(2024, 5, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1026) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 3, 7, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1035), new DateTime(2024, 4, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1037) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 3, 26, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1041), new DateTime(2024, 4, 25, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1042) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 4, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1044), new DateTime(2024, 5, 4, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1045), new DateTime(2024, 5, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1047) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 8, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1050), new DateTime(2024, 5, 8, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1051), new DateTime(2024, 4, 29, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1053) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
