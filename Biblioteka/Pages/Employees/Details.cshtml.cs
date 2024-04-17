using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Employees
{
    //[Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeDataRepository _employeeDataRepository;

        public DetailsModel(IEmployeeRepository employeeRepository, IEmployeeDataRepository employeeDataRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeDataRepository = employeeDataRepository;
        }

        public Employee Employee { get; set; } = default!;
        public EmployeeData EmployeeData { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var employee = _employeeRepository.getOne(id);
            var employeeData = _employeeDataRepository.getOne(id);  

            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;  
                EmployeeData = employeeData;    
            }
            return Page();
        }
    }
}
