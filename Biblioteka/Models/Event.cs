using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Column(TypeName = "NUMERIC(3)"),
            Display(Name = "Id wydarzenia"),
            Range(0, 999)]
        public int eventId { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Nazwa"),
            MaxLength(100, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
        public string name { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Opis"),
            MaxLength(400, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]
        public string description { get; set; }

        [BindProperty(SupportsGet = true),
        Required,
        Display(Name = "Data")]
        public DateTime eventDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Autor")]
        public Author author { get; set; }
        public int authorId { get; set; }

    }
}
