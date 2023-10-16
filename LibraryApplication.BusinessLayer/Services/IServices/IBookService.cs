using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApplication.DataAccessLayer.DTO;
using LibraryApplication.EntityLayer.Models;

namespace LibraryApplication.BusinessLayer.Services.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetListBooks();
        Task<bool> UpdateIsBorrowedTrue(object BookId);
        Task<bool> AddBookAsync(AddBookImage book);
    }
}
