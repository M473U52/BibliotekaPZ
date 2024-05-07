using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace Biblioteka.Models
{
	public class RoomReservation
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Column(TypeName = "NUMERIC(4)"),
			Display(Name = "Id rezerwacji"),
			Range(0, 9999)]
		public int reservationId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Wynajmujący czytelnik")]
		public Reader reader { get; set; }

		//
		public int readerId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Wynajmowana sala")]
		public Room room { get; set; }

		//
		public int roomId { get; set; }

		[BindProperty(SupportsGet = true),
			Required(ErrorMessage = "wymagane jest podanie daty"),
			Display(Name = "Data początku rezerwacji")]
		public DateTime begginingOfReservationDate { get; set; }

		[BindProperty(SupportsGet = true),
			Required(ErrorMessage = "wymagane jest podanie daty"),
			Display(Name = "Data końca rezerwacji")]
		public DateTime endOfReservationDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required(ErrorMessage = "wymagane jest podanie godziny"),
            Display(Name = "Godzina rozpoczęczia rezerwacji")]
        public TimeSpan startingTime { get; set; }

        [BindProperty(SupportsGet = true),
            Required(ErrorMessage = "wymagane jest podanie godziny"),
            Display(Name = "Godzina zakończenia rezerwacji")]
        public TimeSpan endingTime { get; set; }

        [BindProperty(SupportsGet = true),
		Display(Name = "Pracownik nadzorujący wynajęcie")]
		public Employee? employee { get; set; }


        [BindProperty(SupportsGet = true),
        Display(Name = "Potwierdzenie wydania klucza")]
        public bool Confirmation { get; set; }
    }
}
