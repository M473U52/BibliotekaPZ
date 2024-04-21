using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace Biblioteka.Models
{
    public class Borrowing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Column(TypeName = "NUMERIC(5)"),
            Display(Name = "Id wypożyczenia"),
            Range(0, 99999)]
        public int borrowId { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Data wypożyczenia")]
        public DateTime borrowDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Planowana data zwrotu")]
        public DateTime plannedReturnDate { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Odbiór")]
        public bool Confirmation { get; set; }
        [BindProperty(SupportsGet = true),
        Display(Name = "Zwrot")]
        public bool IsReturned { get; set; }

        [BindProperty(SupportsGet = true),
        Display(Name = "Opłata")]
        public bool IsPaid { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Data zwrotu")]
        public DateTime? returnDate { get; set; }

        [BindProperty(SupportsGet = true),
            Required,
            Display(Name = "Książka")]
        public Book book { get; set; }

        //
        public int bookId { get; set; }

        [Display(Name = "Opłata za spóźnienie")]
        public decimal LateFee { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Pracownik potwierdzający odbiór")]
        public Employee? employee { get; set; }
        public int employeeId { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Pracownik potwierdzający zwrot")]
        public EmployeeConfirmingReturn? employeeConfirmingReturn { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Pracownik potwierdzający opłatę")]
        public EmployeeConfirmingPayment? employeeConfirmingPayment { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Czy klient zgubił książkę")]
        public bool bookLost { get; set; }

        public ICollection<Reader_Borrowings> readers { get; set; } = new List<Reader_Borrowings>();
    }
}
