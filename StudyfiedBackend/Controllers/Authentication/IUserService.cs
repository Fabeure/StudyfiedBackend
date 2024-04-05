using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Authentication
{
    public interface IUserService
    {
        public BaseResponse<List<ApplicationUser>> GetAllUsersAsync();
    }
}
