using System.ComponentModel.DataAnnotations;


namespace Biblioteka.Models
{
    public class BorrowingDto
    {

            [Display(Name = "Id wypożyczenia")]
        public int borrowId { get; set; }

            [Required,
            Display(Name = "Data wypożyczenia")]
        public DateTime borrowDate { get; set; }

        [
            Required,
            Display(Name = "Planowana data zwrotu")]
        public DateTime plannedReturnDate { get; set; }

        [
        Display(Name = "Potwierdzenie odbioru")]
        public bool Confirmation { get; set; }

        [
        Display(Name = "Potwierdzenie zwrotu")]
        public bool IsReturned { get; set; }

        [
        Display(Name = "Potwierdzenie opłaty")]
        public bool IsPaid { get; set; }

        [
            Display(Name = "Data zwrotu")]
        public DateTime? returnDate { get; set; }

        

        [Display(Name = "Opłata za spóźnienie")]
        public decimal LateFee { get; set; }

        [
            Display(Name = "Czy klient zgubił książkę")]
        public bool bookLost { get; set; }

        
    }
}
