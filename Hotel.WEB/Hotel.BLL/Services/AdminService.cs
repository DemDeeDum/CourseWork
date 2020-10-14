namespace Hotel.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Enums;
    using Hotel.BLL.Interfaces;
    using Hotel.BLL.Models;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.Interfaces;
    using Hotel.DAL.UoW;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Service for serving Admin Controller functions.
    /// </summary>
    public class AdminService : IAdminService
    {
        private UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminService"/> class.
        /// </summary>
        /// <param name="db">Ninject resolved dependency.</param>
        public AdminService(IUoW<ApplicationDbContext> db)
        {
            this.Db = db ?? new UoW();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminService"/> class.
        /// </summary>
        public AdminService()
        {
            this.Db = new UoW();
        }

        /// <summary>
        /// Gets UserManager for interacting with identity users.
        /// </summary>
        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                if (this.userManager is null)
                {
                    this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                }

                return this.userManager;
            }
        }

        private IUoW<ApplicationDbContext> Db { get; }

        /// <summary>
        /// Gets users by a role.
        /// </summary>
        /// <param name="role">Role to search users by.</param>
        /// <returns>IEnumerable of users.</returns>
        public IEnumerable<ApplicationUserDTO> GetUsers(string role = null)
        {
            var list = new List<ApplicationUserDTO>();
            if (role is null)
            {
                list = this.Db.Users.Select(BLLService.BLLMapper.ApplicationUserMap).ToList();
            }
            else
            {
                list = this.Db.Users.ToList().Where(x => this.UserManager.IsInRole(x.Id, role))
                    .Select(BLLService.BLLMapper.ApplicationUserMap).ToList();
                list.ForEach(x => x.Role = role);
            }

            return list.OrderByDescending(x => x.UserName);
        }

        /// <summary>
        /// Gets users whose requests has not been answered.
        /// </summary>
        /// <returns>IEnumerable of users.</returns>
        public IEnumerable<UnluckyRequestDTO> GetAllUnluckyRequests()
        {
            return this.Db.UnluckyRequests.GetAll()
                .Select(BLLService.BLLMapper.UnluckyRequestMap);
        }

        /// <summary>
        /// Makes answer to a request.
        /// </summary>
        /// <param name="requestId">Id of current request.</param>
        /// <param name="roomId">Id of proposed room.</param>
        /// <param name="start">Starting date time.</param>
        /// <param name="finish">Ending date time.</param>
        /// <returns>Opeation Message about this movement.</returns>
        public OperationMessage AnswerToUnluckyRequest(
            int requestId,
            int roomId,
            DateTime start,
            DateTime finish)
        {
            var request = this.Db.UnluckyRequests.Get(requestId);

            // check for all errors
            if (request is null || request.IsDeleted == true)
            {
                return new OperationMessage() { Text = "Unpredicted problem reload page", IsPositive = false };
            }

            var room = this.Db.Rooms.Get(roomId);

            if (room is null || room.IsDeleted == true)
            {
                return new OperationMessage() { Text = "Unpredicted problem reload page", IsPositive = false };
            }

            if (BLLService.GetStatus(room, start, finish) != RoomStatus.AVAILABLE)
            {
                return new OperationMessage() { Text = "Do not change the date before cliking", IsPositive = false };
            }

            this.Db.Bookings.Create(new Booking()
            {
                DeadLine = DateTime.Today.AddDays(2),
                BeginningDate = start,
                EndingDate = finish,
                User = request.User,
                Room = room,
                IsConfirmed = false,
            });
            this.Db.UnluckyRequests.Get(requestId).IsDeleted = true;
            this.Db.Commit();
            return new OperationMessage() { Text = "Proposition has been sent!", IsPositive = true };
        }

        /// <summary>
        /// Gets possible people counts in vacant rooms.
        /// </summary>
        /// <returns>IEnumerable of strings.</returns>
        public IEnumerable<string> GetPossiblePeopleCounts()
        {
            return (from i in this.Db.Rooms.GetAll().ToList()
                    where i.IsAccessible
                    orderby i.PeopleCount
                    select i.PeopleCount.ToString()).Distinct();
        }

        public OperationMessage AddToRole(string UserName, string Role)
        {
            var user = this.UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, Role))
                return new OperationMessage() { Text = $"This user is {Role}", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = "Unexpected user", IsPositive = false };

            if (UserManager.AddToRole(user.Id, Role).Succeeded)
                return new OperationMessage() { Text = $"The user {UserName} has just got the {Role} permissions", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false }; 
        }

        public OperationMessage DownGradeFromStaff(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, "user"))
                return new OperationMessage() { Text = $"This user is user", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "staff").Succeeded)
                return new OperationMessage() { Text = "Unexpected user", IsPositive = false };

            if (UserManager.AddToRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = $"The user {UserName} has just lost the staff permissions", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public OperationMessage DeleteUser(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, "deleted"))
                return new OperationMessage() { Text = "This user is deleted already", IsPositive = false };

            UserManager.RemoveFromRole(user.Id, "user");
            UserManager.RemoveFromRole(user.Id, "admin");
            UserManager.RemoveFromRole(user.Id, "staff");

            if (UserManager.AddToRole(user.Id, "deleted").Succeeded)
                return new OperationMessage() { Text = $"User {UserName} has just been deleted", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public OperationMessage RestoreUser(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (!UserManager.IsInRole(user.Id, "deleted"))
                return new OperationMessage() { Text = "This user is not deleted", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "deleted").Succeeded)
                return new OperationMessage() { Text = "Unexpected error", IsPositive = false };

            if (UserManager.AddToRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = $"User {UserName} has just been restored", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public IEnumerable<string> GetPossibleRoomClassNames()
        {
            return (from i in Db.Rooms.GetAll().ToList()
                    where i.IsAccessible
                    orderby i.RoomClass.Price
                    select i.RoomClass.Name).Distinct();
        }

        public IEnumerable<RoomDTO> SearchRooms(string ClassName, byte PeopleCount,
            DateTime start, DateTime finish)
        {
            return Db.Rooms.GetAll().ToList().Where(x => 
            x.PeopleCount == PeopleCount
            && x.RoomClass.Name == ClassName
            && BLLService.GetStatus(x, start,finish) == RoomStatus.AVAILABLE)
                .Select(BLLService.BLLMapper.RoomMap);
        }

        public IEnumerable<LogDTO> GetLastLogs()
        {
            var count = Db.Logs.GetCount();
            if (count == 0)
                return new List<LogDTO>();
            return Db.Logs.GetRange(count <= 200 ? 0 : count - 200, count - 1)
                .Select(BLLService.BLLMapper.LogMap);
        }

        public IEnumerable<LogDTO> GetLogs(string userName, DateTime? start
            , DateTime? finish, bool? searchExept, string URL)
        {
            var list = Db.Logs.GetAll();

            if (!(URL is null))
                list = list.Where(x => x.URL.Contains(URL));

            if (!(userName is null))
                list = list.Where(x => x.UserName.Contains(userName));

            if (start.HasValue)
                list = list.Where(x => x.Date > start);

            if (finish.HasValue)
                list = list.Where(x => x.Date < finish);

            if (searchExept.HasValue && searchExept.Value)
                list = list.Where(x => x.Exeption != "-");

            return list.Select(BLLService.BLLMapper.LogMap);
        }
        public void DeleteFromAdminRole(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (UserManager.RemoveFromRole(user.Id, "admin").Succeeded)
                UserManager.AddToRole(user.Id, "user");
        }
        public void Dispose()
        {
            Db.Dispose();
        }


    }
}
