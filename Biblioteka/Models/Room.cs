using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Biblioteka.Models
{
	public class Room
	{
		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(2)"),
			Required,
			Display(Name = "Rodzaj sali"),
			Range(0, 99, ErrorMessage = "Rodzaj sali nie może być ujemny ani większy od 99")]
		public int roomNumber { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(2)"),
			Required,
			Display(Name = "Liczba miejsc"),
			Range(0, 99, ErrorMessage = "Liczba miejsc nie może być ujemna ani większa od 99")]
		public int seatCount { get; set; }
		public ICollection<RoomReservation> reservations { get; set; } = new List<RoomReservation>();

        [BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Rodzaj sali")]
        public RoomType roomType { get; set; }
        public int roomTypeId { get; set; }
        public string FullData
        {
            get
            {
                return $"Nr sali: {roomNumber}, ilość miejsc: {seatCount}";
            }
        }

    }
}
