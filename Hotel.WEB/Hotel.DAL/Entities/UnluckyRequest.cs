using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class UnluckyRequest : GreatEntity
    {
        public string RoomClassName { get; set; }
        public byte PeopleCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
