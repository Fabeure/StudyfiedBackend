using System.Runtime.Serialization;

namespace StudyfiedBackend.BaseResponse
{
    public class BaseResponse : IBaseResponse
    {
        [DataMember]
        public ResultCodeEnum ResultCode { get; set; }

        public BaseResponse(ResultCodeEnum code)
        {
            ResultCode = code;
        }
        public BaseResponse()
        { }

        public bool IsSuccess => this.IsSuccess();
        public bool IsFailed => this.IsFailed();

        public static BaseResponse<T> FromResult<T>(T resultItem, ResultCodeEnum code = ResultCodeEnum.Success)
            where T : class
            => new BaseResponse<T>(code, resultItem);

        public static BaseResponse Success()
            => new BaseResponse(ResultCodeEnum.Success);
    }

    public class BaseResponse<T> : BaseResponse
        where T : class
    {
        public BaseResponse() { }

        public BaseResponse(ResultCodeEnum code, T resultItem)
            : base(code)
        {
            ResultItem = resultItem;
        }

        [DataMember]
        public T ResultItem { get; set; }

        public static BaseResponse<T> FromResult(T resultItem,  ResultCodeEnum code = ResultCodeEnum.Success)
            => new BaseResponse<T>(code, resultItem);

    }

    public class PrimitiveBaseResponse<T> : BaseResponse
        where T : struct
    {
        public PrimitiveBaseResponse() { }

        public PrimitiveBaseResponse(ResultCodeEnum code, T resultItem)
          : base(code)
        {
            ResultItem = resultItem;
        }

        [DataMember]
        public T ResultItem { get; set; }
    }
}
