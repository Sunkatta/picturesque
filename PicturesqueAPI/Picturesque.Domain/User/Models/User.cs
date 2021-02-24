using Microsoft.AspNetCore.Identity;

namespace Picturesque.Domain
{
    public sealed class User : IdentityUser
    {
        public User() { }

        public User(bool isAdmin)
        {
            IsAdmin = isAdmin;
            IsBlocked = false;
        }
        
        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }
    }
}
