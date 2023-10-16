using AutoMapper;
using LibraryApplication.DataAccessLayer.DTO;
using LibraryApplication.EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer.Mapping
{
    public class BooksMapping : Profile
    {
        public BooksMapping()
        {
            CreateMap<Book, AddBookImage>()
                .ReverseMap();
        }
    }
}
