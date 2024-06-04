using Biblioteka.Models;
using Biblioteka.Pages.Genres;
using Biblioteka.Repositories;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Tests.Sprint_8.Author_Book_Genre
{
    public class GenreAddEditDeleteTests
    {
        private IGenreRepository _genreRepository;
        private readonly TempDataDictionary tempData;
        public GenreAddEditDeleteTests()
        {
            _genreRepository = A.Fake<IGenreRepository>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }
        [Fact]
        public void Index_OnPostDeleteGenre_Should_Delete_Genre_And_Provide_Success_Message()
        {
            // Arrange
            var genreId = 1;
            var genre = new Genre { genreId = genreId, name = "Fiction" };

            A.CallTo(() => _genreRepository.getOne(genreId)).Returns(genre);

            var deleteModel = new IndexModel(_genreRepository)
            {
                TempData = tempData
            };

            // Act
            var result = deleteModel.OnPostDeleteGenre(genreId) as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie usunięto gatunek: \"{genre.name}\" ");

            A.CallTo(() => _genreRepository.Delete(genreId)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void Edit_OnPost_Should_Edit_Genre_And_Provide_Success_Message()
        {
            // Arrange
            var genre = new Genre { genreId = 1, name = "New Genre" };

            var editModel = new EditModel(_genreRepository)
            {
                TempData = tempData,
                Genre = genre
            };

            A.CallTo(() => _genreRepository.searchGenre(genre.name)).Returns(null);

            // Act
            var result = editModel.OnPost() as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie zmodyfikowano gatunek: {genre.name}.");

            A.CallTo(() => _genreRepository.Update(genre)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void Create_OnPost_Should_Create_Genre_And_Provide_Success_Message()
        {
            var genre = new Genre { genreId = 1, name = "New Genre" };

            var createModel = new CreateModel(_genreRepository)
            {
                TempData = tempData,
                Genre = genre
            };

            A.CallTo(() => _genreRepository.searchGenre(genre.name)).Returns(null);

            // Act
            var result = createModel.OnPost() as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result.PageName.Should().Be("./Index");
            result.Should().BeOfType<RedirectToPageResult>();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie dodano gatunek: {genre.name}.");

            A.CallTo(() => _genreRepository.Add(genre)).MustHaveHappenedOnceExactly();
        }
    }
}
