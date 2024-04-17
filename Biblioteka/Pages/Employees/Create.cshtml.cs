using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Employees
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private IPositionRepository _positionRepository;


        public CreateModel(IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public List<SelectListItem>? Positions { get; set; }

        /*[BindProperty]
        public string PositionId { get; set; } = default!;*/


        public IActionResult OnGet()
        {
            Positions = _positionRepository.getAll().Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            Position? foundPosition = _positionRepository.getOne(Employee.positionId);

            if(foundPosition != null && Employee != null)
                Employee.position = foundPosition;
            else
                ModelState.AddModelError("", "Stanowisko jest wymagane.");

            //ModelState.Remove("Employee.position");
            //ModelState.Remove("Employee.employeeData");

            if (Employee == null || !ModelState.IsValid)
            {
                 Positions = _positionRepository.getAll().Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();
                 return Page();
            }    

            _employeeRepository.Add(Employee);
            
            return RedirectToPage("./Index");
        }
    }
}
