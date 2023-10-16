using Microsoft.AspNetCore.Http;

namespace LibraryApplication.DataAccessLayer.DTO
{
    public class AddBookImage
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public IFormFile ImageUrl { get; set; } // Photo path of the book (URL)
        public bool IsBorrowed { get; set; } //True if borrowed, false if not borrowed
    }
}
