using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Reference:BaseHome
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
    }
}
