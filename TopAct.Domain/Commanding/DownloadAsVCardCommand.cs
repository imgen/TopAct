using System;
using System.Collections.Generic;
using TopAct.Domain.Contracts;

namespace TopAct.Domain.Commanding
{
    public record DownloadAsVCardCommand(IList<Guid> ContactIds) : CommandBase<byte[]>;
}
