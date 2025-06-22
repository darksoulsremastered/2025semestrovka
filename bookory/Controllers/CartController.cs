using bookory.Data;
using bookory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bookory.Controllers
{
    public class CartController : Controller
    {
        private readonly BooksDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public CartController(BooksDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");
            var existingCartItem = _context.CartItems.FirstOrDefault(c => c.BookId == bookId && c.UserId == user.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _context.Update(existingCartItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    BookId = bookId,
                    UserId = user.Id,
                    Quantity = 1
                };
                _context.CartItems.Add(newItem);
            }

            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult RemoveFromCart(int bookId) 
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var userId = user.Id;
            var items = _context.CartItems.Include(c => c.Book).Where(c => c.UserId == userId).ToList();

            return View(items);
        }

        public IActionResult CartSidebar()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
                return Unauthorized(); 

            var items = _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToList();

            return PartialView("_CartSidebar", items);
        }
    }
}
