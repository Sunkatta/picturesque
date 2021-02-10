namespace Picturesque.Domain
{
    public sealed class UserView
    {
        public UserView(
            string id,
            string email,
            string username,
            bool isBlocked,
            bool isAdmin
        )
        {
            Id = id;
            Email = email;
            Username = username;
            IsBlocked = isBlocked;
            IsAdmin = isAdmin;
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsAdmin { get; set; }
    }
}
