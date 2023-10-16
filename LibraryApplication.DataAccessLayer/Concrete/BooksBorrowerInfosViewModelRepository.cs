using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Context;
using LibraryApplication.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Concrete
{
    public class BooksBorrowerInfosViewModelRepository : GenericRepository<BooksBorrowerInfosViewModel>, IBooksBorrowerInfosViewModelDal
    {
        public BooksBorrowerInfosViewModelRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
