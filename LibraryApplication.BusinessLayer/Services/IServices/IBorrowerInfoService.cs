using LibraryApplication.DataAccessLayer.DTO;
using LibraryApplication.EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer.Services.IServices
{
    public interface IBorrowerInfoService
    {
        Task<BorrowerInfo> AddBorrowerInfo(BorrowerInfo borrowerInfo);
    }
}
