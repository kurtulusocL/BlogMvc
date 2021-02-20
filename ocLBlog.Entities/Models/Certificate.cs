using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Certificate:BaseHome
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DateTime CertificateDate { get; set; }
        public string FromWhere { get; set; }
        public string Photo { get; set; }
    }
}
