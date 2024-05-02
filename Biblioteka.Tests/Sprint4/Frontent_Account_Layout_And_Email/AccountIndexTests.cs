using Biblioteka.Areas.Identity.Data;
using Biblioteka.Areas.Identity.Pages.Account.Manage;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data;
using System.Security.Claims;

namespace Biblioteka.Tests.Sprint4.Frontent_Account_Layout_And_Email
{
    public class AccountIndexTests
    {
        private readonly TempDataDictionary tempData;
        private readonly UserManager<BibUser> userManager;
        private readonly SignInManager<BibUser> signInManager;
        private readonly IReaderRepository readerRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountIndexTests()
        {
            // Arrange - mockup repo i serwisów
            userManager = A.Fake<UserManager<BibUser>>();
            signInManager = A.Fake<SignInManager<BibUser>>();
            readerRepository = A.Fake<IReaderRepository>();
            employeeRepository = A.Fake<IEmployeeRepository>();
            emailSender = A.Fake<IEmailSender>();
            httpContextAccessor = A.Fake<IHttpContextAccessor>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
        }
        private ClaimsPrincipal CreateUser(string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"), // Możesz zmienić Id użytkownika według potrzeb
                new Claim(ClaimTypes.Name, "example@example.com") // Możesz zmienić nazwę użytkownika według potrzeb
            };

            // Dodaj role do listy claimów
            claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, "TestAuthentication");

            return new ClaimsPrincipal(identity);
        }

        /*[Fact]
        public async Task OnPostChangeEmailAsync_Should_Change_Email_And_Provide_Correct_Success_Message()
        {
            var model = new IndexModel(userManager, signInManager, readerRepository, employeeRepository, emailSender)
            {
                TempData = tempData
            };
            model.NewEmail = "new.mail@example.com";
            model.RepeatNewEmail = "new.mail@example.com";

            var user = new BibUser { Id = "1", UserName = "old.mail@example.com", Email = "old.mail@example.com" };
            var role = "Employee";
            var userWithRole = CreateUser(role);
            A.CallTo(() => userManager.GetUserAsync(userWithRole)).Returns(user);
            A.CallTo(() => userManager.GetEmailAsync(user)).Returns(user.Email);
            A.CallTo(() => userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail)).Returns("token");
            A.CallTo(() => userManager.GetUserIdAsync(user)).Returns(user.Id);
            A.CallTo(() => userManager.UpdateAsync(user)).Returns(IdentityResult.Success);
            A.CallTo(() => readerRepository.GetByMail(user.Email)).Returns(null);
            A.CallTo(() => employeeRepository.GetOneByMail(user.Email)).Returns(null);

            // Act
            var result = await model.OnPostChangeEmailAsync() as RedirectToPageResult;

            // Assert
            result.Should().NotBeNull();
            result?.PageName.Should().Be("/Account/Manage/Index");

            A.CallTo(() => userManager.UpdateAsync(user));

            // Sprawdzenie wiadomości uzyskanej w TempData
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            model.TempData["Message"].Should().Be("Success/Link potwierdzający został wysłany na podany adres e-mail. Sprawdź swoją skrzynkę.");

            
            A.CallTo(() => emailSender.SendEmailAsync("new.mail@example.com", A<string>._, A<string>._)).MustHaveHappened();

            // If user is in Employee role
            A.CallTo(() => employeeRepository.Update(A<Employee>._)).WhenArgumentsMatch(args => args.Get<Employee>(0).email == model.NewEmail).MustHaveHappened();

            // If user is in Reader role
            A.CallTo(() => readerRepository.Update(A<Reader>._)).WhenArgumentsMatch(args => args.Get<Reader>(0).email == model.NewEmail).MustHaveHappened();

            // Check if user has new email
            user.Email.Should().Be(model.NewEmail);
            

           
        }*/

    }
}
