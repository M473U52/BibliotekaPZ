using Biblioteka.Models;
using FakeItEasy;
using Biblioteka.Pages;
using Biblioteka.Context;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Services;
using Biblioteka.Repositories.DbImplementations;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Biblioteka.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace TestsBib
{
    public class HomePageTest
    {

        [Fact]
        public void NewPublicationsHomeTest()
        {
            //WA¯NE!!!!!!!!
            //DateTime.Parse("2024-04-01");strona bierze dzisiejsza date (-30 DNI OD NIEJ) dlatego przed wykonaniem testow nalezy dopasowac date i oczekiwane wyniki

            //sprawdzenie z Pages/Index
            //NewPublications = _bibContext.Book.OrderBy(d => d.releaseDate).Where(d => d.releaseDate >= lastMonth).ToList();

            var options = new DbContextOptionsBuilder<BibContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BibliotekaPZ;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;


            var realContext = new BibContext(options);
            var eventRepo = A.Fake<IEventRepository>();
            var bibUser = A.Fake<UserManager<BibUser>>();
            var borrowingRepository = A.Fake<IBorrowingRepository>();
            var emailSender = A.Fake<IEmailSender>();
            var bookRepo = A.Fake<IBookRepository>();

            var books = new List<Book>
            {
                new Book { bookId = 1, ISBN = 1234567890123, title = "ksiazka", releaseDate = DateTime.Parse("2024-03-11"),  availableCopys = 22, authors = null, alley = 2, ratingAVG = 3 },
                new Book { bookId = 2, ISBN = 1234567890122, title = "ksiazka2", releaseDate = DateTime.Parse("2022-02-09"),  availableCopys = 22, authors = null, alley = 2, ratingAVG = 3 },
                new Book { bookId = 3, ISBN = 1234567890125, title = "ksiazka4", releaseDate = DateTime.Parse("2024-02-28"),  availableCopys = 22, authors = null, alley = 2, ratingAVG = 3 },
                new Book { bookId = 4, ISBN = 1234567890126, title = "ksiazka5", releaseDate = DateTime.Parse("2024-01-01"),  availableCopys = 22, authors = null, alley = 2, ratingAVG = 5 },
                new Book { bookId = 5, ISBN = 1234567890128, title = "ksiazka6", releaseDate = DateTime.Parse("2024-03-22"),  availableCopys = 22, authors = null, alley = 2, ratingAVG = 5 }
            };


            A.CallTo(() => bookRepo.getAll()).Returns(books);

            var indexModel = new IndexModel(realContext, bookRepo, eventRepo, bibUser, borrowingRepository, emailSender);

            var result = indexModel.OnGet();

            Assert.Equal(3, indexModel.NewPublications.Count);
            Assert.Equal(books[4], indexModel.NewPublications[0]);
            Assert.Equal(books[0], indexModel.NewPublications[1]);
            Assert.Equal(books[2], indexModel.NewPublications[2]);
        }

        [Fact]
        public void EventFutureTest()
        {
            //WA¯NE!!!!!!!!
            //DateTime.Parse("2024-03-26");strona bierze dzisiejsza date dlatego przed wykonaniem testow nalezy dopasowac date i oczekiwane wyniki

            //sprawdzenie z Pages/Index

            var options = new DbContextOptionsBuilder<BibContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BibliotekaPZ;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;


            var realContext = new BibContext(options);
            var eventRepo = A.Fake<IEventRepository>();
            var bibUser = A.Fake<UserManager<BibUser>>();
            var borrowingRepository = A.Fake<IBorrowingRepository>();
            var emailSender = A.Fake<IEmailSender>();
            var bookRepo = A.Fake<IBookRepository>();

            var events = new List<Event>
            {
                new Event { eventId = 2,  name = "ela", description = "opis2", author = null, eventDate = DateTime.Parse("2024-02-09") },
                new Event { eventId = 1,  name = "Hania i ktosiek", description = "opis1", author = null, eventDate = DateTime.Parse("2024-01-07") },
                new Event { eventId = 3,  name = "spotkanie", description = "opis3", author = null, eventDate = DateTime.Parse("2024-03-14") },
                new Event { eventId = 4,  name = "eerfg", description = "opis5", author = null, eventDate = DateTime.Parse("2024-09-12") },
                new Event { eventId = 5,  name = "erfw", description = "opis4", author = null, eventDate = DateTime.Parse("2024-04-20") }
            };

            A.CallTo(() => eventRepo.getAll()).Returns(events);

            var indexModel = new IndexModel(realContext, bookRepo, eventRepo, bibUser, borrowingRepository, emailSender);

            var result =  indexModel.OnGet();

            Assert.Equal(2, indexModel.Events.Count);
            Assert.Equal(events[4], indexModel.Events[0]);
            Assert.Equal(events[3], indexModel.Events[1]);
        }
    }
}