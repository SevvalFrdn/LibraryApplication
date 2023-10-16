using AutoMapper;
using LibraryApplication.BusinessLayer.Services.IServices;
using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Context;
using LibraryApplication.DataAccessLayer.DTO;
using LibraryApplication.EntityLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(IBookDal bookDal, ILogger<BookService> logger, IMapper mapper)
        {
            _bookDal = bookDal;
            _logger = logger;
            _mapper = mapper;
        }
        //For used to add data(book) to the Books table.
        public async Task<bool> AddBookAsync(AddBookImage addbook)
        {
            try
            {
                _logger.LogInformation($"{nameof(AddBookAsync)} was started");

                //I wrote it to upload photos
                if (addbook.ImageUrl != null)
                {
                    var extension = Path.GetExtension(addbook.ImageUrl.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BookImageFiles/", newimagename);
                    var stream = new FileStream(location, FileMode.Create);
                    addbook.ImageUrl.CopyTo(stream);
                    var book = _mapper.Map<Book>(addbook);
                    book.ImageUrl = newimagename;
                    book.BookId = Guid.NewGuid(); //for book name ex name(Guid).jpg

                    var result = await _bookDal.AddAsync(book); //add book
                    await _bookDal.SaveAsync();
                    return result;

                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(AddBookAsync)} is error");
                throw;
            }

        }

        //To list data(book) in the Books table
        public async Task<List<Book>> GetListBooks()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetListBooks)} was started");

                List<Book> books = await _bookDal.GetList(); 

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(GetListBooks)} is error");
                throw;
            }
        }

        //To update the IsBorrowed column in the Books table to true.So I understand that the book is not in the library.
        public async Task<bool> UpdateIsBorrowedTrue(object BookId)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateIsBorrowedTrue)} was started");

                var bookInfo = await _bookDal.GetById(BookId);//I find the book by BookId

                bookInfo.IsBorrowed = true;

                var result = _bookDal.Update(bookInfo); //I update Isborrowed is true

                await _bookDal.SaveAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(UpdateIsBorrowedTrue)} is error");
                throw;
            }
        }
    }
}
