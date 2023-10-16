using LibraryApplication.BusinessLayer.Services.IServices;
using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer.Services
{
    public class BooksBorrowerInfosViewModelService : IBooksBorrowerInfosViewModelService
    {
        private readonly IBooksBorrowerInfosViewModelDal _viewModelDal;
        private readonly ILogger<BooksBorrowerInfosViewModelService> _logger;

        public BooksBorrowerInfosViewModelService(IBooksBorrowerInfosViewModelDal viewModelDal, ILogger<BooksBorrowerInfosViewModelService> logger)
        {
            _viewModelDal = viewModelDal;
            _logger = logger;
        }

        //To find out who bought book and when it will be returned, I created a view and list this view according to BookId.
        public async Task<BooksBorrowerInfosViewModel> GetListBooksBorrowerInfo(string BookId)
        {
            try
            {
                //I list datas with sql query.
                var BorrowerBooksInfo = await _viewModelDal.GetListFromSqlRaw("BooksBorrowerInfosViewModel", BookId);
                var BorrowerInfo = BorrowerBooksInfo.FirstOrDefault(p => p.BookId.ToString() == BookId);

                if (BorrowerInfo.IsReturned == false)
                {
                    return BorrowerInfo;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(GetListBooksBorrowerInfo)} is error");
                throw;
            }
        }
    }
}
