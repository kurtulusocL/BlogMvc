using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Article:BaseHome
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Text { get; set; }
        public string Subtext { get; set; }
        public string Detail { get; set; }
        public int? Hit { get; set; }

        public int CategoryId { get; set; }
        public int TagId { get; set; }
        public string AdminId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Tag Tag { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Picture> Pictures { get; set; }

        public Article()
        {
            Hit = 0;
        }
    }
}
