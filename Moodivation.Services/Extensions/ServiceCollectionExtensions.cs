using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moodivation.Data.Abstract;
using Moodivation.Data.Concrete;
using Moodivation.Data.Concrete.EntityFramework.Contexts;
using Moodivation.Services.Abstract;
using Moodivation.Services.AutoMapper;
using Moodivation.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<MoodivationContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            serviceCollection.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralMapper());
            }).CreateMapper());

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IProductService, ProductManager>();
            return serviceCollection;
        }
    }
}
