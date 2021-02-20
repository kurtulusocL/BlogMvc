using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Entities.Models
{
    public class LearnPrice:BaseHome
    {
        public string SiteKind { get; set; }
        public string SiteAttribute { get; set; }
        public string AreasOfUsage { get; set; }
        public string ChoiseLanguage { get; set; }
        public DateTime FinishingTime { get; set; }
        public string ForExpamle { get; set; }
        public string YourNameSurname { get; set; }
        public string YourPhoneNumber { get; set; }
        public string YourMailAddress { get; set; }
        public string YourCityandCountry { get; set; }
    }
}
