using bookory.Data;
using bookory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookory.Controllers
{
    public class AdminController : Controller
    {
        private readonly BooksDbContext _context;

        public AdminController(BooksDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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

    }
}
