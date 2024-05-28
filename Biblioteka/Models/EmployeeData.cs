using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
	public class EmployeeData
	{
		[Column(TypeName = "NUMERIC(2)"),
			Required,
			Display(Name = "Id pracownika"),
			Range(0, 99)]
		public int employeeId { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(11)"),
			Required,
			Display(Name = "PESEL"),
			Range(10000000000, 99999999999, ErrorMessage = "PESEL składa się z 11 cyfr"),]
		public long? pesel { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Ulica"),
			MaxLength(40, ErrorMessage = "Ulica nie może zaweirać więcej niż 40 znaków")]
		public string street { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(3)"),
			Display(Name = "Nr mieszkania"),
			Range(0, 999, ErrorMessage = "Numer mieszkania nie może być większy niż 999")]
		public int? houseNumber { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Miasto"),
			MaxLength(30, ErrorMessage = "Miasto nie może zawierać więcej niż 30 znaków")]
		public string town { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Kod pocztowy"),
			MaxLength(6, ErrorMessage = "Kod pocztowy nie może zawierać więcej niż 6 znaków")]
		public string zipCode { get; set; }

		[BindProperty(SupportsGet = true),
			Phone,
			Required,
            //Column(TypeName = "NUMERIC(9)"),
            Display(Name = "Nr telefonu")]
		public string phoneNumber { get; set; }

		[BindProperty(SupportsGet = true),
            Required,
			Display(Name = "Data urodzenia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime birthDate { get; set; }

		public Employee employee { get; set; }
	}
}
