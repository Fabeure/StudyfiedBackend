
namespace StudyfiedBackend.BaseResponse
{
    public interface IBaseResponse : IWithResultCode
    {
        ResultCodeEnum ResultCode { get; set; }
    }
}