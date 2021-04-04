using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TopAct.Domain.Contracts;

namespace TopAct.WebApi.Controllers
{
    [ApiController]
    [Route("api/Contact")]
    [Authorize]
    public class ContactReaderController : Controller
    {
        private readonly IContactsModule _contactsModule;

        public ContactReaderController(IContactsModule contactsModule)
        {
            _contactsModule = contactsModule;
        }

        [HttpGet]
        public IList<ContactDto> GetContacts()
        {
            return new[]
            {
                new ContactDto("Bill", "Shu"),
                new ContactDto("Mark", "Cal"),
                new ContactDto("Make", "Up")
            };
        }
    }

    public record ContactDto(string FirstName, string LastName);
}
