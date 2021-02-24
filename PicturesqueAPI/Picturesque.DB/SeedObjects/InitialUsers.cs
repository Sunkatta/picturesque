using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.DB
{
    public static class InitialUsers
    {
        public static void Seed(PicturesqueDbContext ctx)
        {
            foreach (var user in GetInitialUsers())
            {
                ctx.Users.Add(user);
            }
            ctx.SaveChanges();
        }

        public static List<User> GetInitialUsers()
        {
            User sunkatta = new User()
            {
                Email = "sunni98@abv.bg",
                PasswordHash = "123456",
                UserName = "Sunkatta",
                IsAdmin = true,
            };
            User pesho = new User()
            {
                Email = "pesho@gmail.com",
                PasswordHash = "pesho",
                UserName = "Pesho",
                IsAdmin = false,
            };
            return
                new List<User>() { sunkatta, pesho };
        }
    }
}
