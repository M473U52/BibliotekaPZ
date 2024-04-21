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
    public class IndexAdminTests
    {
        private readonly IBorrowingRepository borrowingRepository;
        private readonly IBookRepository bookRepository;
        private readonly IReaderRepository readerRepository;
        private readonly IReader_BorrowingsRepository readerBorrowingsRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly TempDataDictionary tempData;

        public IndexAdminTests()
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


        [Fact]
        public void OnPostDeleteBorrowing_Shouldnt_Delete_Borrowing_Provide_Correct_Error_Message_BecauseOfInvalidBorrowId()
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

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane - brak wypożyczenia o danym ID
            A.CallTo(() => borrowingRepository.GetOne(borrowingId)).Returns(null);

            // Act
            var result = indexAdminModel.OnPostDeleteBorrowing(borrowingId) as RedirectToPageResult;

            // Assert
            // Sprawdzenie przekierowania, meotdy repo nie powinny się wywołać
            // i ilości książek po zakończonej operacji powinna być taka sama
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./IndexAdmin");
            A.CallTo(() => borrowingRepository.Delete(borrowingId)).MustNotHaveHappened();
            A.CallTo(() => bookRepository.Update(borrowing.book)).MustNotHaveHappened();
            borrowing.book.availableCopys.Should().Be(0);

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Error/Brak wypożyczenia o takim ID.");
        }

        [Fact]
        public void OnPostConfirmBorrowing_Should_Confirm_Borrowing_Update_Book_Availability_And_Provide_Correct_Success_Message()
        {
            // Tworzenie fałszywego Usera
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "employee@employee.com")
            }, "mock"));

            // Przypisanie usera do PageContext
            var pageContext = new PageContext { HttpContext = new DefaultHttpContext { User = user } };

            // Obiekt testowanej strony i inicjalizacja TempData
            var indexAdminModel = new IndexAdminModel(borrowingRepository, readerRepository,
                                                      readerBorrowingsRepository, employeeRepository, bookRepository)
            {
                TempData = tempData,
                PageContext = pageContext
            };

            indexAdminModel.SearchTerm = "";

            // Wywołanie onGet
            indexAdminModel.OnGet();

            // Tworzenie obiektu wypożyczenia i pracownika
            var borrowingId = 1;
            var borrowing = new Borrowing { borrowId = borrowingId, book = new Book { bookId = 1, title = "Nowa ksiażka", availableCopys = 1 }, borrowDate = DateTime.Now, Confirmation=false };
            var employee = new Employee { email = "employee@employee.com" };

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane
            A.CallTo(() => borrowingRepository.GetOne(borrowingId)).Returns(borrowing);
            A.CallTo(() => employeeRepository.GetByMail(employee.email)).Returns(employee);

            // Act
            var result = indexAdminModel.OnPostConfirmBorrowing(borrowingId) as RedirectToPageResult;

            // Assert
            // Sprawdzenie przekierowania, wywołania metody repo, wartości zmiennej bool - potwierdzenie wypożyczenia
            // i ilości książek po zakończonej operacji potwierdzenia wypożyczenia - była 1 po wypożyczeniu powinno być 0
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./IndexAdmin");
            A.CallTo(() => borrowingRepository.Update(borrowing)).MustHaveHappened();
            borrowing.Confirmation.Should().BeTrue();
            borrowing.book.availableCopys.Should().Be(0);

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie potwierdzono odbiór ksiązki \"{borrowing.book.title}\"");
        }

        [Fact]
        public void OnPostConfirmReturn_Should_Confirm_Return_Update_Book_Availability_And_Provide_Correct_Success_Message()
        {
            // Tworzenie fałszywego Usera
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "employee@employee.com")
            }, "mock"));

            // Przypisanie usera do PageContext
            var pageContext = new PageContext { HttpContext = new DefaultHttpContext { User = user } };

            // Obiekt testowanej strony i inicjalizacja TempData
            var indexAdminModel = new IndexAdminModel(borrowingRepository, readerRepository,
                                                      readerBorrowingsRepository, employeeRepository, bookRepository)
            {
                TempData = tempData,
                PageContext = pageContext
            };

            indexAdminModel.SearchTerm = "";

            // Wywołanie onGet
            indexAdminModel.OnGet();

            // Tworzenie obiektu wypożyczenia i pracownika
            var borrowingId = 1;
            var borrowing = new Borrowing { borrowId = borrowingId, book = new Book { bookId = 1, title = "Nowa ksiażka", availableCopys = 0 }, borrowDate = DateTime.Now, IsReturned=false };
            var employee = new Employee { email = "employee@example.com" };

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane
            A.CallTo(() => borrowingRepository.GetOne(borrowingId)).Returns(borrowing);
            A.CallTo(() => employeeRepository.GetByMail(employee.email)).Returns(employee);

            // Act
            var result = indexAdminModel.OnPostConfirmReturn(borrowingId) as RedirectToPageResult;

            // Assert
            // Sprawdzenie przekierowania, wywołania metody repo, wartości zmiennej bool - potwierdzenie zwrotu książki
            // i ilości książek po zakończonej operacji potwierdzenia zwrotu - było 0 po zwrocie powinna być 1
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./IndexAdmin");
            A.CallTo(() => borrowingRepository.CalculateLateFee(borrowingId)).MustHaveHappened();
            A.CallTo(() => borrowingRepository.AddECR(borrowing)).MustHaveHappened();
            A.CallTo(() => borrowingRepository.Update(borrowing)).MustHaveHappened();
            borrowing.IsReturned.Should().BeTrue();
            borrowing.book.availableCopys.Should().Be(1);

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie potwierdzono zwrot ksiązki \"{borrowing.book.title}\".");
        }
        [Fact]
        public void OnPostConfirmPayment_Should_Confirm_Payment_And_Provide_Correct_Success_Message()
        {
            // Tworzenie fałszywego Usera
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "employee@employee.com")
            }, "mock"));

            // Przypisanie usera do PageContext
            var pageContext = new PageContext { HttpContext = new DefaultHttpContext { User = user } };

            // Obiekt testowanej strony i inicjalizacja TempData
            var indexAdminModel = new IndexAdminModel(borrowingRepository, readerRepository,
                                                      readerBorrowingsRepository, employeeRepository, bookRepository)
            {
                TempData = tempData,
                PageContext = pageContext
            };

            indexAdminModel.SearchTerm = "";

            // Wywołanie onGet
            indexAdminModel.OnGet();

            var borrowingId = 1;
            var borrowing = new Borrowing { borrowId = borrowingId, book = new Book { bookId = 1 }, IsPaid=false };
            var employee = new Employee { email = "employee@example.com" };

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane
            A.CallTo(() => borrowingRepository.GetOne(borrowingId)).Returns(borrowing);
            A.CallTo(() => employeeRepository.GetByMail(employee.email)).Returns(employee);

            // Act
            var result = indexAdminModel.OnPostConfirmPayment(borrowingId) as RedirectToPageResult;          

            // Assert
            // Sprawdzenie przekierowania, wywołania metod repo, wartości zmiennej bool - czy opłacona
            // i czy przypisnano jakiegoś pracownika który zatwierdzil płatność
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./IndexAdmin");
            A.CallTo(() => borrowingRepository.AddECP(borrowing)).MustHaveHappened();
            A.CallTo(() => borrowingRepository.Update(borrowing)).MustHaveHappened();
            borrowing.IsPaid.Should().BeTrue();
            borrowing.employeeConfirmingPayment.Should().NotBeNull();

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie potwierdzono opłatę za książkę \"{borrowing.book.title}\".");
        }
    }
}
