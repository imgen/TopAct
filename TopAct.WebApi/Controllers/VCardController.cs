using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopAct.Common;
using TopAct.Domain.Commanding;
using TopAct.Domain.DtoModels;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class VCardController : Controller
    {
        private readonly IMediator _mediator;

        public VCardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Download")]
        public async Task<IActionResult> DownloadAsVCard([FromQuery] string fileName,
            [FromBody] IList<Guid> contactIds)
        {
            return await ControllerUtils.ResultWithNotFoundHandlingAsync(async () =>
            {
                var vcardStream = await _mediator.Send(new DownloadAsVCardCommand(contactIds));
                if (fileName.IsNullOrWhiteSpace())
                {
                    fileName = "contacts";
                }

                return File(vcardStream, VCardContentType, fileName);
            });
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<UploadVCardResponseDto> UploadVCardAsync(IFormFile vCardFile)
        {
            var contentType = vCardFile.ContentType?.ToLowerInvariant();
            if (ValidVCardContentTypes.Contains(contentType) is false)
            {
                throw new InvalidOperationException($"Only accepts VCard file");
            }
            if (vCardFile.Length <= 0)
            {
                throw new InvalidOperationException("Only accepts non-Empty VCard file");
            }
            var bytes = await vCardFile.GetBytesAsync();

            return await _mediator.Send(new UploadVCardCommand(bytes));
        }
    }
}
