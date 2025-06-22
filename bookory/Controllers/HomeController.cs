using System.Diagnostics;
using bookory.Data;
using bookory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksDbContext _context;


        public HomeController(ILogger<HomeController> logger, BooksDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).Include(a => a.Genres).ToList();
            var authors = _context.Authors.ToList();

            ViewBag.Authors = authors;
            return View(books);
        }

    }
}
