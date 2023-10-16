using LibraryApplication.EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> GetById(object id);
        bool Add(T t);
        bool Update(T t);
        bool Delete(T t);
        Task<bool> AddAsync(T t);
        Task<int> SaveAsync();
        Task<List<T>> GetListFromSqlRaw(string tableName, string BookId);

        //Task<bool> AddRangeAsync(IEnumerable<T> t);
        //bool AddRange(IEnumerable<T> t);
        //bool RemoveRange(IEnumerable<T> t);
        //bool AddOrUpdate(T t);
        //Task<int> DeleteByIdsAsync(IEnumerable<Guid> ids);
    }
}
