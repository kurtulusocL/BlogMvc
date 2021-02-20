using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Skill:BaseHome
    {
        public string Name { get; set; }
        public int Degree { get; set; }

        public string AdminId { get; set; }
    }
}
