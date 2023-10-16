using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Context;
using LibraryApplication.EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Concrete
{
    public class BorrowerInfoRepository : GenericRepository<BorrowerInfo>, IBorrowerInfoDal
    {
        public BorrowerInfoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
