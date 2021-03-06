using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TopAct.Domain.Commanding;
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
        public async Task<IActionResult> GetContactsAsync(
            [FromQuery] QueryContactsRequestDto request
        )
        {
            return await ResultWithNotFoundHandlingAsync(
                () => _mediator.Send(
                    new QueryContactsCommand(
                        request.Name,
                        request.Phone,
                        request.Email,
                        request.WebsiteUrl,
                        request.Notes,
                        request.Category
                    )
                )
            );
        }

        /// <summary>
        /// Gets the details of a contact by id
        /// </summary>
        /// <param name="id">The id of the contact</param>
        /// <returns>The contact</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactAsync(Guid id)
        {
            return await ResultWithNotFoundHandlingAsync(
                () => _mediator.Send(new GetContactCommand(id))
            );
        }
    }
}
