using bookory.Data;
using bookory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookory.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksDbContext _context;

        public BooksController(BooksDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).ToList();
            var authors = _context.Authors.ToList();

            ViewBag.Authors = authors;
            return View(books);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View(new BookFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(BookFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Vendors = _context.Vendors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View(vm);
            }

            var selectedGenres = _context.Genres
            .Where(g => vm.SelectedGenreIds.Contains(g.Id))
            .ToList();

            var selectedTags = _context.Tags
            .Where(g => vm.SelectedTagIds.Contains(g.Id))
            .ToList();

            var book = new Book
            {
                Title = vm.Title,
                Price = vm.Price,
                AuthorId = vm.AuthorId,
                VendorId = vm.VendorId,
                CoverUrl = vm.CoverUrl,
                Description = vm.Description,
                Tags = selectedTags,
                Genres = selectedGenres 
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var book = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .Include(b => b.Tags)
            .FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            var otherBooks = _context.Books
                    .Where(b => b.Id != id).
                    Include(b => b.Author)
                    .Take(6)
                    .ToList();


            var viewModel = new BookDetailsViewModel
            {
                MainBook = book,
                OtherBooks = otherBooks
            };
            return View(viewModel);
        }
    }

}
