using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class RoomClass : GreatEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public float Price { get; set; }
        public string ColorToDisplay { get; set; }
        public string AddressImage { get; set; }

        public RoomClass()
        {
            Rooms = new List<Room>();
        }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
