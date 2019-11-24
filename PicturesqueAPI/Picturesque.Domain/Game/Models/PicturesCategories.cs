using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class PicturesCategories
    {
        public PicturesCategories() { }

        public PicturesCategories(
            string categoryId,
            string pictureId
            )
        {
            CategoryId = categoryId;
            PictureId = pictureId;
        }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}
