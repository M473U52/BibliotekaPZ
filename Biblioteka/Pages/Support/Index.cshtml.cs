using Biblioteka.Repositories.Interfaces;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.Support
{
    public class IndexModel : PageModel
    {
        public readonly IEmailSender _emailSender;
        private IEmployeeRepository _employeeRepository;

        public IndexModel(IEmailSender emailSender, IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
            _emailSender = emailSender;
        }

        public List<string> employeeEmails { get; set; } = new List<string>();

        // public string message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string subject, string message)
        {
            if (!ModelState.IsValid)
                return Page();

			var employees = _employeeRepository.GetAll();
			foreach (var employee in employees)
			{
				employeeEmails.Add(employee.email);
			}

			var random = new Random();
            int id = random.Next(employeeEmails.Count);
            string employeeEmail = employeeEmails[id];

            employeeEmail = "dimitriy.treysin@moongit.com";

            await _emailSender.SendEmailAsync(employeeEmail, subject, message);

			TempData["Message"] = "Wiadomoœæ wys³ana pomyœlnie";
			return RedirectToPage("Index");
        }
    }
}
