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

        [Fact]
        public async Task OnPostChangePhotoAsync_Should_Change_User_Photo_And_Provide_Success_Message()
        {
            // Arrange - user, image, page
            var user = new BibUser();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(new string('a', (int)1000000));
            writer.Flush();
            stream.Position = 0;
            var userImage = new FormFile(stream, 0, 1000000, "image", "fakeImage.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            var userImageData = await GetImageDataAsync(userImage);
            var model = new IndexModel(userManager, signInManager, readerRepository, employeeRepository, emailSender)
            {
                TempData = tempData,
                image = userImage
            };

            A.CallTo(() => userManager.GetUserAsync(A<ClaimsPrincipal>._)).Returns(user);
            A.CallTo(() => userManager.UpdateAsync(user)).Returns(IdentityResult.Success);

            // Act
            var result = await model.OnPostChangePhotoAsync() as RedirectToPageResult;

            // Assert
            // Czy przekierowano spowrotem do /Account/Manage/Index ?
            result.Should().NotBeNull();
            result?.PageName.Should().Be("/Account/Manage/Index");

            // Czy rzeczywiście zmieniono poprawnie zdjęcie profilowe ?
            user.profilePicData.Should().NotBeNull();
            user.profilePicData.Should().Equal(userImageData);

            // Czy została dostarczona wiadomość z sukcesem operacji ?
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            model.TempData["Message"].Should().Be("Success/Pomyślnie zmieniono zdjęcie profilowe.");
        }

        [Fact]
        public async Task OnPostChangePhotoAsync_Should_Not_Change_User_Photo_And_Provide_Error_Message()
        {
            // Arrange - user, image, page
            var user = new BibUser();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(new string('a', (int)10000001)); // więcej niż 10 MB
            writer.Flush();
            stream.Position = 0;
            var userImage = new FormFile(stream, 0, 10000001, "image", "fakeImage.gif")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/gif"  // Format gif
            };
            var userImageData = await GetImageDataAsync(userImage);
            var model = new IndexModel(userManager, signInManager, readerRepository, employeeRepository, emailSender)
            {
                TempData = tempData,
                image = userImage
            };

            // Act
            var result = await model.OnPostChangePhotoAsync() as RedirectToPageResult;

            // Assert
            // Czy przekierowano spowrotem do /Account/Manage/Index ?
            result.Should().NotBeNull();
            result?.PageName.Should().Be("/Account/Manage/Index");

            // Czy zdjęcie profilowe nie zostało zmienione - nadal jest null ?
            user.profilePicData.Should().BeNull();

            // Czy funkcje wewnątrz zostały wykonane lub nie ?
            A.CallTo(() => userManager.GetUserAsync(A<ClaimsPrincipal>._)).MustHaveHappened();
            A.CallTo(() => userManager.UpdateAsync(user)).MustNotHaveHappened();

            // Czy została dostarczona wiadomość z sukcesem operacji ?
            tempData.ContainsKey("Message").Should().BeTrue();
            tempData["Message"].Should().NotBeNull();
            model.TempData["Message"].Should().Be("Error/Plik musi być w formacie JPG i nie większy niż 10MB!");
        }
        private async Task<byte[]> GetImageDataAsync(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

    }
}
