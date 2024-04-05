using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Authentication
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;

        public UserService(IMongoContext context)
        {
            _userRepository = context.GetRepository<ApplicationUser>();
        }

        public BaseResponse<List<ApplicationUser>> GetAllUsersAsync()
        { 
            var users = _userRepository.GetAllAsync().Result.ToList();
            return new BaseResponse<List<ApplicationUser>>(ResultCodeEnum.Success, users);
        }
    }

}
