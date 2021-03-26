using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TopAct.WebApi.Controllers
{
    [ApiController]
    [Route("Contact")]
    [Authorize]
    public class ContactReaderController
    {
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
