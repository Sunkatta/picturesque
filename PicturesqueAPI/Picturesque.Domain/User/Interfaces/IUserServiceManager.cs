using Picturesque.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IUserServiceManager
    {
        Task CreateUserAsync(User user, string password);
        
        Task<string> GenerateJWTAsync(LoginUserEntry login);
        
        Task<User> GetRawUserByEmailAsync(string email);
        
        Task<User> GetRawUserByIdAsync(string id);

        Task<IEnumerable<UserView>> GetAll();

        Task<bool> BlockAsync(string id);

        Task<bool> IsBlocked(string email);

        Task ConfirmEmailAsync(string email, string code);
    }
}
