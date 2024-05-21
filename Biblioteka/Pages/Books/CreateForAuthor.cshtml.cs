using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;

namespace Biblioteka.Views.Books
{
    [Authorize(Roles = "Author")]
    public class CreateForAuthorModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private IPublisherRepository _publisherRepository;
        private ITagRepository _tagRepository;
        private IAuthorRepository _authorRepository;
        private readonly ILogger<CreateModel> _logger;
        private UserManager<BibUser> _userManager;

        public CreateForAuthorModel(ILogger<CreateModel> logger, IBookRepository bookRepository, IGenreRepository genreRepository,
            IPublisherRepository publisherRepository, ITagRepository tagRepository,
            IBookTypeRepository bookTypeRepository, IAuthorRepository authorRepository, UserManager<BibUser> userManager)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _tagRepository = tagRepository;
            _bookTypeRepository = bookTypeRepository;
            _logger = logger;
            _authorRepository = authorRepository;
            _userManager = userManager;
        }
        public List<SelectListItem>? Genre { get; set; }
        public List<SelectListItem>? Type { get; set; }
        public List<SelectListItem>? Publisher { get; set; }
        public List<SelectListItem>? Tag { get; set; }

        [BindProperty]
        public BookDto Book { get; set; } = default!;
        [BindProperty]
        public string GenreId { get; set; } = default!;
        [BindProperty]
        public string[] TagIds { get; set; } = default!;
        [BindProperty]
        public string BookTypeId { get; set; } = default!;

        [BindProperty]
        public string PublisherId { get; set; } = default!;
        public IActionResult OnGet()
        {
            Genre = _genreRepository.getAll().Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
            Type = _bookTypeRepository.getAll().Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
            Publisher = _publisherRepository.getAll().Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();
            Tag = _tagRepository.getAll().Select(t => new SelectListItem { Value = t.tagId.ToString(), Text = t.name }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            if (Book.releaseDate > DateTime.Now)
            {
                ModelState.AddModelError("Book.releaseDate", "Nie można dodać książki, której data wydania jeszcze nie nastąpiła.");
            }

            if (_bookRepository.search(Book.title) != null)
            {
                ModelState.AddModelError("Book.title", "Książka o tym tytule została już dodana");
            }
            if (_bookRepository.searchISBN(Book.ISBN) != null)
            {
                ModelState.AddModelError("Book.ISBN", "Książka o tym numerze ISBN jest już dodana");
            }
            if (!ModelState.IsValid || Book == null)
            {
                Genre = _genreRepository.getAll().Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
                Type = _bookTypeRepository.getAll().Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
                Publisher = _publisherRepository.getAll().Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();
                Tag = _tagRepository.getAll().Select(t => new SelectListItem { Value = t.tagId.ToString(), Text = t.name }).ToList();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var book = Book;
                        Console.WriteLine($"Model error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            else
            {
                var book = Book;
                Book newBook = new Book()
                {
                    title = Book.title,
                    ISBN = Book.ISBN,
                    description = Book.description,
                    availableCopys = Book.availableCopys,
                    ratingAVG = Book.ratingAVG,
                    releaseDate = Book.releaseDate,
                    floor = Book.floor,
                    alley = Book.alley,
                    rowNumber = Book.rowNumber,
                };

                Genre? foundGenre = _genreRepository.getAll().FirstOrDefault(r => r.genreId.ToString().Equals(GenreId.ToString()));

                if (foundGenre != null)
                {
                    newBook.genre = foundGenre;
                }

                newBook.tags = new List<Book_Tag>();
                bool tag_found = false;

                foreach (var tagId in TagIds)
                {
                    Tag? foundTag = _tagRepository.getAll().FirstOrDefault(r => r.tagId.ToString() == tagId);
                    if (foundTag != null)
                    {
                        newBook.tags.Add(new Book_Tag { tag = foundTag, book = newBook });
                        tag_found = true;
                    }
                }

                Publisher? foundPublisher = _publisherRepository.getAll().FirstOrDefault(r => r.publisherId.ToString().Equals(PublisherId.ToString()));

                if (foundPublisher != null)
                {
                    newBook.publisher = foundPublisher;
                }
                BookType? foundType = _bookTypeRepository.getAll().FirstOrDefault(r => r.typeId.ToString().Equals(BookTypeId.ToString()));

                if (foundType != null)
                {
                    newBook.type = foundType;
                }

                newBook.authors = new List<Book_Author>();

                var loggedInUserId = _userManager.GetUserId(User);

                if (loggedInUserId != null)
                {
                    var user = await _userManager.FindByIdAsync(loggedInUserId);

                    if (user != null)
                    {
                        string email = user.Email;

                    var foundAuthor =  _authorRepository.GetByMail(email);

                        if (foundAuthor != null)
                        {
                            newBook.authors.Add(new Book_Author { author = foundAuthor, book = newBook });
                        }
                    }
                }

                _bookRepository.Add(newBook);

                return RedirectToPage("./AuthorBooks");

            }
        }
    }
}