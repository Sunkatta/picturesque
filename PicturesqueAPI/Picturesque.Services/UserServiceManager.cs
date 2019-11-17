using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class UserServiceManager : IUserServiceManager
    {
        private readonly PicturesqueDbContext _ctx;

        public UserServiceManager(PicturesqueDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateUserAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
        }
    }
}
