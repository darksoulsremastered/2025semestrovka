using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace bookory.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AuthorId { get; set; }

        [ValidateNever]
        public Authors Author { get; set; }

        [Required]
        public int VendorId { get; set; }

        [ValidateNever]
        public Vendor Vendor { get; set; }

        [Required]
        public string CoverUrl { get; set; }

        [ValidateNever]
        public ICollection<Genres> Genres { get; set; }
        [ValidateNever]
        public ICollection<Tags> Tags { get; set; }
        public string Description { get; set; }
    }

    public class Authors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Book> Books { get; set; }
    }

    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }

    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
       
    }

    public class BookDetailsViewModel
    {
        public Book MainBook { get; set; }
        public List<Book> OtherBooks { get; set; }


    }

    public class BookFormViewModel
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public int VendorId { get; set; }
        public string CoverUrl { get; set; }

        public string Description { get; set; }
        public List<int> SelectedGenreIds { get; set; } = new();
        public List<int> SelectedTagIds { get; set; } = new();
    }

}

