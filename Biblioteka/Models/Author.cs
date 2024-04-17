using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Biblioteka.Repositories;
using Biblioteka.Context;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteka.Models
{
	public class Author : IValidatableObject
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Column(TypeName = "NUMERIC(3)"),
			Display(Name = "Id autora"),
			Range(0, 999)]
		public int authorId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Imię"),
			MaxLength(20, ErrorMessage = "Imię nie może zawierać więcej niż 20 znaków")]
		public string? name { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwisko"),
			MaxLength(40, ErrorMessage = "Nazwisko nie może zawierać więcej niż 40 znaków")]
		public string? surname { get; set; }

		[BindProperty(SupportsGet = true),
			//Required,
			Display(Name = "Data urodzenia")]
		public DateTime birthDate { get; set; }

		[BindProperty(SupportsGet = true),
			//Required,
			Display(Name = "Kraj pochodzenia"),
			MaxLength(30, ErrorMessage = "Kraj nie może zawierać więcej niż 30 znaków")]
		public string? country { get; set; }

		[BindProperty(SupportsGet = true),
			Display(Name = "Pseudonim"),
			MaxLength(35, ErrorMessage = "Pseudonim nie może zawierać więcej niz 35 znaków")]
		public string? nickname { get; set; }

		[BindProperty(SupportsGet = true),
			Display(Name = "Opis"),
			MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
		public string? description { get; set; }
		
		// zmiana
		//public ICollection<Book> books { get; set; } = new List<Book>();
        public ICollection<Book_Author> books { get; set; } = new List<Book_Author>();


        [BindProperty(SupportsGet = true),
			//Required,
			Display(Name = "Adres e-mail konta czytelnika"),
			EmailAddress,
			MaxLength(40, ErrorMessage = "E-mail nie może zawierać więcej niż 40 znaków")]
		public string? email { get; set; }

        [AllowNull]
        public byte[]? imageData { get; set; }

        [Display(Name = "Zdjęcie"),
            NotMapped]
        public IFormFile? image { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (name == "aaa")
			{
				yield return new ValidationResult("Imie nie moze byc aaa!", new[] { "name" });
			}
		}
		public ICollection<Event> events { get; set; } = new List<Event>();

        public string FullName
        {
            get
            {
				if (nickname != null)
				{
					return $"{name} \"{nickname}\" {surname}";
				}
				else
					return $"{name} {surname}";
            }
        }
    }

}
