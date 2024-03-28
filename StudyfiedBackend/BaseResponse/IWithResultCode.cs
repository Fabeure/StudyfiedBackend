using StudyfiedBackend.BaseResponse;

namespace StudyfiedBackend.BaseResponse
{
    public interface IWithResultCode
    {
        ResultCodeEnum ResultCode { get; set; }
    }

    public static class IWithResultCodeExtensions
    {
        public static bool IsSuccess(this IWithResultCode response)
            => response.ResultCode == ResultCodeEnum.Success;

        public static bool IsFailed(this IWithResultCode response)
            => response.ResultCode == ResultCodeEnum.Failed;
    }
}