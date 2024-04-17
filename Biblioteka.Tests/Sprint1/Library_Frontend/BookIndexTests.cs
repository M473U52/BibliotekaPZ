using Biblioteka.Views.Books;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;

namespace Biblioteka.Tests.Sprint1.Library_Frontend
{
    public class BookIndexTests
    {
        [Fact]
        public void BookCountForGenre_ShouldReturnCorrectCount()
        {
            // Arrange
            // Fake'owanie interfejsów repozytoriów
            var bookRepository = A.Fake<IBookRepository>();
            var genreRepository = A.Fake<IGenreRepository>();
            var bookTypeRepository = A.Fake<IBookTypeRepository>();
            var tagRepository = A.Fake<ITagRepository>();
            var authorRepository = A.Fake<IAuthorRepository>();

            // Tworzenie listy obiektów książek z gatunkami
            var genreId = 1;
            var books = new List<Book>
            {
                new Book { genre = new Genre { genreId = genreId } },
                new Book { genre = new Genre { genreId = genreId } },
                new Book { genre = new Genre { genreId = 2 } }
            };

            // Gdy zostanie wywołana metoda bookRepository.SearchBooks("")) niech zwróci listę książek
            A.CallTo(() => bookRepository.SearchBooks("")).Returns(books);

            // Tworzenie obiektu strony Index Book
            var indexModel = new IndexModel(bookRepository, genreRepository, bookTypeRepository, tagRepository, authorRepository);
            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet();

            // Act
            // Wywołanie odpowiedniej metody
            var count = indexModel.BookCountForGenre(genreId);

            // Assert
            // Sprawdzenie wyniku
            count.Should().Be(2);
        }

        [Fact]
        public void BookCountForType_ShouldReturnCorrectCount()
        {
            // Arrange
            // Fake'owanie interfejsów repozytoriów
            var bookRepository = A.Fake<IBookRepository>();
            var genreRepository = A.Fake<IGenreRepository>();
            var bookTypeRepository = A.Fake<IBookTypeRepository>();
            var tagRepository = A.Fake<ITagRepository>();
            var authorRepository = A.Fake<IAuthorRepository>();

            // Tworzenie listy obiektów książek z typami
            var typeId = 3;
            var books = new List<Book>
            {
                new Book { type = new BookType { typeId = typeId } },
                new Book { type = new BookType { typeId = typeId } },
                new Book { type = new BookType { typeId = 5 } }
            };

            // Gdy zostanie wywołana metoda bookRepository.SearchBooks("")) niech zwróci listę książek
            A.CallTo(() => bookRepository.SearchBooks("")).Returns(books);

            // Tworzenie obiektu strony Index Book
            var indexModel = new IndexModel(bookRepository, genreRepository, bookTypeRepository, tagRepository, authorRepository);
            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet();

            // Act
            // Wywołanie odpowiedniej metody
            var count = indexModel.BookCountForType(typeId);

            // Assert
            // Sprawdzenie wyniku
            count.Should().Be(2);
        }

        [Fact]
        public void AvailableBookCount_ShouldReturnCorrectCount()
        {
            // Arrange
            // Fake'owanie interfejsów repozytoriów
            var bookRepository = A.Fake<IBookRepository>();
            var genreRepository = A.Fake<IGenreRepository>();
            var bookTypeRepository = A.Fake<IBookTypeRepository>();
            var tagRepository = A.Fake<ITagRepository>();
            var authorRepository = A.Fake<IAuthorRepository>();

            // Tworzenie listy obiektów książek z ilością kopii
            // ilość >0 oznacza że książka dostępna
            var books = new List<Book>
            {
                new Book { availableCopys = 3 },
                new Book { availableCopys = 0 },
                new Book { availableCopys = 7 },
                new Book { availableCopys = 40 }
            };

            A.CallTo(() => bookRepository.SearchBooks("")).Returns(books);

            // Tworzenie obiektu strony Index Book
            var indexModel = new IndexModel(bookRepository, genreRepository, bookTypeRepository, tagRepository, authorRepository);
            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet();


            // Act
            // Wywołanie odpowiedniej metody
            var count = indexModel.AvailableBookCount();

            // Assert
            // Sprawdzenie czy metoda poprawnie wykryła że są dostępne 2 książki
            count.Should().Be(3);
        }

        [Fact]
        public void LowQuantityBookCount_ShouldReturnCorrectCount()
        {
            // Arrange
            // Fake'owanie interfejsów repozytoriów
            var bookRepository = A.Fake<IBookRepository>();
            var genreRepository = A.Fake<IGenreRepository>();
            var bookTypeRepository = A.Fake<IBookTypeRepository>();
            var tagRepository = A.Fake<ITagRepository>();
            var authorRepository = A.Fake<IAuthorRepository>();

            // Tworzenie listy obiektów książek z ilością kopii
            // ilość > 0 ale < 10 oznacza że książka dostępna w małej ilości
            var books = new List<Book>
            {
                new Book { availableCopys = 3 },
                new Book { availableCopys = 0 },
                new Book { availableCopys = 7 },
                new Book { availableCopys = 40 },
                new Book { availableCopys = 10 }
            };

            A.CallTo(() => bookRepository.SearchBooks("")).Returns(books);

            // Tworzenie obiektu strony Index Book
            var indexModel = new IndexModel(bookRepository, genreRepository, bookTypeRepository, tagRepository, authorRepository);
            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet();


            // Act
            // Wywołanie odpowiedniej metody
            var count = indexModel.LowQuantityBookCount();

            // Assert
            // Sprawdzenie czy metoda poprawnie wykryła że są dostępne 2 książki
            count.Should().Be(2);
        }

        [Fact]
        public void NotAvailableBookCount_ShouldReturnCorrectCount()
        {
            // Arrange
            // Fake'owanie interfejsów repozytoriów
            var bookRepository = A.Fake<IBookRepository>();
            var genreRepository = A.Fake<IGenreRepository>();
            var bookTypeRepository = A.Fake<IBookTypeRepository>();
            var tagRepository = A.Fake<ITagRepository>();
            var authorRepository = A.Fake<IAuthorRepository>();

            // Tworzenie listy obiektów książek z ilością kopii
            // ilość > 0 ale < 10 oznacza że książka dostępna w małej ilości
            var books = new List<Book>
            {
                new Book { availableCopys = 3 },
                new Book { availableCopys = 0 },
                new Book { availableCopys = 7 },
                new Book { availableCopys = 40 },
                new Book { availableCopys = 10 }
            };

            A.CallTo(() => bookRepository.SearchBooks("")).Returns(books);

            // Tworzenie obiektu strony Index Book
            var indexModel = new IndexModel(bookRepository, genreRepository, bookTypeRepository, tagRepository, authorRepository);
            indexModel.SearchTerm = "";


            // Wywołanie onGet
            indexModel.OnGet();

            // Act
            // Wywołanie odpowiedniej metody
            var count = indexModel.NotAvailableBookCount();

            // Assert
            // Sprawdzenie czy metoda poprawnie wykryła że 1 książka jest niedostępna
            count.Should().Be(1);
        }
    }
}
