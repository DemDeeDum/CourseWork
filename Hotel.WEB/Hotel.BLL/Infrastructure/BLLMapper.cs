using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;
using Hotel.DAL.Entities;
using Hotel.BLL.DTOs;
using Hotel.BLL.Models;
using Hotel.BLL.IdentityLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hotel.BLL.Infrastructure
{
    public class BLLMapper
    {
        private UserManager<ApplicationUser> userManager;
        /// <summary>
        /// to interact with identity framework users
        /// </summary>
        public UserManager<ApplicationUser> UserManager 
        {
            get
            {
                if (userManager is null)
                    userManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>
                (new ApplicationDbContext()));
                return userManager;
            }
        }
        public RoomDTO RoomMap(Room obj)
        {
            return new RoomDTO()
            {
                PeopleCount = obj.PeopleCount,
                RoomClassName = obj.RoomClass.Name,
                Number = obj.Number,
                ClassDisplayColor = obj.RoomClass.ColorToDisplay,
                Price = obj.RoomClass.Price * obj.PeopleCount,
                Id = obj.Id
            };
        }


        public RoomClassDTO RoomClassMap(RoomClass obj)
        {
            return new RoomClassDTO()
            {
                Name = obj.Name,
                Price = obj.Price,
                DisplayColor = obj.ColorToDisplay,
                Info = obj.Info
            };
        }

        public RoomWholeInfo RoomWholeInfoMap(Room obj)
        {
            return new RoomWholeInfo()
            {
                Id = obj.Id,
                Number = obj.Number,
                PeopleCount = obj.PeopleCount,
                RoomClassInfo = obj.RoomClass.Info,
                RoomClassName = obj.RoomClass.Name,
                Price = obj.PeopleCount * obj.RoomClass.Price,
                RoomClassColorToDisplay = obj.RoomClass.ColorToDisplay,
                RoomClassImageAddress = obj.RoomClass.AddressImage
            };
        }

        public BookingDTO BookingMap(Booking obj)
        {
            return new BookingDTO()
            {
                BegginingTime = obj.BeginningDate,
                EndingTime = obj.EndingDate,
                DeadLine = obj.DeadLine,
                IsConfirmed = obj.IsConfirmed,
                RoomClassName = obj.Room.RoomClass.Name,
                RoomColorToDisplay = obj.Room.RoomClass.ColorToDisplay,
                RoomNumber = obj.Room.Number,
                PeopleCount = obj.Room.PeopleCount,
                Id = obj.Id
            };

        }

        public UnluckyRequestDTO UnluckyRequestMap(UnluckyRequest obj)
        {
            return new UnluckyRequestDTO()
            {
                UnluckyRequestId = obj.Id,
                FinishDate = obj.FinishDate,
                PeopleCount = obj.PeopleCount,
                RoomClassName = char.ToUpper(obj.RoomClassName[0])
                + obj.RoomClassName.Substring(1),
                StartDate = obj.StartDate,
                UserEmail = UserManager.GetEmail(obj.ApplicationUserId),
                UserName = UserManager.FindById(obj.ApplicationUserId).UserName
            };
        }

        public ApplicationUserDTO ApplicationUserMap(ApplicationUser obj)
        {
            var user = UserManager.FindById(obj.Id);
            return new ApplicationUserDTO()
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }
        
        public Log LogMap(LogDTO obj)
        {
            return new Log()
            {
                BrowserName = obj.BrowserName,
                BrowserVersion = obj.BrowserVersion,
                Exeption = obj.Exeption,
                IsMobile = obj.IsMobile,
                JavasriptVersion = obj.JavasriptVersion,
                Platform = obj.Platform,
                URL = obj.URL,
                UserName = obj.UserName,
                Date = obj.Date
            };
        }

        public LogDTO LogMap(Log obj)
        {
            return new LogDTO()
            {
                BrowserName = obj.BrowserName,
                BrowserVersion = obj.BrowserVersion,
                Exeption = obj.Exeption,
                IsMobile = obj.IsMobile,
                JavasriptVersion = obj.JavasriptVersion,
                Platform = obj.Platform,
                URL = obj.URL,
                UserName = obj.UserName,
                Date = obj.Date
            };
        }
    }
}


