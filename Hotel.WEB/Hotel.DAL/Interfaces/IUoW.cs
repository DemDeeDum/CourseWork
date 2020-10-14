using System;
using Hotel.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Hotel.DAL.Interfaces
{
    public interface IUoW<TContext> : IDisposable
        where TContext: DbContext
    {
        /// <summary>
        /// Save changes in the database
        /// </summary>
        void Commit();
        /// <summary>
        /// Context field
        /// </summary>
        TContext context { get; }
        IRepository<Room, TContext> Rooms { get; }
        IRepository<RoomClass, TContext> RoomClasses { get; }
        IRepository<Booking, TContext> Bookings { get; }
        IRepository<UnluckyRequest, TContext> UnluckyRequests { get; }
        IRepository<Log,  TContext> Logs { get; }
        IDbSet<ApplicationUser> Users { get; }
    }
}
