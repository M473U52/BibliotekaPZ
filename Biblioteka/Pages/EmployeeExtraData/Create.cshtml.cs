using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.EmployeeExtraData
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IEmployeeDataRepository _employeeDataRepository;
        private IEmployeeRepository _employeeRepository;

        public CreateModel(IEmployeeDataRepository employeeDataRepository, IEmployeeRepository employeeRepository)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EmployeeDataDto EmployeeData { get; set; } = default!;
        
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid || EmployeeData == null)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var employeedata = EmployeeData;
                        Console.WriteLine($"Model error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            else
            {
                var employeedata = EmployeeData;
                EmployeeData newEmployeeData = new EmployeeData()
                {
                    pesel = EmployeeData.pesel,
                    street = EmployeeData.street,
                    houseNumber = EmployeeData.houseNumber,
                    town = EmployeeData.town,
                    zipCode = EmployeeData.zipCode,
                    phoneNumber = EmployeeData.phoneNumber,
                    birthDate = EmployeeData.birthDate,
                };
                EmployeeData.employeeId = id;
                Employee? employee = _employeeRepository.getOne(id);

                if (EmployeeData != null && employee != null)
                    newEmployeeData.employee = employee;

                _employeeDataRepository.Add(newEmployeeData);

                return RedirectToPage("../Employees/Index");
            }
        }
    }
}
