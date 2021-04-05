using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Handlers
{
    public class QueryContactsCommandHandler : ICommandHandler<QueryContactsCommand, IList<QueryContactsDto>>
    {
        public Task<IList<QueryContactsDto>> Handle(QueryContactsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
