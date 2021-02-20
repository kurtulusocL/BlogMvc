using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class About:BaseHome
    {
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
