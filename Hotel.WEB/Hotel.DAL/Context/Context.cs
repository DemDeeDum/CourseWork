using System.Data.Entity;
using Hotel.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Hotel.DAL.Initializer;
namespace Hotel.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbContextInitializer()); //initializer
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<UnluckyRequest> UnluckyRequests { get; set; }
        public DbSet<Log> Logs { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
