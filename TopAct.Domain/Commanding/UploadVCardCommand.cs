using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Commanding
{
    public record UploadVCardCommand(byte[] Bytes) : CommandBase<UploadVCardResponseDto>;
}
