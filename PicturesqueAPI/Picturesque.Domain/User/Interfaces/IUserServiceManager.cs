using Picturesque.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IUserServiceManager
    {
        Task CreateUserAsync(User user, string password);
        
        Task<string> GenerateJWTAsync(LoginUserEntry login);
        
        Task<User> GetRawUserByIdAsync(string id);

        Task<IEnumerable<UserView>> GetAll();

        Task<bool> BlockAsync(string id);

        Task<bool> IsBlocked(string email);

        Task<bool> ConfirmEmailAsync(string email, string code);

        Task<bool> CheckIfUserExistsByEmail(string email);

        Task<bool> CheckIfUserExistsByUsername(string username);

        Task<bool> IsEmailConfiemd(string email);

        Task ResendConfirmationEmail(string email);

        Task ForgotPassword(string email);

        Task<bool> ResetPassword(string email, string newPassword, string code);

        Task<ProfileView> GetProfile(string userId);
    }
}
