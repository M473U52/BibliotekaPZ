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

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
                TempData["Message"] = $"Success/Pomyślnie usunięto pracownika {employee.name} {employee.surname}" +
                                    $" zatrudnionego dnia {employee.dateOfEmployment.ToString("dd.MM.yyyy")}.";

                _employeeRepository.Delete(employeeId);
            }
            else
            {
                TempData["Message"] = $"Error/Brak pracownika o takim ID.";
            }
            return RedirectToPage("./Index", new { searchTerm = "" });
        }
    }
}
