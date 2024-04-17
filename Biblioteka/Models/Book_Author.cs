using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Biblioteka.Models
{
    public class Book_Author
    {
        /*[BindProperty(SupportsGet = true),
            Display(Name = "Id książki")]*/
        
        //[ForeignKey("bookId")]
        public int bookId { get; set; }

        /*[BindProperty(SupportsGet = true),
            Display(Name = "Id autora")]*/
        //[ForeignKey("authorId")]
        public int authorId { get; set; }

        public Book book { get; set; }

        public Author author { get; set; }
    }
}