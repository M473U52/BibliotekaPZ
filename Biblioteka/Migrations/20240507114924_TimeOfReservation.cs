using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class TimeOfReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "endingTime",
                table: "RoomReservation",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "startingTime",
                table: "RoomReservation",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

            migrationBuilder.UpdateData(
                table: "RoomReservation",
                keyColumn: "reservationId",
                keyValue: 1m,
                columns: new[] { "endingTime", "startingTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "RoomReservation",
                keyColumn: "reservationId",
                keyValue: 2m,
                columns: new[] { "endingTime", "startingTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "RoomReservation",
                keyColumn: "reservationId",
                keyValue: 3m,
                columns: new[] { "endingTime", "startingTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "RoomReservation",
                keyColumn: "reservationId",
                keyValue: 4m,
                columns: new[] { "endingTime", "startingTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "RoomReservation",
                keyColumn: "reservationId",
                keyValue: 5m,
                columns: new[] { "endingTime", "startingTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endingTime",
                table: "RoomReservation");

            migrationBuilder.DropColumn(
                name: "startingTime",
                table: "RoomReservation");

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 1m, 1m },
                column: "addedDate",
                value: new DateTime(2024, 4, 3, 1, 23, 31, 823, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 2m, 2m },
                column: "addedDate",
                value: new DateTime(2024, 4, 14, 2, 23, 31, 823, DateTimeKind.Local).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 3m, 3m },
                column: "addedDate",
                value: new DateTime(2024, 4, 23, 7, 23, 31, 823, DateTimeKind.Local).AddTicks(5978));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 4m },
                column: "addedDate",
                value: new DateTime(2024, 4, 20, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(5981));

            migrationBuilder.UpdateData(
                table: "Book_Opinions",
                keyColumns: new[] { "bookId", "readerId" },
                keyValues: new object[] { 4m, 5m },
                column: "addedDate",
                value: new DateTime(2024, 4, 25, 19, 23, 31, 823, DateTimeKind.Local).AddTicks(5984));

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 1m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 25, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6261), new DateTime(2024, 5, 25, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 2m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 3, 22, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6275), new DateTime(2024, 4, 25, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6278) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 3m,
                columns: new[] { "borrowDate", "plannedReturnDate" },
                values: new object[] { new DateTime(2024, 4, 10, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6285), new DateTime(2024, 5, 10, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6288) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 4m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 19, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6291), new DateTime(2024, 5, 19, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6293), new DateTime(2024, 5, 25, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6296) });

            migrationBuilder.UpdateData(
                table: "Borrowing",
                keyColumn: "borrowId",
                keyValue: 5m,
                columns: new[] { "borrowDate", "plannedReturnDate", "returnDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6300), new DateTime(2024, 5, 23, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6302), new DateTime(2024, 5, 14, 22, 23, 31, 823, DateTimeKind.Local).AddTicks(6304) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 1m,
                column: "eventDate",
                value: new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 2m,
                column: "eventDate",
                value: new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 3m,
                column: "eventDate",
                value: new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 4m,
                column: "eventDate",
                value: new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "eventId",
                keyValue: 5m,
                column: "eventDate",
                value: new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
