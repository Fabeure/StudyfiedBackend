using System.Runtime.Serialization;

namespace StudyfiedBackend.BaseResponse
{
    public enum ResultCodeEnum
    {
        [EnumMember]
        Success = 0,

        [EnumMember]
        PartialSuccess = 2,

        [EnumMember]
        Failed = 1,

        [EnumMember]
        Unauthorized = 10,

        [EnumMember]
        NotAdmin = 20,

        [EnumMember]
        NotAllowed = 30,

        [EnumMember]
        NotAllowedToUseApp = 31,

        [EnumMember]
        InvalidArgument = 40,

        [EnumMember]
        BadRequest = 400,

        [EnumMember]
        NotFound = 404,
    }
}