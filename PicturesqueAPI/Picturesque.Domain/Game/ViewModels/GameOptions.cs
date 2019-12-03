using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class GameOptions
    {
        public GameOptions(
            List<CategoryView> categories,
            string[] difficulties
            )
        {
            Categories = categories;
            Difficulties = difficulties;
        }

        public List<CategoryView> Categories { get; set; }
        public string[] Difficulties { get; set; }
    }
}
