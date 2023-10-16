using LibraryApplication.BusinessLayer.Services.IServices;
using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Context;
using LibraryApplication.EntityLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer.Services
{
    public class BorrowerInfoService : IBorrowerInfoService
    {
        private readonly IBorrowerInfoDal _borrowerInfoDal;
        private readonly ILogger<BorrowerInfoService> _logger;

        public BorrowerInfoService(IBorrowerInfoDal borrowerInfoDal, ILogger<BorrowerInfoService> logger)
        {
            _borrowerInfoDal = borrowerInfoDal;
            _logger = logger;
        }
        //I add the borrower's datas to the database.
        public async Task<BorrowerInfo> AddBorrowerInfo(BorrowerInfo borrowerInfo)
        {
            try
            {
                _logger.LogInformation($"{nameof(AddBorrowerInfo)} was started");

                borrowerInfo.Id = Guid.NewGuid();//I set an ID for the person who borrows the book.

                await _borrowerInfoDal.AddAsync(borrowerInfo);
                await _borrowerInfoDal.SaveAsync();

                return borrowerInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(AddBorrowerInfo)} is error");
                throw;
            }
        }
    }
}
