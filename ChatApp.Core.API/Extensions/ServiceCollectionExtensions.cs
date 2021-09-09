using ChatApp.Core.API.Database.Context;
using ChatApp.Core.API.Database.Repositories;
using ChatApp.Core.API.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Core.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddChatAppDbContext (this IServiceCollection services)
        {
            services.AddDbContext<ChatAppDbContext>(options => options.UseInMemoryDatabase("ChatAppDB"));

            services.AddScoped<DbContext, ChatAppDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConnectionRepository, ConnectionRepository>();

            return services;
        }

        public static IServiceCollection AddEfRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(EFRepository<>));

            return services;
        }
    }
}
