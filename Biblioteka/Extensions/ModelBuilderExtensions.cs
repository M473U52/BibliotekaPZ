using Biblioteka.Areas.Identity.Data;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Biblioteka.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    genreId = 1,
                    name = "Przygodowe"
                },

                new Genre
                {
                    genreId = 2,
                    name = "Akcji"
                },

                new Genre
                {
                    genreId = 3,
                    name = "Dramat"
                },

                new Genre
                {
                    genreId = 4,
                    name = "Fanstasy"
                },

                new Genre
                {
                    genreId = 5,
                    name = "Science fiction"
                },

                new Genre
                {
                    genreId = 6,
                    name = "Horror"
                },

                new Genre
                {
                    genreId = 7,
                    name = "Komedia"
                },

                new Genre
                {
                    genreId = 8,
                    name = "Historyczne"
                },

                new Genre
                {
                    genreId = 9,
                    name = "Dokumentalne"
                },

                new Genre
                {
                    genreId = 10,
                    name = "Dla dzieci"
                }

                );

            // tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    tagId = 1,
                    name = "tag1"
                },

                new Tag
                {
                    tagId = 2,
                    name = "tag2"
                },

                new Tag
                {
                    tagId = 3,
                    name = "tag3"
                },

                new Tag
                {
                    tagId = 4,
                    name = "tag4"
                },

                new Tag
                {
                    tagId = 5,
                    name = "tag5"
                }

                );

            // types
            modelBuilder.Entity<BookType>().HasData(
                new BookType
                {
                    typeId = 1,
                    name = "typ1"
                },

                new BookType
                {
                    typeId = 2,
                    name = "typ2"
                },

                new BookType
                {
                    typeId = 3,
                    name = "typ3"
                },

                new BookType
                {
                    typeId = 4,
                    name = "typ4"
                },

                new BookType
                {
                    typeId = 5,
                    name = "typ5"
                }

                );

            // publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher
                {
                    publisherId = 1,
                    name = "wydawnictwo igła",
                    description = "tak"
                },

                new Publisher
                {
                    publisherId = 2,
                    name = "wydawnictwo2",
                    description = "opis2"
                },

                new Publisher
                {
                    publisherId = 3,
                    name = "wydawnictwo3",
                    description = "opis33331"
                },

                new Publisher
                {
                    publisherId = 4,
                    name = "wydawnictwo kosmos",
                    description = "wszystko co związane z kosmosem"
                },

                new Publisher
                {
                    publisherId = 5,
                    name = "wydawnictwo katolickie",
                    description = "wydawnictwo skupione na książkach religinych"
                }

                );

            modelBuilder.Entity<RoomType>().HasData(
               new RoomType
               {
                   roomTypeId = 1,
                   name = "Sala konferecyjna",
                   price = 200
               },

                new RoomType
                {
                    roomTypeId = 2,
                    name = "Sala multimedialna",
                    price = 400
                },

                new RoomType
                {
                    roomTypeId = 3,
                    name = "Pracownia komputerowa",
                    price = 100.50
                }
            );

            // rooms
           modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    roomNumber = 1,
                    seatCount = 5,
                    roomTypeId = 1
                },

                new Room
                {
                    roomNumber = 2,
                    seatCount = 4,
                    roomTypeId = 3
                },

                new Room
                {
                    roomNumber = 3,
                    seatCount = 3,
                    roomTypeId = 3
                },

                new Room
                {
                    roomNumber = 4,
                    seatCount = 2,
                    roomTypeId = 2
                },

                new Room
                {
                    roomNumber = 5,
                    seatCount = 1,
                    roomTypeId = 1
                }

            );

            // position
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    positionId = 1,
                    name = "barista",
                    salary = 2100
                },

                new Position
                {
                    positionId = 2,
                    name = "barista pomocniczy",
                    salary = 2200
                },

                new Position
                {
                    positionId = 3,
                    name = "recepcjonista",
                    salary = 2600
                },

                new Position
                {
                    positionId = 4,
                    name = "st. recepcjonista",
                    salary = 2851
                },

                new Position
                {
                    positionId = 5,
                    name = "szef pracowników",
                    salary = 6666,
                }

                );

            // employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    employeeId = 1,
                    name = "Janusz",
                    surname = "Kowalski",
                    email = "janusz.kowalski@gmail.com",
                    dateOfEmployment = DateTime.ParseExact("29.01.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    positionId = 1,
                },

                new Employee
                {
                    employeeId = 2,
                    name = "Katarzyna",
                    surname = "Oblik",
                    email = "koblik@koblik.com",
                    dateOfEmployment = DateTime.ParseExact("15.06.2016", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    positionId = 2,
                },

                new Employee
                {
                    employeeId = 3,
                    name = "Jędrzej",
                    surname = "Wykrok",
                    email = "wykrok.j@gmail.com",
                    dateOfEmployment = DateTime.ParseExact("01.10.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    positionId = 3,
                },

                new Employee
                {
                    employeeId = 4,
                    name = "Barbara",
                    surname = "Nowak",
                    email = "barnowak@gmail.com",
                    dateOfEmployment = DateTime.ParseExact("05.09.2017", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    positionId = 4,
                },

                new Employee
                {
                    employeeId = 5,
                    name = "Kamil",
                    surname = "Ślimak",
                    email = "slimakBoss@legenda.pl",
                    dateOfEmployment = DateTime.ParseExact("29.02.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    positionId = 5,
                }

                );

            // employee data
            modelBuilder.Entity<EmployeeData>().HasData(
                new EmployeeData
                {
                    employeeId = 1,
                    pesel = 01101102223,
                    street = "Kieliszkowska 12",
                    houseNumber = 32,
                    zipCode = "15-123",
                    town = "Białystok",
                    phoneNumber = "743 934 324",
                    birthDate = DateTime.ParseExact("11.05.1999", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                },

                new EmployeeData
                {
                    employeeId = 2,
                    pesel = 01101102223,
                    street = "Piastowska 19",
                    houseNumber = 3,
                    zipCode = "16-132",
                    town = "Sokółka",
                    phoneNumber = "732 438 553",
                    birthDate = DateTime.ParseExact("24.09.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                },

                new EmployeeData
                {
                    employeeId = 3,
                    pesel = 01101102223,
                    street = "Lipowa 3",
                    houseNumber = 7,
                    zipCode = "17-154",
                    town = "Łomża",
                    phoneNumber = "832 890 436",
                    birthDate = DateTime.ParseExact("18.11.1974", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                },

                new EmployeeData
                {
                    employeeId = 4,
                    pesel = 01101102223,
                    street = "Wyrobiskowa 8",
                    houseNumber = 32,
                    zipCode = "15-136",
                    town = "Białystok",
                    phoneNumber = "683 690 268",
                    birthDate = DateTime.ParseExact("30.05.1983", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                },

                new EmployeeData
                {
                    employeeId = 5,
                    pesel = 02137100420,
                    street = "Odwrotna 15",
                    houseNumber = 666,
                    zipCode = "15-666",
                    town = "Hel",
                    phoneNumber = "666 666 666",
                    birthDate = DateTime.ParseExact("06.06.1666", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                }

                );

            // bibusers
            /*modelBuilder.Entity<BibUser>().HasData(
                new BibUser
                {
                    Id = 1
                }
                );*/

            // readers 
            modelBuilder.Entity<Reader>().HasData(
                new Reader
                {
                    readerId = 1,
                    name = "Jan",
                    surname = "Kowalski",
                    birthDate = DateTime.ParseExact("10.01.2001", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    email = "jan.kowalski@gmail.com",
                },

                new Reader
                {
                    readerId = 2,
                    name = "Karol",
                    surname = "Głowacki",
                    birthDate = DateTime.ParseExact("02.07.1982", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    email = "glowa123@gmail.com"
                },

                new Reader
                {
                    readerId = 3,
                    name = "Ida",
                    surname = "Bruska",
                    birthDate = DateTime.ParseExact("14.04.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    email = "ibruska00@poczta.onet.com"
                },

                new Reader
                {
                    readerId = 4,
                    name = "Julia",
                    surname = "Kowalska",
                    birthDate = DateTime.ParseExact("09.05.2002", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    email = "juliaww@gmail.com"
                },

                new Reader
                {
                    readerId = 5,
                    name = "Eryk",
                    surname = "Kańczyk",
                    birthDate = DateTime.ParseExact("08.08.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    email = "kanialek@wp.pl",
                }


                );

            // authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    authorId = 1,
                    name = "J.R.R",
                    surname = "Tolkien",
                    birthDate = DateTime.ParseExact("03.01.1892", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Wielka Brytania",
                    nickname = "JRR",
                    description = "niesamowity pisarz",
                    email = "jrtolkien@tolkien.com",
                },

                new Author
                {
                    authorId = 2,
                    name = "J.K",
                    surname = "Rowling",
                    birthDate = DateTime.ParseExact("31.07.1965", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Wielka Brytania",
                    nickname = "Jo Rowling",
                    description = "niesamowita pisarka",
                    email = "jkrowling@rowling.com",
                },

                new Author
                {
                    authorId = 3,
                    name = "Andrzej",
                    surname = "Sapkowski",
                    birthDate = DateTime.ParseExact("21.06.1948", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Polska",
                    nickname = "Sapek",
                    description = "wiedźmin jakich mało",
                    email = "papawiedzmin@gmail.com",
                },

                new Author
                {
                    authorId = 4,
                    name = "Stephen",
                    surname = "King",
                    birthDate = DateTime.ParseExact("21.09.1947", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Stany Zjednoczone",
                    nickname = "St King",
                    description = "super pisarz",
                    email = "stking@stking.com",
                },

                new Author
                {
                    authorId = 5,
                    name = "Adam",
                    surname = "Mickiewicz",
                    birthDate = DateTime.ParseExact("24.12.1798", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Polska",
                    nickname = "A Micki",
                    description = "trochę pali",
                    email = "amickiewicz@mickifan.com",
                },

                new Author
                {
                    authorId = 6,
                    name = "Henryk",
                    surname = "Sienkiwicz",
                    birthDate = DateTime.ParseExact("05.05.1846", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Polska",
                    nickname = "Litwos",
                    description = "Henryk Adam Aleksander Pius Sienkiewicz",
                    email = "czlowiekpuszczy@gmail.com",
                },

                new Author
                {
                    authorId = 7,
                    name = "Czesław",
                    surname = "Miłosz",
                    birthDate = DateTime.ParseExact("30.06.1911", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Polska",
                    nickname = "Czesio",
                    description = "czesław aż miło",
                    email = "czesio123@yahoo.com",
                },

                new Author
                {
                    authorId = 8,
                    name = "Adam",
                    surname = "Niemowa",
                    birthDate = DateTime.ParseExact("23.11.1898", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Polska",
                    nickname = "...",
                    description = "nic nie powie ale napisze",
                    email = "aniemowilem@gmail.com",
                },

                new Author
                {
                    authorId = 9,
                    name = "Stephen",
                    surname = "Hawking",
                    birthDate = DateTime.ParseExact("08.01.1942", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Wielka Brytania",
                    nickname = "Stephen",
                    description = "giga mózg ale książki też pisał",
                    email = "blackhole@gmail.com",
                },

                new Author
                {
                    authorId = 10,
                    name = "George R.R",
                    surname = "Martin",
                    birthDate = DateTime.ParseExact("20.09.1948", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    country = "Stany Zjednoczone",
                    nickname = "GRRR",
                    description = "gra o tron ale nie gra w gry",
                    email = "grrmartin@grrmartin.com"
                }

                );

            // position
            modelBuilder.Entity<Book_Opinions>().HasData(
                new Book_Opinions
                {
                    bookId = 1,
                    readerId = 1,
                    opinion = "niesmowicie wciągająca książka",
                    addedDate = DateTime.Now.AddDays(-23).AddHours(3),
                    rating = 5,
                },

                new Book_Opinions
                {
                    bookId = 2,
                    readerId = 2,
                    opinion = "taka sobie",
                    addedDate = DateTime.Now.AddDays(-12).AddHours(4),
                    rating = 2,
                },

                new Book_Opinions
                {
                    bookId = 3,
                    readerId = 3,
                    opinion = "nawet fajna ale czasem za długie opisy",
                    addedDate = DateTime.Now.AddDays(-3).AddHours(9),
                    rating = 4,
                },

                new Book_Opinions
                {
                    bookId = 4,
                    readerId = 4,
                    opinion = "beznadziejna",
                    addedDate = DateTime.Now.AddDays(-5).AddHours(0),
                    rating = 1,
                },

                new Book_Opinions
                {
                    bookId = 4,
                    readerId = 5,
                    opinion = "mi się nawet podoba",
                    addedDate = DateTime.Now.AddDays(0).AddHours(-3),
                    rating = 3,
                }



                );

            // books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    bookId = 1,
                    title = "Władca Pierścieni",
                    description = "super hit o hobbitach",
                    availableCopys = 14,
                    releaseDate = DateTime.ParseExact("29.07.1954", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    genreId = 1,
                    publisherId = 1,
                    typeId = 1,
                    ISBN = 4789305434712,
                    ratingId = 1,
                },

                new Book
                {
                    bookId = 2,
                    title = "Zbrodnia Ikara",
                    description = "bo Ikar był zbrodniarzem",
                    availableCopys = 6,
                    releaseDate = DateTime.ParseExact("01.01.0001", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    genreId = 2,
                    publisherId = 2,
                    typeId = 2,
                    ISBN = 8495208934212,
                    ratingId = 2,
                },

                new Book
                {
                    bookId = 3,
                    title = "Wiedźmin",
                    description = "Geralt ze szkoły wilka, historia prawdziwa",
                    availableCopys = 26,
                    releaseDate = DateTime.ParseExact("21.03.1975", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    genreId = 3,
                    publisherId = 3,
                    typeId = 3,
                    ISBN = 3489012343021,
                    ratingId = 3,
                },

                new Book
                {
                    bookId = 4,
                    title = "Mały Książe",
                    description = "o księciu który był naprawdę mały",
                    availableCopys = 11,
                    releaseDate = DateTime.ParseExact("22.06.1987", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    genreId = 4,
                    publisherId = 4,
                    typeId = 4,
                    ISBN = 9321734921412,
                    ratingId = 4,
                },

                new Book
                {
                    bookId = 5,
                    title = "Władca Pierścieni cz. 4",
                    description = "powieść o pół hobbicie a pół elfie",
                    availableCopys = 16,
                    releaseDate = DateTime.ParseExact("19.11.2009", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    genreId = 5,
                    publisherId = 5,
                    typeId = 5,
                    ISBN = 8940237032412,
                    ratingId = 5,
                }

                );

            // book authors
            modelBuilder.Entity<Book_Author>().HasData(
                new Book_Author
                {
                    bookId = 1,
                    authorId = 1,
                },

                new Book_Author
                {
                    bookId = 1,
                    authorId = 2
                },

                new Book_Author
                {
                    bookId = 2,
                    authorId = 3
                },

                new Book_Author
                {
                    bookId = 3,
                    authorId = 3
                },

                new Book_Author
                {
                    bookId = 4,
                    authorId = 4
                },

                new Book_Author
                {
                    bookId = 5,
                    authorId = 5
                }

                );

            // book tag
            modelBuilder.Entity<Book_Tag>().HasData(
                new Book_Tag
                {
                    bookId = 1,
                    tagId = 1,
                },

                new Book_Tag
                {
                    bookId = 2,
                    tagId = 2,
                },

                new Book_Tag
                {
                    bookId = 3,
                    tagId = 3,
                },

                new Book_Tag
                {
                    bookId = 4,
                    tagId = 4,
                },

                new Book_Tag
                {
                    bookId = 5,
                    tagId = 5,
                }
                );


            // room reservations
            modelBuilder.Entity<RoomReservation>().HasData(
                new RoomReservation
                {
                    reservationId = 1,
                    begginingOfReservationDate = DateTime.ParseExact("22.03.2024 14:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),

            endOfReservationDate = DateTime.ParseExact("22.03.2024 16:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    readerId = 1,
                    roomId = 1
                },

                new RoomReservation
                {
                    reservationId = 2,
                    begginingOfReservationDate = DateTime.ParseExact("22.03.2024 16:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    endOfReservationDate = DateTime.ParseExact("22.03.2024 19:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    readerId = 2,
                    roomId = 2
                },

                new RoomReservation
                {
                    reservationId = 3,
                    begginingOfReservationDate = DateTime.ParseExact("23.03.2024 9:00", "dd.MM.yyyy H:mm", CultureInfo.InvariantCulture),
                    endOfReservationDate = DateTime.ParseExact("22.03.2024 13:30", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    readerId = 3,
                    roomId = 3
                },

                new RoomReservation
                {
                    reservationId = 4,
                    begginingOfReservationDate = DateTime.ParseExact("25.03.2024 8:00", "dd.MM.yyyy H:mm", CultureInfo.InvariantCulture),
                    endOfReservationDate = DateTime.ParseExact("22.03.2024 12:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    readerId = 4,
                    roomId = 4
                },

                new RoomReservation
                {
                    reservationId = 5,
                    begginingOfReservationDate = DateTime.ParseExact("27.03.2024 18:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    endOfReservationDate = DateTime.ParseExact("22.03.2024 20:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    readerId = 5,
                    roomId = 5
                }

                );

            // borrowings
            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing
                {
                    borrowId = 1,
                    bookId = 1,
                    borrowDate = DateTime.Now,
                    plannedReturnDate = DateTime.Now.AddMonths(1),
                    returnDate = null,
                    IsReturned = false,
                    employeeId = 1,
                    //book = ,
                },

                new Borrowing
                {
                    borrowId = 2,
                    bookId = 2,
                    borrowDate = DateTime.Now.AddMonths(-1).AddDays(-3),
                    plannedReturnDate = DateTime.Now,
                    returnDate = null,
                    IsReturned = false,
                    LateFee = 3,
                    employeeId = 2,
                    //book = 
                },

                new Borrowing
                {
                    borrowId = 3,
                    bookId = 3,
                    borrowDate = DateTime.Now.AddDays(-15),
                    plannedReturnDate = DateTime.Now.AddMonths(1).AddDays(-15),
                    returnDate = null,
                    IsReturned = false,
                    employeeId = 3,
                    //book = 
                },

                new Borrowing
                {
                    borrowId = 4,
                    bookId = 4,
                    borrowDate = DateTime.Now.AddDays(-6),
                    plannedReturnDate = DateTime.Now.AddMonths(1).AddDays(-6),
                    returnDate = DateTime.Now.AddMonths(1),
                    IsReturned = false,
                    bookLost = true,
                    Confirmation = true,
                    employeeId = 4,
                    //book = 
                },

                new Borrowing
                {
                    borrowId = 5,
                    bookId = 5,
                    borrowDate = DateTime.Now.AddDays(-2),
                    plannedReturnDate = DateTime.Now.AddMonths(1).AddDays(-2),
                    returnDate = DateTime.Now.AddMonths(1).AddDays(-11),
                    IsReturned = true,
                    employeeId = 5,
                    //book = 
                }

                );

            // reader borrowings

            modelBuilder.Entity<Reader_Borrowings>().HasData(
                new Reader_Borrowings
                {
                    readerId = 1,
                    borrowId = 1,
                },

                new Reader_Borrowings
                {
                    readerId = 2,
                    borrowId = 2,
                },

                new Reader_Borrowings
                {
                    readerId = 3,
                    borrowId = 3,
                },

                new Reader_Borrowings
                {
                    readerId = 4,
                    borrowId = 4,
                },

                new Reader_Borrowings
                {
                    readerId = 5,
                    borrowId = 5,
                }

                );

            // eventy
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    eventId = 1,
                    eventDate = DateTime.Today.AddDays(1),
                    name = "Spotkanie fanów książek",
                    authorId = 1,
                    description = "spotkanie fanów książek autora numer 1 ale nazywa się inaczej tylko nie chciało mi się sprawdzić bo długo by scrollować"
                },

                new Event
                {
                    eventId = 2,
                    eventDate = DateTime.Today.AddDays(3),
                    name = "Spotkanie antyfanów książek",
                    authorId = 4,
                    description = "spotkanie antyfanów książek a autor numer 4 będzie ich zachęcał aby czytali"
                },

                new Event
                {
                    eventId = 3,
                    eventDate = DateTime.Today.AddDays(3),
                    name = "Podpisywanie książek",
                    authorId = 8,
                    description = "Autor Adam Niemowa będzie rozdawał autografy ale nie można do niego mówić bo sobie tego nie życzy i wtedy nie da autografu "
                },

                new Event
                {
                    eventId = 4,
                    eventDate = DateTime.Today.AddDays(6),
                    name = "Konkurs pisania na czas",
                    authorId = 3,
                    description = "Konkurs w którym udział może wziąć każdy. Uczestnicy będą mieli 1h na napisanie rozdziału, który zostanie nastepnie oceniony. Czekają na Was wspaniałe nagrody!"
                },

                new Event
                {
                    eventId = 5,
                    eventDate = DateTime.Today.AddDays(11),
                    name = "Dyktando",
                    authorId = 9,
                    description = "Nauczymy się jak poprawnie pisać. Żerzuha a może rzeżuha czy żeżucha bądź rzerzucha? Na spotkaniu to oraz wiele wiedzy więcej."
                }

                );

            modelBuilder.Entity<FAQ>().HasData(
                new FAQ
                {
                    FAQId = 1,
                    question = "jak wypożyczyć książkę?",
                    answer = "aby wypożyczyć książkę należy najpierw się zalogować a następnie wejść w księgozbior i nacisnąć wypożycz jeśli jest wystarczająca ilość egzemplarzy. książka zostanie dodana do koszyka i będzie można wybrać datę jej wypożyczenia."
                },

                new FAQ
                {
                    FAQId = 2,
                    question = "jak cośtam?",
                    answer = "jak nie wiesz to sobie poklikaj i posprawdzaj. nie mam całej nocy na pisanie tej sekcji ;)"
                },

                new FAQ
                {
                    FAQId = 3,
                    question = "jak się wylogować z konta?",
                    answer = "trzeba szybko i kilkukrotnie nacisnąć kombinację klawiszy ALT + F4"
                }

                );
        }
    }
}
