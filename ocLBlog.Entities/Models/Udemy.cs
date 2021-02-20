using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Udemy:BaseHome
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string WebsiteLink { get; set; }
    }
}
