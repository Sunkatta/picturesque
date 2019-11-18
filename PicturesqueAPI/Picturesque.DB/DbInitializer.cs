using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picturesque.DB
{
    public static class DbInitializer
    {
        public static void Initialize(PicturesqueDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any()) InitialUsers.Seed(context);
            if (!context.Categories.Any()) InitialCategories.Seed(context);
        }
    }
}
