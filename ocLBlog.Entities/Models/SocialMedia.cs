using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class SocialMedia : BaseHome
    {
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string MailAddress { get; set; }
        public string GitHub { get; set; }
        public string Udemy { get; set; }
    }
}
