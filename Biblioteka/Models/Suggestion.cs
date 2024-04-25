using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Suggestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Column(TypeName = "NUMERIC(4)"),
            Display(Name = "Id sugestii"),
            Range(0, 9999)]
        public int suggestionId { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Tytuł"),
            MaxLength(50, ErrorMessage = "Tytuł nie może zawierać więcej niż 50 znaków")]
        public string title { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Autor"),
            MaxLength(50, ErrorMessage = "Autor nie może zawierać więcej niż 50 znaków")]
        public string author { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Column(TypeName = "NUMERIC(4)"),
            Display(Name = "Ilość głosów"),
            Range(0, 9999)]
        public int votes { get; set; }
    }
}