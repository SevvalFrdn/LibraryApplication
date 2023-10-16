using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Concrete
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public DbSet<T> Table => _appDbContext.Set<T>();

        public bool Add(T t)
        {
            var entityEntry = Table.Add(t);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddAsync(T t)
        {
            var entityEntry = await Table.AddAsync(t);
            return entityEntry.State == EntityState.Added;
        }

        public bool Delete(T t)
        {
            var entityEntry = Table.Remove(t);
            return entityEntry.State == EntityState.Deleted;
        }


        public bool Update(T t)
        {
            var entityEntry = Table.Update(t);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() => await _appDbContext.SaveChangesAsync();

        public async Task<T> GetById(object id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<List<T>> GetList()
        {
            return await Table.ToListAsync();
        }

        public async Task<List<T>> GetListFromSqlRaw(string tableName, string BookId)
        {
            return await Table.FromSqlRaw($"SELECT * FROM {tableName} where BookId= @p0", BookId).ToListAsync();
        }
    }
}
