using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Booking : GreatEntity
    {
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DeadLine { get; set; }

        public int RoomId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Room Room { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
