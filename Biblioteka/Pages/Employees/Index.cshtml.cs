using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

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
    }
}
