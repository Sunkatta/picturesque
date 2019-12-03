using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class PictureView
    {
        public PictureView(
            string id,
            string img2base64,
            string categoryName,
            string categoryId
        )
        {
            Id = id;
            Img2Base64 = img2base64;
            CategoryName = categoryName;
            CategoryId = categoryId;
        }

        public string Id { get; set; }
        public string Img2Base64 { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
