﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Models
{
    public class GameOptions
    {
        public List<Category> Categories { get; set; }
        public string[] Difficulties { get; set; }
    }
}
