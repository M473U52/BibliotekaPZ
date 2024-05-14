using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class BookDto
    {
        [Display(Name = "Id książki")]
        public int bookId { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany"),
            Display(Name = "Tytuł"),
            MaxLength(50, ErrorMessage = "Tytuł nie może zawierać więcej niż 50 znaków")]
        public string title { get; set; }

        [
            Required(ErrorMessage = "Numer ISBN jest wymagany"),
            Display(Name = "Numer ISBN"),
            Range(1000000000000, 9999999999999, ErrorMessage = "Numer ISBN składa się z 13 cyfr")]
        public long ISBN { get; set; }

        [   Required(ErrorMessage = "Opis jest wymagany"),
            Display(Name = "Opis"),
            MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
        public string description { get; set; }

        [
            Required(ErrorMessage = "Dostępne kopie są wymagane"),
            Display(Name = "Dostępne kopie"),
            Range(0, 999)]
        public int availableCopys { get; set; }

        [
            Display(Name = "Średnia"),
            Range(0.00, 5.00)]
        public double? ratingAVG { get; set; }

        [
            Required(ErrorMessage = "Data wydania jest wymagana"),
            Display(Name = "Data wydania")]
        public DateTime releaseDate { get; set; }

        [
            Required(ErrorMessage = "Piętro jest wymagane"),
            Column(TypeName = "NUMERIC(1)"),
            Display(Name = "Piętro"),
           
        Range(0, 9)]
        public int floor { get; set; }

        [
            Required(ErrorMessage = "Alejka jest wymagana"),
            Display(Name = "Alejka"),
            Range(0, 99)]
        public int alley { get; set; }

        [
           Required(ErrorMessage = "Rząd jest wymagany"),
           Display(Name = "Rząd"),
           Range(0, 99)]
        public int rowNumber { get; set; }

        [Display(Name = "Okładka"),
            NotMapped]
        public IFormFile? image { get; set; }

        [Display(Name = "Ebook"),
            NotMapped]
        public IFormFile? ebook { get; set; }

        [Display(Name = "Audiobook"),
            NotMapped]
        public IFormFile? audiobook { get; set; }
    }
}
