using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public class UsersService : IUsersService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;

        public UsersService(IMongoContext context) { 
            _userRepository = context.GetRepository<ApplicationUser>();
        }

        public BaseResponse<ApplicationUser> getUserById(string id)
        {
            ApplicationUser user = _userRepository.GetByIdAsync(id).Result;

            if (user != null)
            {
                return new BaseResponse<ApplicationUser>(ResultCodeEnum.Success, user, $"Succesfully fetched user {user.Id}");
            }
            return new BaseResponse<ApplicationUser>(ResultCodeEnum.Failed, null, $"No user with id {id} found");
        }
    }
}
