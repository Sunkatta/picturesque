using Picturesque.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class Picture
    {
        private CustomId _id;

        public Picture() { }

        public Picture(
            string img2base64,
            CustomId id = null
            )
        {
            Img2Base64 = img2base64;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string Img2Base64 { get; set; }
    }
}
