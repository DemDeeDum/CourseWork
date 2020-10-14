using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Log : GreatEntity
    { 
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
        public bool IsMobile { get; set; }
        public string Platform { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string JavasriptVersion { get; set; }
        public string Exeption { get; set; }
    }
}
