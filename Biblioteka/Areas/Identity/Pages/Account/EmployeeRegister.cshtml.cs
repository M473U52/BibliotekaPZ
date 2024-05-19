// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Biblioteka.Context;
using Biblioteka.Repositories;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Areas.Identity.Pages.Account.Validators;

namespace Biblioteka.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class EmployeeRegisterModel : PageModel
    {
        private readonly SignInManager<BibUser> _signInManager;
        private readonly UserManager<BibUser> _userManager;
        private readonly IUserStore<BibUser> _userStore;
        private readonly IUserEmailStore<BibUser> _emailStore;
        private readonly ILogger<EmployeeRegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;

        public List<SelectListItem>? position { get; set; }

        public EmployeeRegisterModel(
            UserManager<BibUser> userManager,
            IUserStore<BibUser> userStore,
            SignInManager<BibUser> signInManager,
            ILogger<EmployeeRegisterModel> logger,
            IEmailSender emailSender,
            IEmployeeRepository employeeRepository,
            IPositionRepository positionRepository
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;   
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 

       

        public class InputModel : IValidatableObject
        {
            [Required(ErrorMessage = "Imię jest wymagane."),
                Display(Name = "Imię"),
                StringLength(20, ErrorMessage = "Imię nie może zawierać więcej niż 20 znaków.")]
            public string name { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane."),
                Display(Name = "Nazwisko"),
                StringLength(40, ErrorMessage = "Nazwisko nie może zawierać więcej niż 40 znaków")]
            public string surname { get; set; }

            [Required(ErrorMessage = "Data urodzenia jest wymagana."),
                Display(Name = "Data urodzenia")]
            public DateTime? birthDate { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Email jest wymagany.")]
            [EmailAddress(ErrorMessage = "Podany email jest nieprawidłowy. Sprawdź, czy wpisujesz go zgodnie z formatem przyklad@email.com.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Hasło jest wymagane.")]
            [StringLength(100, ErrorMessage = "{0} Musi mieć przynajmniej {2} i nie więcej niż {1} znaków.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Potiwerdź hasło")]
            [Compare("Password", ErrorMessage = "Hasła nie są takie same.")]
            public string ConfirmPassword { get; set; }

            //Dane dla pracowników
			[Required(ErrorMessage = "PESEL jest wymagany."),
                BindProperty(SupportsGet = true),
				Display(Name = "PESEL"),
				Range(10000000000, 99999999999, ErrorMessage = "PESEL składa się z 11 cyfr")]
			public long? pesel { get; set; }

			[Required(ErrorMessage = "Ulica jest wymagana."),
                BindProperty(SupportsGet = true),
			    Display(Name = "Ulica"),
			    MaxLength(40, ErrorMessage = "Ulica nie może zaweirać więcej niż 40 znaków")]
			public string street { get; set; }

			[BindProperty(SupportsGet = true),
				Column(TypeName = "NUMERIC(3)"),
				Display(Name = "Nr mieszkania"),
				Range(0, 999, ErrorMessage = "Numer mieszkania nie może być większy niż 999")]
			public int? houseNumber { get; set; }

			[Required(ErrorMessage = "Miasto jest wymagane."),
                BindProperty(SupportsGet = true),
				Display(Name = "Miasto"),
				MaxLength(30, ErrorMessage = "Miasto nie może zawierać więcej niż 30 znaków")]
			public string town { get; set; }

			[Required(ErrorMessage = "Kod pocztowy jest wymagany."),
                BindProperty(SupportsGet = true),
				Display(Name = "Kod pocztowy"),
				MaxLength(6, ErrorMessage = "Kod pocztowy nie może zawierać więcej niż 6 znaków"),
                RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Nieprawidłowy kod pocztowy. Sprawdź, czy wpisujesz go zgodnie z formatem 99-999")]
            public string zipCode { get; set; }

			[Required(ErrorMessage = "Numer telefonu jest wymagany."),
                BindProperty(SupportsGet = true),
                Phone(ErrorMessage = "Nieprawidłowy format numeru telefonu"),
				Display(Name = "Nr telefonu")]
			public string phoneNumber { get; set; }

            [Required,
                BindProperty(SupportsGet = true)]
            public string positionId { get; set; } = default!;

            [DataType(DataType.Upload)]
            [Display(Name = "Zdjęcie profilowe")]
            [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "Maksymalny rozmiar pliku wynosi 10 MB.")]
            [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Dozwolone są tylko pliki w formacie JPG lub PNG.")]
            public IFormFile image { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (birthDate >= DateTime.Today)
                {
                    yield return new ValidationResult("Data urodzenia nie może być z przyszłości!", new[] { "BirthDate" });
                }
            }
        }


        public async Task OnGetAsync(string returnUrl = null)   
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            position = _positionRepository.getAll().Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.name = Input.name;
                user.surname = Input.surname;
                user.birthDate = Input.birthDate;
                if (Input.image != null)
                    if (Input.image.Length > 0 && Input.image.Length < 10000000 && (Path.GetExtension(Input.image.FileName) == ".jpg" || Path.GetExtension(Input.image.FileName) == ".png" || Path.GetExtension(Input.image.FileName) == ".jpeg"))
                    {
                        using (var ms = new MemoryStream())
                        {
                            Input.image.CopyTo(ms);
                            user.profilePicData = ms.ToArray();
                        }
                    }           
                
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");

                    await CreateEmployee(user);

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, code = code }, Request.Scheme);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    Log.ForContext("SaveToFile", "AnyValue").Information("Zarejestrowano pracownika " + user.name + " " + user.surname);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        TempData["Message"] = $"Success/Pomyślnie dodano nowego pracownika.";
                        return RedirectToPage("/Employees/Index", new { searchTerm = "" });
                    }       
                }
                else
                {
                    var errorMessages = result.Errors.Select(e => e.Description);
                    TempData["Message"] = $"Error/Wystąpił nieoczekiwany błąd podczas dodawania pracownika. {string.Join(",", errorMessages)}";
                    return RedirectToPage("/Employees/Index", new { searchTerm = "" });
                }
            }
            await OnGetAsync(returnUrl);
            return Page();
        }

        private BibUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<BibUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(BibUser)}'. " +
                    $"Ensure that '{nameof(BibUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<BibUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<BibUser>)_userStore;
        }

        private async Task CreateEmployee(BibUser user)
        {
            
            Employee employee = new Employee();
            employee.name = user.name;
            employee.surname = user.surname;
            employee.email = user.Email;
            employee.dateOfEmployment = DateTime.Now;

            if (Input.positionId != "")
            {
                Position? foundPosition = _positionRepository.getOne(int.Parse(Input.positionId));
                if (foundPosition != null)
                    employee.position = foundPosition;
            }
            EmployeeData employeeData = new EmployeeData();
            employeeData.pesel = Input.pesel;
            employeeData.street = Input.street;
            employeeData.houseNumber = Input.houseNumber;
            employeeData.town = Input.town;
            employeeData.zipCode = Input.zipCode;
            employeeData.phoneNumber = Input.phoneNumber;
            employee.employeeData = employeeData;


            await _employeeRepository?.Add(employee);
        }
    }
}
