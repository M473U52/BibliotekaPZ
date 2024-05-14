using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteka.Models
{
	public class Book
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Column(TypeName = "NUMERIC(4)"),
			Display(Name = "Id książki"),
			Range(0, 9999)]
		public int bookId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Tytuł"),
			MaxLength(50, ErrorMessage = "Tytuł nie może zawierać więcej niż 50 znaków")]
		public string title { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(13)"),
			Required,
			Display(Name = "Numer ISBN"),
			Range(1000000000000, 9999999999999, ErrorMessage = "Numer ISBN składa się z 13 cyfr")]
		public long ISBN { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Opis"),
			MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
		public string description { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(3)"),
			Required,
			Display(Name = "Dostępne kopie"),
			Range(0, 999)]
		public int availableCopys { get; set; }

        [BindProperty(SupportsGet = true),
            Column(TypeName = "NUMERIC(3,2)"),
            Display(Name = "Średnia"),
            Range(0.00, 5.00)]
        public double? ratingAVG { get; set; }

		[ForeignKey("ratingId")]
		public int ratingId { get; set;  }

        [BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Data wydania")]
		public DateTime releaseDate { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Wydawnictwo")]
		public Publisher publisher { get; set; }

        [ForeignKey("publisherId")]
        public int publisherId { get; set; }

        [BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Gatunek")]
		public Genre genre { get; set; }

		[ForeignKey("genreId")]
		public int genreId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Rodzaj")]
		public BookType type { get; set; }

        [ForeignKey("typeId")]
        public int typeId { get; set; }

        [BindProperty(SupportsGet = true),
			Required,
			Column(TypeName = "NUMERIC(1)"),
            Display(Name = "Piętro"),
            Range(0, 9)]
        public int floor { get; set; }
        [BindProperty(SupportsGet = true),
			Required,
			Column(TypeName = "NUMERIC(2)"),
            Display(Name = "Alejka"),
            Range(0, 99)]
        public int alley { get; set; }

        [BindProperty(SupportsGet = true),
			Required,
			Column(TypeName = "NUMERIC(2)"),
            Display(Name = "Rząd"),
            Range(0, 99)]
        public int rowNumber { get; set; }

		[AllowNull]
        public byte[]? imageData { get; set; }

        [AllowNull]
        public byte[]? ebookData { get; set; }

        [AllowNull]
        public byte[]? audiobookData { get; set; }

        public ICollection<Book_Author> authors { get; set; } = new List<Book_Author>();
        public ICollection<Book_Opinions> opinions { get; set; } = new List<Book_Opinions>();
		public ICollection<Book_Tag> tags { get; set; } = new List<Book_Tag>();
		public ICollection<Borrowing> borrowings { get; set; } = new List<Borrowing>();
	}
}
