using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picturesque.DB
{
    public static class InitialPicturesCategories
    {
        public static void Seed(PicturesqueDbContext ctx)
        {
            foreach (var pictureCategory in GetInitialPicturesCategories(ctx))
            {
                ctx.Add(pictureCategory);
            }
            ctx.SaveChanges();
        }

        private static List<PicturesCategories> GetInitialPicturesCategories(PicturesqueDbContext ctx)
        {
            string catsCategoryId = ctx.Categories.FirstOrDefault(c => c.Name == "Cats").Id;
            List<Picture> catPictures = ctx.Pictures.ToList();

            return new List<PicturesCategories>()
            {
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[0].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[1].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[2].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[3].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[4].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[5].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[6].Id
                    ),
                new PicturesCategories(
                    catsCategoryId,
                    catPictures[7].Id
                    ),
            };
        }
    }
}
