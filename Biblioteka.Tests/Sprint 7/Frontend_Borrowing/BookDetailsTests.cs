using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Views.Books;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace Biblioteka.Tests.Sprint_7.Frontend_Borrowing
{
    public class BookDetailsTests
    {
        private readonly TempDataDictionary tempData;
        private readonly UserManager<BibUser> _userManager;
        private IBookRepository _bookRepository;
        private IBookOpinionRepository _bookOpinionRepository;
        private IReaderRepository _readerRepository;
        private IAuthorRepository _authorRepository;
        private IEmployeeRepository _employeeRepository;
        private IBorrowingRepository _borrowingRepository;

        public BookDetailsTests()
        {
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
            _userManager = A.Fake<UserManager<BibUser>>();
            _bookRepository = A.Fake<IBookRepository>();
            _bookOpinionRepository = A.Fake<IBookOpinionRepository>();
            _readerRepository = A.Fake<IReaderRepository>();
            _authorRepository = A.Fake<IAuthorRepository>();
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _borrowingRepository = A.Fake<IBorrowingRepository>();
        }

        [Fact]
        public async Task OnPostAddBorrowing_Should_Add_Borrowing_And_Provide_Success_Message()
        {
            // Obiekt testowanej strony i inicjalizacja TempData
            var detailsModel = new DetailsModel(_userManager, _bookRepository, _bookOpinionRepository,
                                                _readerRepository, _authorRepository, _employeeRepository,
                                                _borrowingRepository)
            {
                TempData = tempData,
               Input = new DetailsModel.InputModel
               {
                   predictedEndDate = DateTime.Now.AddDays(2)
               }
            };

            var bookId = 1;
            var book = new Book { bookId = bookId, title = "Test Book", availableCopys = 5 };
            var reader = new Reader { readerId = 1, email="test@test.com" };
            var employee = new Employee { employeeId = 1, email = "test123@test123.com" };
            var user = new BibUser { Email = "test@example.com" };

            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>._)).Returns(user);
            A.CallTo(() => _readerRepository.GetByMail(user.Email)).Returns(reader);
            A.CallTo(() => _employeeRepository.GetAll()).Returns(new List<Employee> { employee });
            A.CallTo(() => _bookRepository.getOne(bookId)).Returns(book);

            // Act
            var result = await detailsModel.OnPostAddBorrowing(bookId) as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result?.PageName.Should().Be("../Borrowings/IndexReader");

            A.CallTo(() => _borrowingRepository.Add(A<Borrowing>.That.Matches(b =>
                b.book == book &&
                b.employee == employee &&
                b.readers.Any(rb => rb.reader == reader) &&
                b.plannedReturnDate == detailsModel.Input.predictedEndDate))).MustHaveHappenedOnceExactly();

            A.CallTo(() => _bookRepository.Update(A<Book>.That.Matches(b =>
                b.bookId == bookId &&
                b.availableCopys == 4))).MustHaveHappenedOnceExactly();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie dokonano wypożyczenia książki: \"{book.title}\" " +
                                $"w dniach {DateTime.Now.ToString("dd.MM.yyyy")} - {detailsModel.Input.predictedEndDate.ToString("dd.MM.yyyy")}.");
        }

        [Fact]
        public async Task OnPostAddBorrowing_Should_Add_Borrowing_And_Provide_Error_Message_Because_Of_Empty_Employee_List()
        {
            // Obiekt testowanej strony i inicjalizacja TempData
            var detailsModel = new DetailsModel(_userManager, _bookRepository, _bookOpinionRepository,
                                                _readerRepository, _authorRepository, _employeeRepository,
                                                _borrowingRepository)
            {
                TempData = tempData,
                Input = new DetailsModel.InputModel
                {
                    predictedEndDate = DateTime.Now.AddDays(2)
                }
            };

            var bookId = 1;
            var book = new Book { bookId = bookId, title = "Test Book", availableCopys = 5 };
            var reader = new Reader { readerId = 1, email = "test@test.com" };
            var employee = new Employee { employeeId = 1, email = "test123@test123.com" };
            var user = new BibUser { Email = "test@example.com" };

            A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>._)).Returns(user);
            A.CallTo(() => _readerRepository.GetByMail(user.Email)).Returns(reader);
            A.CallTo(() => _employeeRepository.GetAll()).Returns(new List<Employee>());
            A.CallTo(() => _bookRepository.getOne(bookId)).Returns(book);

            // Act
            var result = await detailsModel.OnPostAddBorrowing(bookId) as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./Details");

            A.CallTo(() => _borrowingRepository.Add(A<Borrowing>.That.Matches(b =>
                b.book == book &&
                b.employee == employee &&
                b.readers.Any(rb => rb.reader == reader) &&
                b.plannedReturnDate == detailsModel.Input.predictedEndDate))).MustNotHaveHappened();

            A.CallTo(() => _bookRepository.Update(A<Book>.That.Matches(b =>
                b.bookId == bookId &&
                b.availableCopys == 4))).MustNotHaveHappened();

            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Error/Brak pracownika mogącego obsłużyć wypożyczenie.");
        }
    }
}
