﻿using LibraryApplication.EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer.Abstract
{
    public interface IBookDal : IGenericDal<Book>
    {
    }
}
