﻿using Hotel.BLL.DTOs;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Models;
using Hotel.BLL.Enums;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.Entities;

namespace Hotel.BLL.Services
{
    public class StaffService : IStaffService
    {
        private IUoW<ApplicationDbContext> db { get; }
        public StaffService(IUoW<ApplicationDbContext> db)
        {
            this.db = db ?? new UoW();
        }

        public StaffService()
        {
            db = new UoW();
        }

        public bool IsRoomNumber(int number)
        {
            return db.Rooms.GetAll().Where(x => x.Number == number).Count() != 0;
        }

        public OperationMessage AddNewRoom(RoomDTO room)
        {
            if (room is null)
                return new OperationMessage()
                { IsPositive = false, Text = "Parameter \"room\" was null" };

            if (IsRoomNumber(room.Number))
                return new OperationMessage()
                { IsPositive = false, Text = $"Room which has number {room.Number} exists" };

            db.Rooms.Create(new DAL.Entities.Room()
            {
                Number = room.Number,
                IsAccessible = room.AppartmentStatus != Enums.RoomStatus.INACCESSIBLE,
                PeopleCount = room.PeopleCount,
                RoomClass = db.RoomClasses.set.Where(x => x.Name == room.RoomClassName).Single()
            });
            db.Commit();
            return new OperationMessage() { Text = $"Room №{room.Number} has been added", IsPositive = true };

        }


        public IEnumerable<string> GetPossibleRoomClassNames()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    orderby i.RoomClass.Price
                    select i.RoomClass.Name).Distinct();
        }

        public IEnumerable<RoomDTO> GetRooms(int number, string className
            , int peopleCount, bool inAccessible)
        {
            IEnumerable<Room> list = db.Rooms.GetAll().OrderBy(x => x.Number).ToList();
            if (number != 0)
            {
                list = list.Where(x => x.Number == number);
            }
            else
            {
                if (className != null && className != "")
                    list = list.Where(x => x.RoomClass.Name == className);

                if (peopleCount != 0)
                    list = list.Where(x => x.PeopleCount == peopleCount);

                if (inAccessible)
                    list = list.Where(x => !x.IsAccessible);
            }

            var result = new List<RoomDTO>();
            foreach(var room in list)
            {
                var roomDTO = BLLService.BLLMapper.RoomMap(room);
                if (!room.IsAccessible)
                    roomDTO.AppartmentStatus = RoomStatus.INACCESSIBLE;
                result.Add(roomDTO);
            }

            return result;
        }

        public RoomDTO GetCertainRoom(int id)
        {
            var room = db.Rooms.Get(id);
            if (room is null)
                return null;

            var roomDto = BLLService.BLLMapper.RoomMap(room);
            if(!room.IsAccessible)
            {
                roomDto.AppartmentStatus = RoomStatus.INACCESSIBLE;
            }

            return roomDto;
        }

        public OperationMessage ChangeRoom(RoomDTO roomDTO)
        {
            if (roomDTO is null)
                return new OperationMessage()
                { IsPositive = false, Text = "Parameter \"room\" was null" };

            var room = db.Rooms.Get(roomDTO.Id);
            if (room.Number != roomDTO.Number && IsRoomNumber(room.Number))
                    return new OperationMessage()
                    { IsPositive = false, Text = $"Room which has number {room.Number} exists" };
            room.Number = roomDTO.Number;

            if(room.RoomClass.Name != roomDTO.RoomClassName)
            {
                var roomClass = db.RoomClasses.GetAll().Where(x => x.Name == roomDTO.RoomClassName).Single();
                room.RoomClass = roomClass ?? room.RoomClass;
            }

            if( roomDTO.PeopleCount > 0 && roomDTO.PeopleCount < 11)
            {
                room.PeopleCount = roomDTO.PeopleCount;
            }

            room.IsAccessible = roomDTO.AppartmentStatus == RoomStatus.AVAILABLE;

            db.Rooms.Update(room);
            db.Commit();
            return new OperationMessage()
            { IsPositive = true, Text = $"Room which has number {room.Number} has been changed!" };
        }

    }
}
