using LibraryApplication.DataAccessLayer.Abstract;
using LibraryApplication.DataAccessLayer.Concrete;
using LibraryApplication.DataAccessLayer.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DataAccessLayer
{
    public static class ServiceRegistiration
    {
        public static void AddDataAccessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IBookDal, BookRepository>();
            services.AddScoped<IBorrowerInfoDal, BorrowerInfoRepository>();
            services.AddScoped<IBooksBorrowerInfosViewModelDal, BooksBorrowerInfosViewModelRepository>();
        }
    }
}
