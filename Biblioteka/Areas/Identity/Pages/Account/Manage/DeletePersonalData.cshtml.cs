// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private readonly SignInManager<BibUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeDataRepository _employeeDataRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IRoomReservationRepository _roomReservationRepository;
        private readonly IBookOpinionRepository _bookOpinionRepository;

        public DeletePersonalDataModel(
            UserManager<BibUser> userManager,
            SignInManager<BibUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IEmployeeRepository employeeRepository,
            IEmployeeDataRepository employeeDataRepository,
            IReaderRepository readerRepository,
            IBorrowingRepository borrowingRepository,
            IRoomReservationRepository roomReservationRepository,
            IBookOpinionRepository bookOpinionRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _employeeRepository = employeeRepository;
            _employeeDataRepository = employeeDataRepository;
            _readerRepository = readerRepository;
            _borrowingRepository = borrowingRepository;
            _roomReservationRepository = roomReservationRepository;
            _bookOpinionRepository = bookOpinionRepository;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name ="Hasło")]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            deleteEmployee();
            deleteReader();

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        private void deleteReader()
        {
            Reader reader = _readerRepository.GetByMail(User.Identity.Name);

            if (reader !=  null)
            {
                foreach (Reader_Borrowings borrowing in reader.borrowings)
                {
                    _borrowingRepository.Delete(borrowing.borrow.borrowId);
                }
                foreach (RoomReservation reservation in reader.reservations)
                {
                    _roomReservationRepository.Delete(reservation.reservationId);
                }
                foreach (Book_Opinions opinion in reader.bookOpinions)
                {
                    _bookOpinionRepository.DeleteOpinion(opinion);
                }
                _readerRepository.Delete(reader.readerId);
            }
        }
        private void deleteEmployee()
        {
            Employee employee = _employeeRepository.GetByMail(User.Identity.Name);
            
            if (employee != null)
            {
                _employeeDataRepository.Delete(employee.employeeId);
                _employeeRepository.Delete(employee.employeeId);
            }
        }

    }
}
