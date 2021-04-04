using MediatR;
using System;
using System.Threading.Tasks;
using TopAct.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace TopAct.Domain
{
    public class ContactsModule : IContactsModule
    {
        private readonly IServiceProvider _serviceProvider;

        public ContactsModule(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            await mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(query);
        }
    }
}
