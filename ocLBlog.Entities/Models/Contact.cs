using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class Contact:BaseHome
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        [EmailAddress]
        public string MailAddress { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
