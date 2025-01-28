using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Authentication;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Users
{
    public class UsersService : IUsersService
    {
        private readonly IMongoRepository<ApplicationUser> _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersService(IMongoContext context, IAuthenticationService authenticationService, UserManager<ApplicationUser> userManager) { 
            _userRepository = context.GetRepository<ApplicationUser>();
            _authenticationService = authenticationService;
            _userManager = userManager;
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

        public PrimitiveBaseResponse<bool> deleteUserById(string id, string token)
        {
            try
            {
                ApplicationUser caller = _authenticationService.AuthenticateTokenAndGetUser(token);
            }
            catch (Exception ex)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, "USER NOT AUTHORIZED");
            }

            bool deleteResult = _userRepository.DeleteAsync(id).Result;

            if (deleteResult)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, null, $"user with id {id} deleted");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"user could not be deleted");
        }
        public PrimitiveBaseResponse<bool> updateUserById(string id, ApplicationUser user, string token)
        {
            bool updateResult = _userRepository.UpdateAsync(id, user).Result;

            if (updateResult)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, null, $"user with id {id} updated");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"user could not be deleted");
        }

        public PrimitiveBaseResponse<bool> updateUserPassword(string id, string oldPassword, string newPassword)
        {
            try
            {
                // Fetch the user from the repository
                ApplicationUser user = _userRepository.GetByIdAsync(id).Result;

                if (user == null)
                {
                    return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"No user with id {id} found");
                }

                // Validate the old password
                var isOldPasswordValid = _userManager.CheckPasswordAsync(user, oldPassword).Result;
                if (!isOldPasswordValid)
                {
                    return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, "Invalid old password");
                }

                // Update the password
                var result = _userManager.ChangePasswordAsync(user, oldPassword, newPassword).Result;
                if (!result.Succeeded)
                {
                    return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"Failed to update password: {result.Errors.FirstOrDefault()?.Description}");
                }

                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, null, "Password updated successfully");
            }
            catch (Exception ex)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, null, $"Error occurred: {ex.Message}");
            }
        }
    }
}
