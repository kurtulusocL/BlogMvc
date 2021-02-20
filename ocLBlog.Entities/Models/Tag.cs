﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Tag:BaseHome
    {
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
