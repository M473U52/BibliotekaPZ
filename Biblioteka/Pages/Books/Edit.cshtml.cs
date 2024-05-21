using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using System.ComponentModel;

using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Views.Books
{
    [Authorize(Roles = "Employee, Admin, Author")]
    public class EditModel : PageModel
    {
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IBookTypeRepository _bookTypeRepository;
        private IPublisherRepository _publisherRepository;
        private IAuthorRepository _authorRepository;
        private ITagRepository _tagRepository;
        public EditModel(IBookRepository bookRepository,  IGenreRepository genreRepository,
            IPublisherRepository publisherRepository, IBookTypeRepository bookTypeRepository, IAuthorRepository authorRepository, ITagRepository tagRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _bookTypeRepository = bookTypeRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
        }
        public List<SelectListItem>? Genre { get; set; }
        public List<SelectListItem>? Type { get; set; }
        public List<SelectListItem>? Publisher { get; set; }
        public List<SelectListItem>? Author { get; set; }
        public List<SelectListItem>? Tag { get; set; }

        [BindProperty]
        public BookDto Book { get; set; } = new BookDto();
        [BindProperty]
        [DisplayName("Gatunek")]
        public string GenreId { get; set; } = "";

        [BindProperty]
        [DisplayName("Tagi")]
        public string[] TagIds { get; set; } = default!;

        [BindProperty]
        [DisplayName("Typ")]
        public string BookTypeId { get; set; } = "";
        [BindProperty]
        [DisplayName("Wydawca")]
        public string PublisherId { get; set; } = "";

        [BindProperty]
        [DisplayName("Autorzy")]
        public string[] AuthorIds { get; set; }


        public IActionResult OnGet(int id)
        {
            Genre = _genreRepository.getAll().Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
            Type = _bookTypeRepository.getAll().Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
            Publisher = _publisherRepository.getAll().Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();
            Author = _authorRepository.getAll().Select(r => new SelectListItem { Value = r.authorId.ToString(), Text = r.name + " " + r.surname }).ToList();
            var book_authors = _bookRepository.getOne(id).authors.Select(a => a.authorId);
            foreach (var author in Author)
            {
                if (book_authors.Contains(int.Parse(author.Value)))
                {
                    author.Selected = true;
                }
            }
            Tag = _tagRepository.getAll().Select(r => new SelectListItem { Value = r.tagId.ToString(), Text = r.name }).ToList();
            
            var book_tags = _bookRepository.getOne(id).tags.Select(t => t.tagId);
            foreach (var tag in Tag)
            {
                if (book_tags.Contains(int.Parse(tag.Value)))
                {
                    tag.Selected = true;
                }
            }

            var book = _bookRepository.getOne(id);
            if (book == null)
            {
                return NotFound();
            }

            // Przypisanie wartości z obiektu book do właściwości Book
            Book = new BookDto
            {
                bookId = book.bookId,
                title = book.title,
                ISBN = book.ISBN,
                description = book.description,
                availableCopys = book.availableCopys,
                ratingAVG = book.ratingAVG,
                releaseDate = book.releaseDate,
                floor = book.floor,
                alley = book.alley,
                rowNumber = book.rowNumber
            };

            return Page();
        }
        public IActionResult OnPost()
        {
            ValidateFile(Book.image, new[] { ".jpg" }, 10 * 1024 * 1024, "Okładka");
            ValidateFile(Book.ebook, new[] { ".pdf" }, 10 * 1024 * 1024, "Ebook");
            ValidateFile(Book.audiobook, new[] { ".mp3" }, 10 * 1024 * 1024, "Audiobook");
            if (!ModelState.IsValid || Book == null)
            {
                return Page();
            }
            else
            {
                var existingBook = _bookRepository.getOne(Book.bookId);

                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.title = Book.title;
                existingBook.ISBN = Book.ISBN;
                existingBook.description = Book.description;
                existingBook.availableCopys = Book.availableCopys;
                existingBook.ratingAVG = Book.ratingAVG;
                existingBook.releaseDate = Book.releaseDate;
                existingBook.floor = Book.floor;
                existingBook.alley = Book.alley;
                existingBook.rowNumber = Book.rowNumber;

                Genre? foundGenre = _genreRepository.getAll().FirstOrDefault(r => r.genreId.ToString().Equals(GenreId.ToString()));
                
                if (foundGenre != null)
                {
                    existingBook.genre = foundGenre;
                }
                else
                    ModelState.AddModelError("", "Gatunek jest wymagany.");

                existingBook.tags = new List<Book_Tag>();

                foreach (var tagId in TagIds)
                {
                    Tag? foundTag = _tagRepository.getAll().FirstOrDefault(r => r.tagId.ToString() == tagId);
                    if (foundTag != null)
                    {
                        existingBook.tags.Add(new Book_Tag { tag = foundTag, book = existingBook });
                    }
                    else
                        ModelState.AddModelError("", "Tagi są wymagane.");
                }

                Publisher? foundPublisher = _publisherRepository.getAll().FirstOrDefault(r => r.publisherId.ToString().Equals(PublisherId.ToString()));

                if (foundPublisher != null)
                {
                    existingBook.publisher = foundPublisher;
                }
                BookType? foundType = _bookTypeRepository.getAll().FirstOrDefault(r => r.typeId.ToString().Equals(BookTypeId.ToString()));

                if (foundType != null)
                {
                    existingBook.type = foundType;
                }

                existingBook.authors = new List<Book_Author>();

                if (AuthorIds != null && AuthorIds.Length > 0)
                {
                    foreach (var authorId in AuthorIds)
                    {
                        Author? foundAuthor = _authorRepository.getAll().FirstOrDefault(r => r.authorId.ToString() == authorId);
                        if (foundAuthor != null)
                        {

                            existingBook.authors.Add(new Book_Author { author = foundAuthor, book = existingBook });
                        }
                    }
                }

                if (Book.image != null && Book.image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.image.CopyTo(ms);
                        existingBook.imageData = ms.ToArray();
                    }
                }

                if (Book.ebook != null && Book.ebook.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.ebook.CopyTo(ms);
                        existingBook.ebookData = ms.ToArray();
                    }
                }

                if (Book.audiobook != null && Book.audiobook.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Book.audiobook.CopyTo(ms);
                        existingBook.audiobookData = ms.ToArray();
                    }
                }

                _bookRepository.Update(existingBook);
                return RedirectToPage("./Index");
            }
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
