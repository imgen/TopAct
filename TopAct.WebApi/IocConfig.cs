using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopAct.Domain;
using TopAct.Domain.Contracts;
using TopAct.Domain.Handlers;
using TopAct.Domain.Repositories;
using TopAct.Infrastructure.Dal;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi
{
    public static class IocConfig
    {
        public static void AddTopActServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbFilePath = configuration.GetValue("DbFilePath", DefaultDbFilePath);
            services.AddTransient(_ => new DbContext(dbFilePath));
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactsModule, ContactsModule>();
            services.AddMediatR(typeof(CreateContactCommandHandler));
        }
    }
}
