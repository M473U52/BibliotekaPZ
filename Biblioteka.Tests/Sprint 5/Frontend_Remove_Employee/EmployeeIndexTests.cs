using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Pages.Employees;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Biblioteka.Tests.Sprint_5.Frontend_Add_Remove_Employee
{
    public class EmployeeIndexTests
    {
        private readonly TempDataDictionary tempData;
        private readonly IEmployeeRepository fakeEmployeeRepository;
        private readonly IUserRepository fakeUserRepository;

        public EmployeeIndexTests()
        {
            fakeEmployeeRepository = A.Fake<IEmployeeRepository>();
            fakeUserRepository = A.Fake<IUserRepository>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }


        [Fact]
        public void OnPostDeleteEmployee_Should_Delete_Emploee_And_Provide_Correct_Success_Message()
        {
            // Obiekt testowanej strony i inicjalizacja TempData
            var indexModel = new IndexModel(fakeEmployeeRepository, fakeUserRepository)
            {
                TempData = tempData
            };

            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet("");

            // Tworzenie obiektu pracownika
            var employeeId = 1;
            var employee = new Employee
            {
                employeeId = employeeId,
                name = "John",
                surname = "Doe",
                email = "john@example.com",
                dateOfEmployment = DateTime.Now
            };
            BibUser user = new BibUser();

            // Fake'owe wywołanie metod repo aby zwróciły pożądane dane
            A.CallTo(() => fakeEmployeeRepository.getOne(employeeId)).Returns(employee);
            A.CallTo(() => fakeUserRepository.GetOne(employee.name, employee.surname, employee.email)).Returns(user);

            // Act
            var result = indexModel.OnPostDeleteEmployee(employeeId) as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./Index");

            A.CallTo(() => fakeUserRepository.Delete(user)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEmployeeRepository.Delete(employeeId)).MustHaveHappenedOnceExactly();

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Success/Pomyślnie usunięto pracownika {employee.name} {employee.surname}" +
                    $" zatrudnionego dnia {employee.dateOfEmployment.ToString("dd.MM.yyyy")}.");
        }

        [Fact]
        public void OnPostDeleteEmployee_Should_Not_Delete_Employee_Cause_Of_Bad_ID_And_Provide_Correct_Error_Message()
        {
            // Obiekt testowanej strony i inicjalizacja TempData
            var indexModel = new IndexModel(fakeEmployeeRepository, fakeUserRepository)
            {
                TempData = tempData
            };

            indexModel.SearchTerm = "";

            // Wywołanie onGet
            indexModel.OnGet("");

            var employeeId = 1;
            A.CallTo(() => fakeEmployeeRepository.getOne(employeeId)).Returns(null);

            // Act
            var result = indexModel.OnPostDeleteEmployee(employeeId) as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result?.PageName.Should().Be("./Index");

            A.CallTo(() => fakeUserRepository.Delete(A<BibUser>._)).MustNotHaveHappened();
            A.CallTo(() => fakeEmployeeRepository.Delete(A<int>._)).MustNotHaveHappened();

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            tempData["Message"].Should().Be($"Error/Brak pracownika o takim ID.");
        }
    }
}
