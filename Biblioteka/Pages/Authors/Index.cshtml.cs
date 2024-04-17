using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class IndexModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public IndexModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IList<Author> Author { get; set; } = default!;

        public void OnGet()
        {
            Author = _authorRepository.getAll();
        }
    }
}
