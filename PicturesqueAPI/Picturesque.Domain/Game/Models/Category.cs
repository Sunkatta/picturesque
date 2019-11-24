using Picturesque.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class Category
    {
        private CustomId _id;

        public Category() { }

        public Category(
            string name,
            CustomId id = null
            )
        {
            Name = name;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string Name { get; set; }

        public ICollection<PicturesCategories> Pictures { get; private set; } =
            new HashSet<PicturesCategories>();
    }
}
