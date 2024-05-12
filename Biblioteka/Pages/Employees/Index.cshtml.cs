using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Context;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories;

namespace Biblioteka.Pages.Employees
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private IUserRepository _userRepository;

        public IndexModel(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public IList<Employee> Employee { get; set; } = default!;
        public string SearchTerm { get; set; }

        public void OnGet(string searchTerm)
        {
            SearchTerm = searchTerm;
            Employee = _employeeRepository.SearchEmployees(SearchTerm);
        }

        public IActionResult OnPostDeleteEmployee(int employeeId)
        {
            var employee = _employeeRepository.getOne(employeeId);

            if (employee != null)
            {
                var user = _userRepository.GetOne(employee.name, employee.surname, employee.email);

                try
                {
                    _userRepository.Delete(user);
                    _employeeRepository.Delete(employeeId);
                    TempData["Message"] = $"Success/Pomyślnie usunięto pracownika {employee.name} {employee.surname}" +
                    $" zatrudnionego dnia {employee.dateOfEmployment.ToString("dd.MM.yyyy")}.";
                }
                catch (Exception)
                {
                    TempData["Message"] = $"Error/Konto wybranego pracownika nie istnieje.";
                }                
            }
            else
            {
                TempData["Message"] = $"Error/Brak pracownika o takim ID.";
            }
            return RedirectToPage("./Index", new { searchTerm = "" });
        }
    }
}
