using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TopAct.Domain;
using TopAct.Domain.Commanding;
using TopAct.Domain.DtoModels;
using static TopAct.WebApi.ControllerUtils;

namespace TopAct.WebApi.Controllers
{
    [ApiController]
    [Route("api/Contact")]
    [Authorize]
    public class ContactWriterController : Controller
    {
        private readonly IMediator _mediator;

        public ContactWriterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreateOrGetContactResponseDto> CreateContact([FromBody] CreateOrEditContactRequestDto request)
        {
            var contactId = await _mediator.Send(new CreateContactCommand
                (
                    request.FirstName,
                    request.LastName,
                    request.OrganisationName,
                    request.WebsiteUrl,
                    request.Notes,
                    request.Phones,
                    request.Addresses,
                    request.Emails,
                    request.Categories,
                    request.Tags,
                    request.CustomFields
                )
            );
            return new CreateOrGetContactResponseDto(
                    contactId,
                    request.FirstName,
                    request.LastName,
                    request.OrganisationName,
                    request.WebsiteUrl,
                    request.Notes,
                    request.Phones?
                        .Select(x => new PhoneDto(x, x.FormatPhone()))
                        .ToArray() ??
                        Array.Empty<PhoneDto>(),
                    request.Addresses ?? Array.Empty<string>(),
                    request.Emails ?? Array.Empty<string>(),
                    request.Categories ?? Array.Empty<string>(),
                    request.Tags ?? Array.Empty<string>(),
                    request.CustomFields ?? new()
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContact(Guid id,
            [FromBody] CreateOrEditContactRequestDto request)
        {
            return await WithNotFoundHandlingAsync(
                () =>
                    _mediator.Send(new EditContactCommand
                        (
                            id,
                            request.FirstName,
                            request.LastName,
                            request.OrganisationName,
                            request.WebsiteUrl,
                            request.Notes,
                            request.Phones,
                            request.Addresses,
                            request.Emails,
                            request.Categories,
                            request.Tags,
                            request.CustomFields
                        )
                )
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            return await WithNotFoundHandlingAsync(
                () => _mediator.Send(new DeleteContactCommand(id))
            );
        }
    }
}
