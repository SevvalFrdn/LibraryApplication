using LibraryApplication.BusinessLayer.Services;
using LibraryApplication.BusinessLayer.Services.IServices;
using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.DTO;
using LibraryApplication.EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBorrowerInfoService _borrowerInfoService;
        private readonly IBooksBorrowerInfosViewModelService _booksBorrowerInfosService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, IBorrowerInfoService borrowerInfoService, IBooksBorrowerInfosViewModelService booksBorrowerInfosViewModelService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _borrowerInfoService = borrowerInfoService;
            _booksBorrowerInfosService = booksBorrowerInfosViewModelService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var booklist = await _bookService.GetListBooks();
            return View(booklist);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookImage book)
        {
            bool isSuccessful = await _bookService.AddBookAsync(book);

            if (isSuccessful)
            {
                TempData["SuccessMessage"] = "Kitap başarıyla kaydedildi.";
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        public async Task<JsonResult> AddBorrowerInfo(BorrowerInfo Borrower)
        {
            try
            {
                //It works when I click on the "Ödünç ver" button. It help add Borrower information to database.
                await _bookService.UpdateIsBorrowedTrue(Borrower.BookId);

                await _borrowerInfoService.AddBorrowerInfo(Borrower);

                return new JsonResult(JsonConvert.SerializeObject(Borrower));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(AddBorrowerInfo)} is error");
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> BookBorrowerInfos(Guid BookId)
        {
            try
            {
                //It works when I click on the "Ödünç verildi" button. It help shows me the book name, delivery date and book ID.
                var BorrowerBooksInfo = await _booksBorrowerInfosService.GetListBooksBorrowerInfo(BookId.ToString());

                if (BorrowerBooksInfo is null)
                {
                    TempData["WarningMessage"] = "Bilgilere ulaşamadık.";
                }

                return new JsonResult(JsonConvert.SerializeObject(BorrowerBooksInfo));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(BookBorrowerInfos)} is error");
                throw;
            }
        }
    }
}
