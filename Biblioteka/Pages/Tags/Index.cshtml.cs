using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private ITagRepository _tagRepository;

        public IndexModel(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public void OnGet(string? tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
            {
                Tag = new List<Tag> { _tagRepository.searchTag(tagName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Tag = _tagRepository.getAll();
            }
        }
    }
}
