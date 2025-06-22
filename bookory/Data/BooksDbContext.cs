using bookory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace bookory.Data
{
    public class BooksDbContext : IdentityDbContext<IdentityUser>
    {

        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Tags> Tags { get; set ;}
        public DbSet<Vendor> Vendors { get; set;}
        public DbSet<CartItem> CartItems { get; set; }

    }
}
