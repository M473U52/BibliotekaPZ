using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    profilePicData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    authorId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    nickname = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    imageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.authorId);
                });

            migrationBuilder.CreateTable(
                name: "BookType",
                columns: table => new
                {
                    typeId = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookType", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    FAQId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.FAQId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    genreId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    positionId = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    salary = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.positionId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    publisherId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.publisherId);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    readerId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DaysBeforeReturn = table.Column<int>(type: "int", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isAuthor = table.Column<bool>(type: "bit", nullable: false),
                    imageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.readerId);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    roomTypeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.roomTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tagId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRol~",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRole~",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUser~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    eventId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    eventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    authorId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.eventId);
                    table.ForeignKey(
                        name: "FK_Event_Author_authorId",
                        column: x => x.authorId,
                        principalTable: "Author",
                        principalColumn: "authorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    dateOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    positionId = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Position_position~",
                        column: x => x.positionId,
                        principalTable: "Position",
                        principalColumn: "positionId");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    bookId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ISBN = table.Column<decimal>(type: "NUMERIC(13,0)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    availableCopys = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    ratingAVG = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: true),
                    ratingId = table.Column<int>(type: "int", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    publisherId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    genreId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    typeId = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false),
                    floor = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false),
                    alley = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    rowNumber = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    imageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_Book_BookType_typeId",
                        column: x => x.typeId,
                        principalTable: "BookType",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genre_genreId",
                        column: x => x.genreId,
                        principalTable: "Genre",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Publisher_publisherId",
                        column: x => x.publisherId,
                        principalTable: "Publisher",
                        principalColumn: "publisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    roomNumber = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatCount = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    roomTypeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.roomNumber);
                    table.ForeignKey(
                        name: "FK_Room_RoomType_roomTypeId",
                        column: x => x.roomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "roomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeConfirmingPaymentsBook",
                columns: table => new
                {
                    employeeConfirmingPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeConfirmingPayments~", x => x.employeeConfirmingPaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeeConfirmingPayments~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeConfirmingReturnsBook",
                columns: table => new
                {
                    employeeConfirmingReturnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeConfirmingReturnsB~", x => x.employeeConfirmingReturnId);
                    table.ForeignKey(
                        name: "FK_EmployeeConfirmingReturnsB~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeData",
                columns: table => new
                {
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    pesel = table.Column<decimal>(type: "NUMERIC(11,0)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    houseNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: true),
                    town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    zipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeData", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeData_Employee_empl~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Author",
                columns: table => new
                {
                    bookId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    authorId = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Author", x => new { x.bookId, x.authorId });
                    table.ForeignKey(
                        name: "FK_Book_Author_Author_authorId",
                        column: x => x.authorId,
                        principalTable: "Author",
                        principalColumn: "authorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Author_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Opinions",
                columns: table => new
                {
                    bookId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    readerId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    rating = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    addedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    opinion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Opinions", x => new { x.bookId, x.readerId });
                    table.ForeignKey(
                        name: "FK_Book_Opinions_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Opinions_Reader_reade~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Tag",
                columns: table => new
                {
                    bookId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    tagId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Tag", x => new { x.bookId, x.tagId });
                    table.ForeignKey(
                        name: "FK_Book_Tag_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Tag_Tag_tagId",
                        column: x => x.tagId,
                        principalTable: "Tag",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomReservation",
                columns: table => new
                {
                    reservationId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    readerId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    roomId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    begginingOfReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endOfReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: true),
                    Confirmation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservation", x => x.reservationId);
                    table.ForeignKey(
                        name: "FK_RoomReservation_Employee_e~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_RoomReservation_Reader_rea~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomReservation_Room_roomId",
                        column: x => x.roomId,
                        principalTable: "Room",
                        principalColumn: "roomNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrowing",
                columns: table => new
                {
                    borrowId = table.Column<decimal>(type: "NUMERIC(5,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    plannedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Confirmation = table.Column<bool>(type: "bit", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bookId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    employeeId = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    employeeConfirmingReturnId = table.Column<int>(type: "int", nullable: true),
                    employeeConfirmingPaymentId = table.Column<int>(type: "int", nullable: true),
                    bookLost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowing", x => x.borrowId);
                    table.ForeignKey(
                        name: "FK_Borrowing_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrowing_EmployeeConfirmi~",
                        column: x => x.employeeConfirmingPaymentId,
                        principalTable: "EmployeeConfirmingPaymentsBook",
                        principalColumn: "employeeConfirmingPaymentId");
                    table.ForeignKey(
                        name: "FK_Borrowing_EmployeeConfirm~1",
                        column: x => x.employeeConfirmingReturnId,
                        principalTable: "EmployeeConfirmingReturnsBook",
                        principalColumn: "employeeConfirmingReturnId");
                    table.ForeignKey(
                        name: "FK_Borrowing_Employee_employe~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reader_Borrowings",
                columns: table => new
                {
                    readerId = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    borrowId = table.Column<decimal>(type: "NUMERIC(5,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader_Borrowings", x => new { x.readerId, x.borrowId });
                    table.ForeignKey(
                        name: "FK_Reader_Borrowings_Borrowin~",
                        column: x => x.borrowId,
                        principalTable: "Borrowing",
                        principalColumn: "borrowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reader_Borrowings_Reader_r~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "authorId", "birthDate", "country", "description", "email", "imageData", "name", "nickname", "surname" },
                values: new object[,]
                {
                    { 1m, new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wielka Brytania", "niesamowity pisarz", "jrtolkien@tolkien.com", null, "J.R.R", "JRR", "Tolkien" },
                    { 2m, new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wielka Brytania", "niesamowita pisarka", "jkrowling@rowling.com", null, "J.K", "Jo Rowling", "Rowling" },
                    { 3m, new DateTime(1948, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polska", "wiedźmin jakich mało", "papawiedzmin@gmail.com", null, "Andrzej", "Sapek", "Sapkowski" },
                    { 4m, new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stany Zjednoczone", "super pisarz", "stking@stking.com", null, "Stephen", "St King", "King" },
                    { 5m, new DateTime(1798, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polska", "trochę pali", "amickiewicz@mickifan.com", null, "Adam", "A Micki", "Mickiewicz" },
                    { 6m, new DateTime(1846, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polska", "Henryk Adam Aleksander Pius Sienkiewicz", "czlowiekpuszczy@gmail.com", null, "Henryk", "Litwos", "Sienkiwicz" },
                    { 7m, new DateTime(1911, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polska", "czesław aż miło", "czesio123@yahoo.com", null, "Czesław", "Czesio", "Miłosz" },
                    { 8m, new DateTime(1898, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polska", "nic nie powie ale napisze", "aniemowilem@gmail.com", null, "Adam", "...", "Niemowa" },
                    { 9m, new DateTime(1942, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wielka Brytania", "giga mózg ale książki też pisał", "blackhole@gmail.com", null, "Stephen", "Stephen", "Hawking" },
                    { 10m, new DateTime(1948, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stany Zjednoczone", "gra o tron ale nie gra w gry", "grrmartin@grrmartin.com", null, "George R.R", "GRRR", "Martin" }
                });

            migrationBuilder.InsertData(
                table: "BookType",
                columns: new[] { "typeId", "name" },
                values: new object[,]
                {
                    { 1m, "typ1" },
                    { 2m, "typ2" },
                    { 3m, "typ3" },
                    { 4m, "typ4" },
                    { 5m, "typ5" }
                });

            migrationBuilder.InsertData(
                table: "FAQ",
                columns: new[] { "FAQId", "answer", "question" },
                values: new object[,]
                {
                    { 1m, "aby wypożyczyć książkę należy najpierw się zalogować a następnie wejść w księgozbior i nacisnąć wypożycz jeśli jest wystarczająca ilość egzemplarzy. książka zostanie dodana do koszyka i będzie można wybrać datę jej wypożyczenia.", "jak wypożyczyć książkę?" },
                    { 2m, "jak nie wiesz to sobie poklikaj i posprawdzaj. nie mam całej nocy na pisanie tej sekcji ;)", "jak cośtam?" },
                    { 3m, "trzeba szybko i kilkukrotnie nacisnąć kombinację klawiszy ALT + F4", "jak się wylogować z konta?" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "genreId", "name" },
                values: new object[,]
                {
                    { 1m, "Przygodowe" },
                    { 2m, "Akcji" },
                    { 3m, "Dramat" },
                    { 4m, "Fanstasy" },
                    { 5m, "Science fiction" },
                    { 6m, "Horror" },
                    { 7m, "Komedia" },
                    { 8m, "Historyczne" },
                    { 9m, "Dokumentalne" },
                    { 10m, "Dla dzieci" }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "positionId", "name", "salary" },
                values: new object[,]
                {
                    { 1m, "barista", 2100m },
                    { 2m, "barista pomocniczy", 2200m },
                    { 3m, "recepcjonista", 2600m },
                    { 4m, "st. recepcjonista", 2851m },
                    { 5m, "szef pracowników", 6666m }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "publisherId", "description", "name" },
                values: new object[,]
                {
                    { 1m, "tak", "wydawnictwo igła" },
                    { 2m, "opis2", "wydawnictwo2" },
                    { 3m, "opis33331", "wydawnictwo3" },
                    { 4m, "wszystko co związane z kosmosem", "wydawnictwo kosmos" },
                    { 5m, "wydawnictwo skupione na książkach religinych", "wydawnictwo katolickie" }
                });

            migrationBuilder.InsertData(
                table: "Reader",
                columns: new[] { "readerId", "DaysBeforeReturn", "birthDate", "email", "imageData", "isAuthor", "name", "surname" },
                values: new object[,]
                {
                    { 1m, 0, new DateTime(2001, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.kowalski@gmail.com", null, false, "Jan", "Kowalski" },
                    { 2m, 0, new DateTime(1982, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "glowa123@gmail.com", null, false, "Karol", "Głowacki" },
                    { 3m, 0, new DateTime(2000, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ibruska00@poczta.onet.com", null, false, "Ida", "Bruska" },
                    { 4m, 0, new DateTime(2002, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "juliaww@gmail.com", null, false, "Julia", "Kowalska" },
                    { 5m, 0, new DateTime(1998, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "kanialek@wp.pl", null, false, "Eryk", "Kańczyk" }
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "roomTypeId", "name", "price" },
                values: new object[,]
                {
                    { 1m, "Sala konferecyjna", 200m },
                    { 2m, "Sala multimedialna", 400m },
                    { 3m, "Pracownia komputerowa", 100.5m }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "tagId", "name" },
                values: new object[,]
                {
                    { 1m, "tag1" },
                    { 2m, "tag2" },
                    { 3m, "tag3" },
                    { 4m, "tag4" },
                    { 5m, "tag5" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "bookId", "ISBN", "alley", "availableCopys", "description", "floor", "genreId", "imageData", "publisherId", "ratingAVG", "ratingId", "releaseDate", "rowNumber", "title", "typeId" },
                values: new object[,]
                {
                    { 1m, 4789305434712m, 0m, 14m, "super hit o hobbitach", 0m, 1m, null, 1m, null, 1, new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, "Władca Pierścieni", 1m },
                    { 2m, 8495208934212m, 0m, 6m, "bo Ikar był zbrodniarzem", 0m, 2m, null, 2m, null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, "Zbrodnia Ikara", 2m },
                    { 3m, 3489012343021m, 0m, 26m, "Geralt ze szkoły wilka, historia prawdziwa", 0m, 3m, null, 3m, null, 3, new DateTime(1975, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, "Wiedźmin", 3m },
                    { 4m, 9321734921412m, 0m, 11m, "o księciu który był naprawdę mały", 0m, 4m, null, 4m, null, 4, new DateTime(1987, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, "Mały Książe", 4m },
                    { 5m, 8940237032412m, 0m, 16m, "powieść o pół hobbicie a pół elfie", 0m, 5m, null, 5m, null, 5, new DateTime(2009, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, "Władca Pierścieni cz. 4", 5m }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "employeeId", "dateOfEmployment", "email", "name", "positionId", "surname" },
                values: new object[,]
                {
                    { 1m, new DateTime(2022, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "janusz.kowalski@gmail.com", "Janusz", 1m, "Kowalski" },
                    { 2m, new DateTime(2016, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "koblik@koblik.com", "Katarzyna", 2m, "Oblik" },
                    { 3m, new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wykrok.j@gmail.com", "Jędrzej", 3m, "Wykrok" },
                    { 4m, new DateTime(2017, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "barnowak@gmail.com", "Barbara", 4m, "Nowak" },
                    { 5m, new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "slimakBoss@legenda.pl", "Kamil", 5m, "Ślimak" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "eventId", "authorId", "description", "eventDate", "name" },
                values: new object[,]
                {
                    { 1m, 1m, "spotkanie fanów książek autora numer 1 ale nazywa się inaczej tylko nie chciało mi się sprawdzić bo długo by scrollować", new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Local), "Spotkanie fanów książek" },
                    { 2m, 4m, "spotkanie antyfanów książek a autor numer 4 będzie ich zachęcał aby czytali", new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Local), "Spotkanie antyfanów książek" },
                    { 3m, 8m, "Autor Adam Niemowa będzie rozdawał autografy ale nie można do niego mówić bo sobie tego nie życzy i wtedy nie da autografu ", new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Local), "Podpisywanie książek" },
                    { 4m, 3m, "Konkurs w którym udział może wziąć każdy. Uczestnicy będą mieli 1h na napisanie rozdziału, który zostanie nastepnie oceniony. Czekają na Was wspaniałe nagrody!", new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Local), "Konkurs pisania na czas" },
                    { 5m, 9m, "Nauczymy się jak poprawnie pisać. Żerzuha a może rzeżuha czy żeżucha bądź rzerzucha? Na spotkaniu to oraz wiele wiedzy więcej.", new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), "Dyktando" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "roomNumber", "roomTypeId", "seatCount" },
                values: new object[,]
                {
                    { 1m, 1m, 5m },
                    { 2m, 3m, 4m },
                    { 3m, 3m, 3m },
                    { 4m, 2m, 2m },
                    { 5m, 1m, 1m }
                });

            migrationBuilder.InsertData(
                table: "Book_Author",
                columns: new[] { "authorId", "bookId" },
                values: new object[,]
                {
                    { 1m, 1m },
                    { 2m, 1m },
                    { 3m, 2m },
                    { 3m, 3m },
                    { 4m, 4m },
                    { 5m, 5m }
                });

            migrationBuilder.InsertData(
                table: "Book_Opinions",
                columns: new[] { "bookId", "readerId", "addedDate", "opinion", "rating" },
                values: new object[,]
                {
                    { 1m, 1m, new DateTime(2024, 3, 18, 3, 48, 51, 580, DateTimeKind.Local).AddTicks(803), "niesmowicie wciągająca książka", 5m },
                    { 2m, 2m, new DateTime(2024, 3, 29, 4, 48, 51, 580, DateTimeKind.Local).AddTicks(861), "taka sobie", 2m },
                    { 3m, 3m, new DateTime(2024, 4, 7, 9, 48, 51, 580, DateTimeKind.Local).AddTicks(864), "nawet fajna ale czasem za długie opisy", 4m },
                    { 4m, 4m, new DateTime(2024, 4, 5, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(866), "beznadziejna", 1m },
                    { 4m, 5m, new DateTime(2024, 4, 9, 21, 48, 51, 580, DateTimeKind.Local).AddTicks(868), "mi się nawet podoba", 3m }
                });

            migrationBuilder.InsertData(
                table: "Book_Tag",
                columns: new[] { "bookId", "tagId" },
                values: new object[,]
                {
                    { 1m, 1m },
                    { 2m, 2m },
                    { 3m, 3m },
                    { 4m, 4m },
                    { 5m, 5m }
                });

            migrationBuilder.InsertData(
                table: "Borrowing",
                columns: new[] { "borrowId", "Confirmation", "IsPaid", "IsReturned", "LateFee", "bookId", "bookLost", "borrowDate", "employeeConfirmingPaymentId", "employeeConfirmingReturnId", "employeeId", "plannedReturnDate", "returnDate" },
                values: new object[,]
                {
                    { 1m, false, false, false, 0m, 1m, false, new DateTime(2024, 4, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1023), null, null, 1m, new DateTime(2024, 5, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1026), null },
                    { 2m, false, false, false, 3m, 2m, false, new DateTime(2024, 3, 7, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1035), null, null, 2m, new DateTime(2024, 4, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1037), null },
                    { 3m, false, false, false, 0m, 3m, false, new DateTime(2024, 3, 26, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1041), null, null, 3m, new DateTime(2024, 4, 25, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1042), null },
                    { 4m, true, false, false, 0m, 4m, true, new DateTime(2024, 4, 4, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1044), null, null, 4m, new DateTime(2024, 5, 4, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1045), new DateTime(2024, 5, 10, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1047) },
                    { 5m, false, false, true, 0m, 5m, false, new DateTime(2024, 4, 8, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1050), null, null, 5m, new DateTime(2024, 5, 8, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1051), new DateTime(2024, 4, 29, 0, 48, 51, 580, DateTimeKind.Local).AddTicks(1053) }
                });

            migrationBuilder.InsertData(
                table: "EmployeeData",
                columns: new[] { "employeeId", "birthDate", "houseNumber", "pesel", "phoneNumber", "street", "town", "zipCode" },
                values: new object[,]
                {
                    { 1m, new DateTime(1999, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 32m, 1101102223m, "743 934 324", "Kieliszkowska 12", "Białystok", "15-123" },
                    { 2m, new DateTime(2000, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3m, 1101102223m, "732 438 553", "Piastowska 19", "Sokółka", "16-132" },
                    { 3m, new DateTime(1974, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 7m, 1101102223m, "832 890 436", "Lipowa 3", "Łomża", "17-154" },
                    { 4m, new DateTime(1983, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 32m, 1101102223m, "683 690 268", "Wyrobiskowa 8", "Białystok", "15-136" },
                    { 5m, new DateTime(1666, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 666m, 2137100420m, "666 666 666", "Odwrotna 15", "Hel", "15-666" }
                });

            migrationBuilder.InsertData(
                table: "RoomReservation",
                columns: new[] { "reservationId", "Confirmation", "begginingOfReservationDate", "employeeId", "endOfReservationDate", "readerId", "roomId" },
                values: new object[,]
                {
                    { 1m, false, new DateTime(2024, 3, 22, 14, 30, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 22, 16, 30, 0, 0, DateTimeKind.Unspecified), 1m, 1m },
                    { 2m, false, new DateTime(2024, 3, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 22, 19, 0, 0, 0, DateTimeKind.Unspecified), 2m, 2m },
                    { 3m, false, new DateTime(2024, 3, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 22, 13, 30, 0, 0, DateTimeKind.Unspecified), 3m, 3m },
                    { 4m, false, new DateTime(2024, 3, 25, 8, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 4m, 4m },
                    { 5m, false, new DateTime(2024, 3, 27, 18, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 5m, 5m }
                });

            migrationBuilder.InsertData(
                table: "Reader_Borrowings",
                columns: new[] { "borrowId", "readerId" },
                values: new object[,]
                {
                    { 1m, 1m },
                    { 2m, 2m },
                    { 3m, 3m },
                    { 4m, 4m },
                    { 5m, 5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Book_genreId",
                table: "Book",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_publisherId",
                table: "Book",
                column: "publisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_typeId",
                table: "Book",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Author_authorId",
                table: "Book_Author",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Opinions_readerId",
                table: "Book_Opinions",
                column: "readerId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Tag_tagId",
                table: "Book_Tag",
                column: "tagId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_bookId",
                table: "Borrowing",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeConfirm~1",
                table: "Borrowing",
                column: "employeeConfirmingReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeConfirmi~",
                table: "Borrowing",
                column: "employeeConfirmingPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeId",
                table: "Borrowing",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_positionId",
                table: "Employee",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeConfirmingPayments~",
                table: "EmployeeConfirmingPaymentsBook",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeConfirmingReturnsB~",
                table: "EmployeeConfirmingReturnsBook",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_authorId",
                table: "Event",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Borrowings_borrowId",
                table: "Reader_Borrowings",
                column: "borrowId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_roomTypeId",
                table: "Room",
                column: "roomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_employeeId",
                table: "RoomReservation",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_readerId",
                table: "RoomReservation",
                column: "readerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_roomId",
                table: "RoomReservation",
                column: "roomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Book_Author");

            migrationBuilder.DropTable(
                name: "Book_Opinions");

            migrationBuilder.DropTable(
                name: "Book_Tag");

            migrationBuilder.DropTable(
                name: "EmployeeData");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "Reader_Borrowings");

            migrationBuilder.DropTable(
                name: "RoomReservation");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Borrowing");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "EmployeeConfirmingPaymentsBook");

            migrationBuilder.DropTable(
                name: "EmployeeConfirmingReturnsBook");

            migrationBuilder.DropTable(
                name: "RoomType");

            migrationBuilder.DropTable(
                name: "BookType");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
