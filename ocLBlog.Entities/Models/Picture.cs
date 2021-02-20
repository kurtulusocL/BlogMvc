using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Picture:BaseHome
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public int? ArticleId { get; set; }
        public string AdminId { get; set; }

        public virtual Article Article { get; set; }
    }
}
