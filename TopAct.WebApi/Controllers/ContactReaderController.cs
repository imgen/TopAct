using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IList<ContactDto> GetContacts()
        {
            return null;
        }
    }

    public record ContactDto(string FirstName, string LastName,
        string OrganisationName,
        string WebsiteUrl);
}
