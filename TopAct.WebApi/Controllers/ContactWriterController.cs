using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.WebApi.Controllers
{
    [ApiController]
    [Route("api/Contact")]
    [Authorize]
    public class ContactWriterController : Controller
    {
        private IContactsModule _contactsModule;

        public ContactWriterController(IContactsModule contactsModule)
        {
            _contactsModule = contactsModule;
        }

        [HttpPost]
        public async Task<CreateContactResponseDto> CreateContact([FromBody] CreateContactRequestDto request)
        {
            var contactId = await _contactsModule.ExecuteCommandAsync(new CreateContactCommand
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
            return new CreateContactResponseDto(
                    contactId,
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
                );
        }

        [HttpPut]
        public IActionResult EditContact([FromBody] EditContactRequestDto editContactRequest)
        {
            return Ok();
        }
    }
}
