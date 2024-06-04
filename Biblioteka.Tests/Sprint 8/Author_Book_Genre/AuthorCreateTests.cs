using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Pages.Authors;
using FluentAssertions;

namespace Biblioteka.Tests.Sprint_8.Author_Book_Genre
{
    public class AuthorCreateTests
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly TempDataDictionary tempData;

        public AuthorCreateTests()
        {
            _authorRepository = A.Fake<IAuthorRepository>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }

        [Fact]
        public void OnPostCreateAuthor_Should_Not_Create_Author_And_Provide_Error_Message_Because_Of_Invalid_Image()
        {
            var createModel = new CreateModel(_authorRepository)
            {
                TempData = tempData
            };

            // Arrange
            var fileMock = A.Fake<IFormFile>();
            A.CallTo(() => fileMock.Length).Returns(11000000); // Invalid size
            A.CallTo(() => fileMock.FileName).Returns("image.png"); // Invalid extension

            var author = new Author { image = fileMock };
            createModel.Author = author;

            // Act
            var result = createModel.OnPost() as RedirectToPageResult;

            // Assert
            A.CallTo(() => _authorRepository.Add(A<Author>.That.Matches(a =>
                a.image == fileMock))).MustNotHaveHappened();

            result.Should().NotBeNull();
            result?.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>(); 
            
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Error/Plik z zdjęciem musi być w formacie JPG i nie większy niż 10MB!");
        }

        [Fact]
        public void OnPostCreateAuthor_Should_Create_Author_And_Provide_Correct_Success_Message()
        {

            var createModel = new CreateModel(_authorRepository)
            {
                TempData = tempData
            };
            var author_name = "Author_Name";
            var author_surname = "Author_Surname";
            var author = new Author { name = author_name, surname=author_surname };
            createModel.Author = author;

            var result = createModel.OnPost() as RedirectToPageResult;

            // Assert
            A.CallTo(() => _authorRepository.Add(A<Author>.That.Matches(a =>
                a.name == author_name && a.surname == author_surname))).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result?.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie dodano nowego autora.");
        }
    }
}
