﻿using System;
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

    public record CreateOrGetContactResponseDto(
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

    public record QueryContactsRequestDto(
        string Name,
        string Phone,
        string Email,
        string WebsiteUrl,
        string Notes,
        string[] Categories
    );

    public record QueryContactsDto(
        string FirstName,
        string LastName,
        string OrganisationName,
        string WebsiteUrl,
        string Notes
    );
}