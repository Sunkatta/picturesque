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
            User sunkatta =
                 new
                    User(
                            "sunni98@abv.bg",
                            "Sunkatta",
                            "123456",
                            true
                        );
            User pesho =
                new
                    User(
                            "peshokelesho@gmail.com",
                            "Pesho",
                            "pesho",
                            false
                        );
            return
                new List<User>() { sunkatta, pesho };
        }
    }
}
