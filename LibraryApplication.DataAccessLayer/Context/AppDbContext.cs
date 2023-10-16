using LibraryApplication.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowerInfo> BorrowerInfos { get; set; }
        public DbSet<BooksBorrowerInfosViewModel> BooksBorrowerInfosViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksBorrowerInfosViewModel>(c =>
            {
                c.HasNoKey();
                c.ToView("BooksBorrowerInfosViewModel");
            });
        }
    }
}
