using bookory.Data;
using bookory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookory.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BooksDbContext _context;

        public AuthorsController(BooksDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Authors author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        public IActionResult Details(int id)
        {
            var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
    }
}
