using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Profile
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateTime BegginingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string RoomClassName { get; set; }
        public string RoomColorToDisplay { get; set; }
        public int RoomNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DeadLine { get; set; }
        public byte PeopleCount { get; set; }
    }
}