using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Serilog;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Xunit.Sdk;
using Biblioteka.Repositories.DbImplementations;
namespace Biblioteka.Views.Books
{
    public class CreateModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private IPublisherRepository _publisherRepository;
        private IAuthorRepository _authorRepository;
        private ITagRepository _tagRepository;
        private ISuggestionRepository _suggestionRepository;


        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger, IBookRepository bookRepository, IGenreRepository genreRepository,
            IPublisherRepository publisherRepository, IAuthorRepository authorRepository, ITagRepository tagRepository,
            IBookTypeRepository bookTypeRepository, ISuggestionRepository suggestionRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
            _bookTypeRepository = bookTypeRepository;
            _logger = logger;
            _suggestionRepository = suggestionRepository;
        }
        public List<SelectListItem>? Genre { get; set; }
        public List<SelectListItem>? Type { get; set; }
        public List<SelectListItem>? Publisher { get; set; }
        public List<SelectListItem>? Author { get; set; }
        public List<SelectListItem>? Tag { get; set; }

        [BindProperty]
        public BookDto Book { get; set; } = default!;

        public Book normalBook { get; set; }

        [BindProperty]
        [DisplayName("Gatunek")]
        public string GenreId { get; set; } = default!;
        [BindProperty]
        [DisplayName("Tagi")]
        [Required(ErrorMessage = "Tag jest wymagany")]
        public string[] TagIds { get; set; } = default!;
        [BindProperty]
        [DisplayName("Typ")]
        public string BookTypeId { get; set; } = default!;

        [BindProperty]
        [DisplayName("Wydawca")]
        public string PublisherId { get; set; } = default!;
        [BindProperty]
        public DateTime releaseDate { get; set; } = DateTime.Now!;

        [BindProperty]
        [DisplayName("Autorzy")]
        [Required(ErrorMessage = "Autor jest wymagany")]
        public string[] AuthorIds { get; set; }
        public Suggestion suggestion {get; set;}
        public IActionResult OnGet(int? suggestionId = null)
        {
            Genre = _genreRepository.getAll().Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
            Type = _bookTypeRepository.getAll().Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
            Publisher = _publisherRepository.getAll().Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();

            Author = _authorRepository.getAll().Select(r => new SelectListItem { Value = r.authorId.ToString(), Text = r.name + " " + r.surname }).ToList();
            Tag = _tagRepository.getAll().Select(t => new SelectListItem { Value = t.tagId.ToString(), Text = t.name }).ToList();

            if (suggestionId.HasValue)
            {
                suggestion = _suggestionRepository.getOne(suggestionId);
                TempData["SuggestionId"] = suggestionId.Value;

                Author author = _authorRepository.getOne(suggestion.author);
                if (author is null)
                {
                        
                    Author newAuthor = new Author
                    {
                        name = suggestion.author.Split(' ')[0],
                        surname = suggestion.author.Split(" ")[1],
                    };
                }
                Book newBook = new Book
                {
                    title = suggestion.title,
                    authors = (ICollection<Book_Author>)author,
                };
               
            }


			return Page();
        }

        public IActionResult OnPost()
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
            ValidateFile(Book.image, new[] { ".jpg" }, 10 * 1024 * 1024, "Okładka");
            ValidateFile(Book.ebook, new[] { ".pdf" }, 10 * 1024 * 1024, "Ebook");
            ValidateFile(Book.audiobook, new[] { ".mp3" }, 10 * 1024 * 1024, "Audiobook");

            if (!ModelState.IsValid || Book == null)
            {
                Genre = _genreRepository.getAll().Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
                Type = _bookTypeRepository.getAll().Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
                Publisher = _publisherRepository.getAll().Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();
                Author = _authorRepository.getAll().Select(r => new SelectListItem { Value = r.authorId.ToString(), Text = r.name + " " + r.surname }).ToList();
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
                
                Book newBook = new Book()
                {
                    title = Book.title,
                    ISBN = Book.ISBN,
                    description = Book.description,
                    availableCopys = Book.availableCopys,
                    releaseDate = Book.releaseDate,
                    ratingAVG = Book.ratingAVG,
                    floor = Book.floor,
                    alley = Book.alley,
                    rowNumber = Book.rowNumber,
                };
               
           


                Genre? foundGenre = _genreRepository.getAll().FirstOrDefault(r => r.genreId.ToString().Equals(GenreId.ToString()));

                if (foundGenre != null)
                {
                    newBook.genre = foundGenre;
                }
                else
                    ModelState.AddModelError("", "Gatunek jest wymagany.");

                newBook.tags = new List<Book_Tag>();

                foreach (var tagId in TagIds)
                {
                    Tag? foundTag = _tagRepository.getAll().FirstOrDefault(r => r.tagId.ToString() == tagId);
                    if (foundTag != null)
                    {
                        newBook.tags.Add(new Book_Tag { tag = foundTag, book = newBook });
                    }
                    else
                        ModelState.AddModelError("", "Tagi są wymagane.");
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

                if (AuthorIds != null && AuthorIds.Length > 0)
                {
                    foreach (var authorId in AuthorIds)
                    {
                        Author? foundAuthor = _authorRepository.getAll().FirstOrDefault(r => r.authorId.ToString() == authorId);
                        if (foundAuthor != null)
                        {

                            newBook.authors.Add(new Book_Author { author = foundAuthor, book = newBook });
                        }
                    }
                }


                if (Book.image != null && Book.image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.image.CopyTo(ms);
                        newBook.imageData = ms.ToArray();
                    }
                }

                if (Book.ebook != null && Book.ebook.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.ebook.CopyTo(ms);
                        newBook.ebookData = ms.ToArray();
                    }
                }

                if (Book.audiobook != null && Book.audiobook.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.audiobook.CopyTo(ms);
                        newBook.audiobookData = ms.ToArray();
                    }
                }


                _bookRepository.Add(newBook);
            }

            var suggestionId = TempData["SuggestionId"] as int?;
            if (suggestionId.HasValue)
            {
                _suggestionRepository.Delete(suggestionId.Value);
            }

            Log.ForContext("SaveToFile", "AnyValue").Information("Dodano książkę " + Book.title );

            TempData["Message"] = $"Success/Książka została dodana pomyślnie.";
            return RedirectToPage("./Index");
        }
        private void ValidateFile(IFormFile file, string[] allowedExtensions, int maxFileSize, string fieldName)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError($"Book.{fieldName}", $"Dozwolone rozszerzenia dla {fieldName} to: {string.Join(", ", allowedExtensions)}");
                }

                if (file.Length > maxFileSize)
                {
                    ModelState.AddModelError($"Book.{fieldName}", $"Maksymalny rozmiar pliku dla {fieldName} to {maxFileSize / (1024 * 1024)} MB");
                }
            }
        }
    }
}