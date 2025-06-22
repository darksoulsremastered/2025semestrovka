using bookory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookory.Controllers
{
    public class ShopController : Controller
    {
        private readonly BooksDbContext _context;


        public ShopController(BooksDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Tags)
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .ToList(); 
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(books.ToList());
        }

        [HttpGet]
        public IActionResult Index(string authors, string vendors, string tags, string genres)
        {
            var booksQuery = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Vendor)
                .Include(b => b.Tags)
                .Include(b => b.Genres)
                .AsQueryable();

            if (!string.IsNullOrEmpty(authors))
            {
                var authorIds = authors.Split(',').Select(int.Parse).ToList();
                booksQuery = booksQuery.Where(b => authorIds.Contains(b.AuthorId));
            }
            if (!string.IsNullOrEmpty(vendors))
            {
                var vendorIds = vendors.Split(',').Select(int.Parse).ToList();
                booksQuery = booksQuery.Where(b => vendorIds.Contains(b.VendorId));
            }
            if (!string.IsNullOrEmpty(tags))
            {
                var tagIds = tags.Split(',').Select(int.Parse).ToList();
                booksQuery = booksQuery.Where(b => b.Tags.Any(t => tagIds.Contains(t.Id)));
            }
            if (!string.IsNullOrEmpty(genres))
            {
                var genreIds = genres.Split(',').Select(int.Parse).ToList();
                booksQuery = booksQuery.Where(b => b.Genres.Any(g => genreIds.Contains(g.Id)));
            }

            var filteredBooks = booksQuery.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_BookListPartial", filteredBooks);
            }

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(filteredBooks);
        }

    }
}
