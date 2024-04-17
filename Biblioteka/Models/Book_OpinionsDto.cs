using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class Book_OpinionsDto
    {
        [
            Display(Name = "Id książki")]
        public int bookId { get; set; }
        [
            Display(Name = "Id autora")]
        public int readerId { get; set; }

        [
            Required,
            Column(TypeName = "NUMERIC(3,2)"),
            Range(0.00, 5.00, ErrorMessage = "Ocena nie może być większa niż 5.00 lub mniejsza niż 0.00"),
            Display(Name = "Średnia ocena")]
        public double rating { get; set; }

        [
            Display(Name = "Data dodania")]
        public DateTime? addedDate { get; set; }

        [
            Required,
            Display(Name = "Opinia"),
            MaxLength(500, ErrorMessage = "Opinia nie może zawierać więcej niż 500 znaków")]
        public string opinion { get; set; }
       
    }
}
