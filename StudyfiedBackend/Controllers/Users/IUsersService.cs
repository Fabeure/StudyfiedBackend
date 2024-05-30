using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public interface IUsersService
    {
        public BaseResponse<ApplicationUser> getUserById(string id);
    }
}
