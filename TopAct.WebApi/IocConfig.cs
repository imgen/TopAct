using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopAct.Domain.Contracts;
using TopAct.Domain.Commanding;
using TopAct.Domain.Repositories;
using TopAct.Infrastructure.Dal;

namespace TopAct.WebApi
{
    public static class IocConfig
    {
        public static void AddTopActServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbFilePath = configuration.GetDbFilePath();
            services.AddTransient(_ => new DbContext(dbFilePath));
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddMediatR(typeof(CreateContactCommandHandler));
        }
    }
}
