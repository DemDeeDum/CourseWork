using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Room : GreatEntity
    {
        public int Number { get; set; }
        public byte PeopleCount { get; set; }
        public int RoomClassId { get; set; }
        public bool IsAccessible { get; set; } = true;

        public Room()
        {
            Bookings = new List<Booking>();
        }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual RoomClass RoomClass { get; set; }
    }
}
