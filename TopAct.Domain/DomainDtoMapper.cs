using System;
using System.Collections.Generic;
using System.Linq;
using TopAct.Common;
using TopAct.Domain.DtoModels;
using TopAct.Domain.Entities;

namespace TopAct.Domain
{
    public static class DomainDtoMapper
    {
        public static PhoneDto ToDto(this Phone phone) =>
            new(phone?.PhoneNo, phone?.PhoneNo.FormatPhone());
        public static string ToDto(this Email entity) => entity?.EmailAddress;
        public static string ToDto(this Address entity) => entity?.AddressName;
        public static string ToDto(this Category entity) => entity?.CategoryName;
        public static string ToDto(this Tag entity) => entity?.TagName;
        public static Guid ToDto(this ContactId entity) => entity.Value;

        public static IList<PhoneDto> ToDtos(this IList<Phone> entities) =>
            entities?.Select(ToDto).ToArray() ?? Array.Empty<PhoneDto>();
        public static IList<string> ToDtos(this IList<Email> entities) =>
            entities?.Select(ToDto).ToArray() ?? Array.Empty<string>();
        public static IList<string> ToDtos(this IList<Address> entities) =>
            entities?.Select(ToDto).ToArray() ?? Array.Empty<string>();
        public static IList<string> ToDtos(this IList<Category> entities) =>
            entities?.Select(ToDto).ToArray() ?? Array.Empty<string>();
        public static IList<string> ToDtos(this IList<Tag> entities) =>
            entities?.Select(ToDto).ToArray() ?? Array.Empty<string>();
        public static Dictionary<string, string> ToDtos(this IList<CustomField> entities) =>
            entities?.ToDictionary(x => x.Key, x => x.Value) ?? new();

        public static Phone ToDomainPhone(this string dto) => new(dto);
        public static Email ToDomainEmail(this string dto) => new(dto);
        public static Address ToDomainAddress(this string dto) => new(dto);
        public static Category ToDomainCategory(this string dto) => new(dto);
        public static Tag ToDomainTag(this string dto) => new(dto);
        public static CustomField ToDomainCustomField(this KeyValuePair<string, string> dto) => new(dto.Key, dto.Value);

        public static IList<Phone> ToDomainPhones(this IList<string> dtos) =>
            dtos?.Select(ToDomainPhone).ToArray() ?? Array.Empty<Phone>();
        public static IList<Email> ToDomainEmails(this IList<string> dtos) =>
            dtos?.Select(ToDomainEmail).ToArray() ?? Array.Empty<Email>();
        public static IList<Address> ToDomainAddresses(this IList<string> dtos) =>
            dtos?.Select(ToDomainAddress).ToArray() ?? Array.Empty<Address>();
        public static IList<Category> ToDomainCategories(this IList<string> dtos) =>
            dtos?.Select(ToDomainCategory).ToArray() ?? Array.Empty<Category>();
        public static IList<Tag> ToDomainTags(this IList<string> dtos) =>
            dtos?.Select(ToDomainTag).ToArray() ?? Array.Empty<Tag>();
        public static IList<CustomField> ToDomainCustomFields(this Dictionary<string, string> dtos) =>
            dtos?.Select(ToDomainCustomField).ToArray() ?? Array.Empty<CustomField>();

        public static QueryContactsResponseDto ToQueryContactsDto(this IList<Contact> contacts)
        {
            var contactItemDtos = contacts
                .Select(x =>
                    new QueryContactsItemDto(
                        x.Id.Value,
                        x.FirstName,
                        x.LastName,
                        x.OrganisationName,
                        x.WebsiteUrl,
                        x.Notes,
                        x.Phones.ToDtos(),
                        x.Emails.ToDtos(),
                        x.Categories.ToDtos(),
                        x.Tags.ToDtos()
                    )
                ).ToList();
            if (contacts.Any() is false)
            {
                return new(new(), new());
            }
            var allTags = contactItemDtos
                .Where(x => x.Tags is not null)
                .SelectMany(x => x.Tags)
                .Distinct()
                .ToArray();
            var tagMap = new Dictionary<string, IList<Guid>>();
            foreach (var tag in allTags)
            {
                tagMap[tag] = contactItemDtos
                    .Where(x => x.Tags.Contains(tag))
                    .Select(x => x.Id)
                    .ToList();
            }
            tagMap[""] = contactItemDtos
                .Where(x => x.Tags.IsNullOrEmpty())
                .Select(x => x.Id)
                .ToList();
            return new(contactItemDtos.ToDictionary(x => x.Id), tagMap);
        }
    }
}
