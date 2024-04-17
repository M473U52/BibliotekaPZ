using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class RoomType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Column(TypeName = "NUMERIC(2)"),
			Required,
			Display(Name = "Numer sali"),
			Range(0, 99, ErrorMessage = "Number sali nie może być ujemny ani większy od 99")]
		public int roomTypeId { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(5,2)"),
			Required,
			Display(Name = "Cena"),
			Range(0, 99, ErrorMessage = "Cena nie może być ujemna ani większa od 999,99")]
		public double price { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Nazwa"),
			MaxLength(100, ErrorMessage = "Nazwa nie może zawierać więcej niż 100 znaków")]
		public string name { get; set; }
		public ICollection<Room> rooms { get; set; } = new List<Room>();


	}
}
