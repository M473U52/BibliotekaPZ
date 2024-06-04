using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using Biblioteka.Views.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Biblioteka.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Tests.Sprint_8.Author_Book_Genre
{
    public class EditBookTests
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private IPublisherRepository _publisherRepository;
        private IAuthorRepository _authorRepository;
        private ITagRepository _tagRepository;
        private readonly TempDataDictionary tempData;
        public EditBookTests()
        {
            _bookRepository = A.Fake<IBookRepository>();
            _genreRepository = A.Fake<IGenreRepository>();
            _bookTypeRepository = A.Fake<IBookTypeRepository>();
            _publisherRepository = A.Fake<IPublisherRepository>();
            _authorRepository = A.Fake<IAuthorRepository>();
            _tagRepository = A.Fake<ITagRepository>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }
        [Fact]
        public void OnPostEditBook_Should_Not_EditBook_Author_And_Provide_Error_Message_Because_Of_Invalid_Book_ID()
        {
            // Arrange
            var book = new BookDto { bookId = 1 };
            var editModel = new EditModel(_bookRepository, _genreRepository,
                                          _publisherRepository, _bookTypeRepository,
                                          _authorRepository, _tagRepository)
            {
                TempData = tempData,
                Book = book
            };

            A.CallTo(() => _bookRepository.getOne(book.bookId)).Returns(null);

            // Act
            var result = editModel.OnPost() as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Error/Brak książki o id {book.bookId} w bazie");
        }

        [Fact]
        public void OnPostEditBook_Should_EditBook_And_Provide_Correct_Success_Message()
        {
            // Arrange
            var bookId = 1;
            var genreId = 1;
            var publisherId = 1;
            var bookTypeId = 1;
            var authorIds = new[] { "1" };
            var tagIds = new[] { "1" };

            var existingBook = new Book
            {
                bookId = bookId,
                title = "Old Title",
                ISBN = 7894561231234,
                description = "Old Description",
                availableCopys = 5,
                ratingAVG = 4.5,
                releaseDate = new DateTime(2020, 1, 1),
                floor = 1,
                alley = 1,
                rowNumber = 1,
                genre = new Genre { genreId = genreId, name = "Old Genre" },
                publisher = new Publisher { publisherId = publisherId, name = "Old Publisher" },
                type = new BookType { typeId = bookTypeId, name = "Old Type" },
                authors = new List<Book_Author>
                {
                    new Book_Author { author = new Author { authorId = 1, name = "Old Author" } }
                },
                tags = new List<Book_Tag>
                {
                    new Book_Tag { tag = new Tag { tagId = 1, name = "Old Tag" } }
                }
            };

            var newBook = new BookDto
            {
                bookId = bookId,
                title = "New Title",
                ISBN = 7894561231234,
                description = "New Description",
                availableCopys = 10,
                ratingAVG = 5.0,
                releaseDate = new DateTime(2021, 1, 1),
                floor = 2,
                alley = 2,
                rowNumber = 2,
            };

            A.CallTo(() => _bookRepository.getOne(bookId)).Returns(existingBook);
            A.CallTo(() => _genreRepository.getAll()).Returns(new List<Genre> { new Genre { genreId = genreId, name = "New Genre" } });
            A.CallTo(() => _publisherRepository.getAll()).Returns(new List<Publisher> { new Publisher { publisherId = publisherId, name = "New Publisher" } });
            A.CallTo(() => _bookTypeRepository.getAll()).Returns(new List<BookType> { new BookType { typeId = bookTypeId, name = "New Type" } });
            A.CallTo(() => _authorRepository.getAll()).Returns(new List<Author> { new Author { authorId = 1, name = "New Author" } });
            A.CallTo(() => _tagRepository.getAll()).Returns(new List<Tag> { new Tag { tagId = 1, name = "New Tag" } });

            var editModel = new EditModel(_bookRepository, _genreRepository,
                                          _publisherRepository, _bookTypeRepository,
                                          _authorRepository, _tagRepository)
            {
                TempData = tempData,
                Book = newBook,
                GenreId = genreId.ToString(),
                PublisherId = publisherId.ToString(),
                BookTypeId = bookTypeId.ToString(),
                AuthorIds = authorIds,
                TagIds = tagIds
            };

            // Act
            var result = editModel.OnPost() as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie dokonano edycji książki \"{newBook.title}\".");

            A.CallTo(() => _bookRepository.Update(A<Book>.That.Matches(b =>
                b.bookId == bookId &&
                b.title == newBook.title &&
                b.ISBN == newBook.ISBN &&
                b.description == newBook.description &&
                b.availableCopys == newBook.availableCopys &&
                b.ratingAVG == newBook.ratingAVG &&
                b.releaseDate == newBook.releaseDate &&
                b.floor == newBook.floor &&
                b.alley == newBook.alley &&
                b.rowNumber == newBook.rowNumber &&
                b.genre.genreId == genreId &&
                b.publisher.publisherId == publisherId &&
                b.type.typeId == bookTypeId &&
                b.authors.Count == 1 &&
                b.authors.First().author.authorId == 1 &&
                b.tags.Count == 1 &&
                b.tags.First().tag.tagId == 1
            ))).MustHaveHappenedOnceExactly();
        }
    }
}
