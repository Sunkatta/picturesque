using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Models
{
    public class Picture
    {
        public string Id { get; set; }
        public string Img2Base64 { get; set; }
        public bool IsVisible { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
