using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Repositories;

namespace Hotel.DAL.UoW
{
    public class UoW : IUoW<ApplicationDbContext>
    {
        public ApplicationDbContext context { get; }
        public IRepository<Room, ApplicationDbContext> Rooms { get; }
        public IRepository<RoomClass, ApplicationDbContext> RoomClasses { get; }
        public IRepository<Booking, ApplicationDbContext> Bookings { get;}
        public IRepository<UnluckyRequest, ApplicationDbContext> UnluckyRequests { get; }
        public IRepository<Log, ApplicationDbContext> Logs { get; }
        public IDbSet<ApplicationUser> Users => context.Users;
        public IDbSet<IdentityRole> Roles => context.Roles;
        public UoW()
        {
            //Creating repositories
            context = new ApplicationDbContext();
            Rooms = new Repository<Room, ApplicationDbContext>(context);
            RoomClasses = new Repository<RoomClass, ApplicationDbContext>(context);
            Bookings = new Repository<Booking, ApplicationDbContext>(context);
            UnluckyRequests = new Repository<UnluckyRequest, ApplicationDbContext>(context);
            Logs = new Repository<Log, ApplicationDbContext>(context);
        }

        //Save changes
        public void Commit()
        {
            context.SaveChanges();
        }

        //Dispose
        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
