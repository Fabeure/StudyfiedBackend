using System.Runtime.Serialization;

namespace StudyfiedBackend.BaseResponse
{
    public class BaseResponse : IBaseResponse
    {
        [DataMember]
        public ResultCodeEnum ResultCode { get; set; }
        [DataMember]
        public string? UserMessage { get; set; }

        public BaseResponse(ResultCodeEnum code, string userMessage = "")
        {
            ResultCode = code;
            UserMessage = userMessage;
        }
        public BaseResponse()
        { }

        public bool IsSuccess => this.IsSuccess();
        public bool IsFailed => this.IsFailed();
    }

    public class BaseResponse<T> : BaseResponse
        where T : class
    {
        public BaseResponse() { }

        public BaseResponse(ResultCodeEnum code, T? resultItem, string userMessage="")
            : base(code)
        {
            ResultItem = resultItem;
            UserMessage = userMessage;
        }

        [DataMember]
        public T? ResultItem { get; set; }

    }

    public class PrimitiveBaseResponse<T> : BaseResponse
        where T : struct
    {
        public PrimitiveBaseResponse() { }

        public PrimitiveBaseResponse(ResultCodeEnum code, T? resultItem, string userMessage="")
          : base(code)
        {
            ResultItem = resultItem;
            UserMessage = userMessage;
        }

        [DataMember]
        public T? ResultItem { get; set; }
    }
}
