using System;
using System.Collections.Generic;

namespace TopAct.Domain.DtoModels
{
    public record CreateOrEditContactRequestDto(
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
        Dictionary<string, string> CustomFields
    );

    public record CreateContactResponseDto(
        Guid Id,
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
        Dictionary<string, string> CustomFields
    );

    public record EditContactRequestDto(
        Guid Id,
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
        Dictionary<string, string> CustomFields
    );
}
