using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.EntityLayer.Models
{
    public class BooksBorrowerInfosViewModel
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; } // Photo path of the book (URL)
        public bool IsBorrowed { get; set; } //True if borrowed, false if not borrowed
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}
