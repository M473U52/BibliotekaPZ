// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Services;
using System.Security.Claims;

namespace Biblioteka.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private readonly SignInManager<BibUser> _signInManager;
        private readonly IReaderRepository _readerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailSender _emailSender;


        public IndexModel(
            UserManager<BibUser> userManager,
            SignInManager<BibUser> signInManager, 
            IReaderRepository readerRepository,
            IEmployeeRepository employeeRepository,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _readerRepository = readerRepository;
            _employeeRepository = employeeRepository;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        [BindProperty]
        public string Username { get; set; } = default!;


        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Nowy e-mail")]
        public string NewEmail { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Potwierdź nowy e-mail")]
        public string RepeatNewEmail { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public IFormFile image { get; set; }

        [BindProperty]
        public Reader reader { get; set; } = default!;

        [BindProperty]
        public Employee employee { get; set; } = default!;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = $"Error/Nie można załadować użytkownika.";
                return RedirectToPage("/Account/Manage/Index");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (NewEmail != null && NewEmail != email)
            {
                if(NewEmail != RepeatNewEmail)
                {
                    TempData["Message"] = $"Error/Nowo podane e-maile nie sa identyczne.";
                    return RedirectToPage("/Account/Manage/Index");
                }
                user.UserName = NewEmail;
                var oldEmail = user.Email;
                user.Email = NewEmail;
                user.EmailConfirmed = false;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    TempData["Message"] = $"Error/Wystąpił nieoczekiwany błąd podczas aktualizacji użytkownika.";
                    return RedirectToPage("/Account/Manage/Index");
                }
                if(User.IsInRole("Employee") && !User.IsInRole("Reader"))
                {
                    var employee = _employeeRepository.GetOneByMail(oldEmail);
                    if(employee == null) 
                    {
                        TempData["Message"] = $"Error/Wystąpił błąd przy aktualizacji danych.";
                        return RedirectToPage("/Account/Manage/Index");
                    }
                    employee.email = NewEmail;
                    _employeeRepository.Update(employee);
                }
                else
                {
                    var reader = _readerRepository.GetByMail(oldEmail);
                    if (reader == null)
                    {
                        TempData["Message"] = $"Error/Wystąpił błąd przy aktualizacji danych.";
                        return RedirectToPage("/Account/Manage/Index");
                    }
                    reader.email = NewEmail;
                    _readerRepository.Update(reader);
                }
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    NewEmail,
                    "Potwierdź swój e-mail",
                    $"Potwierdź swój adres e-mail klikając w link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>.");

                TempData["Message"] = $"Success/Link potwierdzający został wysłany na podany adres e-mail. Sprawdź swoją skrzynkę.";
                return RedirectToPage("/Account/Manage/Index");
            }

            TempData["Message"] = $"Error/E-mail nie został zmieniony poprawnie.";
            return RedirectToPage("/Account/Manage/Index");
        }

        public async Task<IActionResult> OnPostResendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = $"Error/Nie można załadować użytkownika.";
                return RedirectToPage("/Account/Manage/Index");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                    email,
                    "Potwierdź swój e-mail",
                    $"Potwierdź swój adres e-mail klikając w link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>.");

            TempData["Message"] = $"Success/Link potwierdzający został wysłany na podany adres e-mail. Sprawdź swoją skrzynkę.";
            return RedirectToPage("/Account/Manage/Index");
        }

        public async Task<IActionResult> OnPostChangePhotoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = $"Error/Nie można załadować użytkownika.";
                return RedirectToPage("/Account/Manage/Index");
            }

            if (image != null)
            {
                if (image.Length > 0 && image.Length < 10000000 && (Path.GetExtension(image.FileName) == ".jpg" || Path.GetExtension(image.FileName) == ".png"))
                {
                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);
                        user.profilePicData = ms.ToArray();
                    }
                }
                else
                {
                    TempData["Message"] = $"Error/Plik musi być w formacie JPG i nie większy niż 10MB!";
                    return RedirectToPage("/Account/Manage/Index");
                }
            }
            else
            {
                TempData["Message"] = $"Error/Nie wybrano pliku.";
                return RedirectToPage("/Account/Manage/Index");
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                TempData["Message"] = $"Error/Wystąpił nieoczekiwany błąd podczas aktualizacji użytkownika.";
                return RedirectToPage("/Account/Manage/Index");
            }
            TempData["Message"] = $"Success/Pomyślnie zmieniono zdjęcie profilowe.";
            return RedirectToPage("/Account/Manage/Index");
        }

        private async Task LoadAsync(BibUser user)
        {
            int a = 1;
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var isReader = await _userManager.IsInRoleAsync(user, "Reader");
            var isEmployee = await _userManager.IsInRoleAsync(user, "Employee");
            if (isReader&&!isEmployee)
            {
                reader = _readerRepository.GetByMail(userName);
            }
            else if(isEmployee)
            {
                employee = _employeeRepository.GetOneByMail(userName);
            }
            else
            {
                // admin nie ma danych
            }
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
