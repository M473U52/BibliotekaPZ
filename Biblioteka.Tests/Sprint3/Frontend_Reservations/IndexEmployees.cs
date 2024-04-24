using Biblioteka.Models;
using Biblioteka.Pages.Borrowings;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using System.Security.Principal;

namespace Biblioteka.Tests.Sprint3.Frontend_Borrowing
{
    public class IndexEmployeesTests
    {
        private readonly IBorrowingRepository borrowingRepository;
        private readonly IBookRepository bookRepository;
        private readonly IReaderRepository readerRepository;
        private readonly IReader_BorrowingsRepository readerBorrowingsRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly TempDataDictionary tempData;

        public IndexEmployeesTests()
        {
            // Arrange - mockup interfejsów repo
            borrowingRepository = A.Fake<IBorrowingRepository>();
            bookRepository = A.Fake<IBookRepository>();
            readerRepository = A.Fake<IReaderRepository>();
            readerBorrowingsRepository = A.Fake<IReader_BorrowingsRepository>();
            employeeRepository = A.Fake<IEmployeeRepository>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }

        [Fact]
        public void OnPostDeleteBorrowing_Should_Delete_Borrowing_Update_Book_Availability_And_Provide_Correct_Succcess_Message()
        {
            // Obiekt testowanej strony i inicjalizacja TempData
            var indexAdminModel = new IndexAdminModel(borrowingRepository, readerRepository,
                                                      readerBorrowingsRepository, employeeRepository, bookRepository)
            {
                TempData = tempData
            };

            indexAdminModel.SearchTerm = "";

            // Wywołanie onGet
            indexAdminModel.OnGet();

            // Tworzenie obiektu wypożyczenia
            var borrowingId = 1;
            var borrowing = new Borrowing { borrowId = borrowingId, book = new Book { bookId = 1, availableCopys = 0 }, borrowDate = DateTime.Now };

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane
            A.CallTo(() => borrowingRepository.GetOne(borrowingId)).Returns(borrowing);
            A.CallTo(() => bookRepository.getOne(borrowing.book.bookId)).Returns(borrowing.book);

            // Act
            var result = indexAdminModel.OnPostDeleteBorrowing(borrowingId) as RedirectToPageResult;

            // Assert
            // Sprawdzenie przekierowania, wywołania metod repo
            // i ilości książek po zakończonej operacji usuwania wypożyczenia - było 0 po usunieciu wypożyczenia powinno być 1 
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./IndexAdmin");
            A.CallTo(() => borrowingRepository.Delete(borrowingId)).MustHaveHappened();
            A.CallTo(() => bookRepository.Update(borrowing.book)).MustHaveHappened();
            borrowing.book.availableCopys.Should().Be(1);

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie usunięto wypożyczenie książki \"{borrowing.book.title}\" z dnia {borrowing.borrowDate.ToString("dd.MM.yyyy")}.");
        }
    }
}