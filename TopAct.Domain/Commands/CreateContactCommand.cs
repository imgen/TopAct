using System;
using System.Collections.Generic;
using TopAct.Domain.Contracts;

namespace TopAct.Domain.Commands
{
    public record CreateContactCommand(
        string FirstName,
        string LastName,
        string OrganisationName,
        string WebsiteUrl,
        string Notes,
        IList<string> Phones,
        IList<string> Addresses,
        IList<string> Emails,
        IList<string> Categories,
        IList<string> Tags,
        Dictionary<string, string> CustomFields) : CommandBase<Guid>;
}
