using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.DtoModels;
using static TopAct.WebApi.ControllerUtils;

namespace TopAct.WebApi.Controllers
{
    [ApiController]
    [Route("api/Contact")]
    [Authorize]
    public class ContactReaderController : Controller
    {
        private readonly IMediator _mediator;

        public ContactReaderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<QueryContactsResponseDto> GetContacts(
            [FromBody] QueryContactsRequestDto request
        )
        {
            var contacts = await _mediator.Send(
                new QueryContactsCommand(
                    request.Name,
                    request.Phone,
                    request.Email,
                    request.WebsiteUrl,
                    request.Notes,
                    request.Category
                )
            );
            if (contacts.Any() is false)
            {
                return new(new(), new());
            }
            var allTags = contacts
                .Where(x => x.Tags is not null)
                .SelectMany(x => x.Tags)
                .Distinct()
                .ToArray();
            var tagMap = new Dictionary<string, IList<Guid>>();
            foreach (var tag in allTags)
            {
                tagMap[tag] = contacts
                    .Where(x => x.Tags.Contains(tag))
                    .Select(x => x.Id)
                    .ToList();
            }
            tagMap[""] = contacts
                .Where(x => x.Tags.IsNullOrEmpty())
                .Select(x => x.Id)
                .ToList();
            return new(contacts.ToDictionary(x => x.Id), tagMap);
        }

        /// <summary>
        /// Gets the details of a contact by id
        /// </summary>
        /// <param name="id">The id of the contact</param>
        /// <returns>The contact</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            return await ResultWithNotFoundHandlingAsync(
                () => _mediator.Send(new GetContactCommand(id))
            );
        }
    }
}
