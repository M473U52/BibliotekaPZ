using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.EmployeeExtraData
{
    //[Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IEmployeeDataRepository _employeeDataRepository;
        private IEmployeeRepository _employeeRepository;

        public EditModel(IEmployeeDataRepository employeeDataRepository,  IEmployeeRepository employeeRepository)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeRepository = employeeRepository;
        }


        [BindProperty]
        public EmployeeDataDto EmployeeData { get; set; } = new EmployeeDataDto();

        public IActionResult OnGet(int id)
        {
            var employeedata =  _employeeDataRepository.getOne(id);
            if (employeedata == null)
            {
                return NotFound();
            }

            EmployeeData = new EmployeeDataDto
            {
                employeeId = employeedata.employeeId,
                pesel = employeedata.pesel,
                street = employeedata.street,
                houseNumber = employeedata.houseNumber,
                town = employeedata.town,
                zipCode = employeedata.zipCode,
                phoneNumber = employeedata.phoneNumber,
                birthDate = employeedata.birthDate,
            };
            
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid || EmployeeData == null)
            {
                return Page();
            }
            else
            {
                var existingEmployeeData = _employeeDataRepository.getOne(EmployeeData.employeeId);
                if (existingEmployeeData == null)
                {
                    return NotFound();
                }

                existingEmployeeData.pesel = EmployeeData.pesel;
                existingEmployeeData.street = EmployeeData.street;
                existingEmployeeData.houseNumber = EmployeeData.houseNumber;
                existingEmployeeData.town = EmployeeData.town;
                existingEmployeeData.zipCode = EmployeeData.zipCode;
                existingEmployeeData.phoneNumber = EmployeeData.phoneNumber;
                existingEmployeeData.birthDate = EmployeeData.birthDate;

                EmployeeData.employeeId = id;
                Employee? employee = _employeeRepository.getOne(id);

                if (employee != null)
                    existingEmployeeData.employee = employee;
                _employeeDataRepository.Update(existingEmployeeData);
                return RedirectToPage("../Employees/Index");

            }
        }

        private bool EmployeeDataExists(int id)
        {
            var isExisted = _employeeDataRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
