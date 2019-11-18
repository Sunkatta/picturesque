using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.DB
{
    public static class InitialCategories
    {
        public static void Seed(PicturesqueDbContext ctx)
        {
            foreach (var category in GetInitialCategories())
            {
                ctx.Categories.Add(category);
            }
            ctx.SaveChanges();
        }

        public static List<Category> GetInitialCategories()
        {
            return new List<Category>()
            {
                new Category("Cats"),
                new Category("Dogs"),
                new Category("Cars")
            };
        }
    }
}
