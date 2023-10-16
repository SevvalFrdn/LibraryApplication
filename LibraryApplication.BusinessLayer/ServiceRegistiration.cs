using LibraryApplication.BusinessLayer.Services;
using LibraryApplication.BusinessLayer.Services.IServices;
using LibraryApplication.DataAccessLayer.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.BusinessLayer
{
    public static class ServiceRegistiration
    {
        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceRegistiration));

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowerInfoService, BorrowerInfoService>();
            services.AddScoped<IBooksBorrowerInfosViewModelService, BooksBorrowerInfosViewModelService>();
        }
    }
}
