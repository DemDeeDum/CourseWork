using System.Web.Mvc;
using Hotel.BLL.DTOs;
using Hotel.BLL.Models;
using Hotel.BLL.Enums;
using AutoMapper;
using Hotel.WEB.Models.Admin;
using Hotel.WEB.Models.Common;
using Hotel.WEB.Models.Profile;
using Hotel.WEB.Models.Booking;
using System;

namespace Hotel.WEB.Infrastructure
{
    public class HotelMapper : Profile
    {
        public HotelMapper()
        {

            Mapper.CreateMap<string, SelectListItem>()
                .ForMember(dest => dest.Text
                , opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Text
                , opt => opt.MapFrom(src => src));

            Mapper.CreateMap<RoomDTO, RoomViewModel>()
                .ForMember(dest => dest.AppartmentStatus
                , opt => opt.MapFrom(src => src.AppartmentStatus.ToString()))
                .ForMember(dest => dest.Price
                , opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.PeopleCount
                , opt => opt.MapFrom(src => src.PeopleCount))
                .ForMember(dest => dest.Number
                , opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.RoomClassName
                , opt => opt.MapFrom(src => src.RoomClassName))
                .ForMember(dest => dest.ClassDisplayColor
                , opt => opt.MapFrom(src => src.ClassDisplayColor))
                .ForMember(dest => dest.Id
                , opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<RoomViewModel, RoomDTO>()
                .ForMember(dest => dest.AppartmentStatus
                , opt => opt.MapFrom(src => Enum.Parse(typeof(RoomStatus), src.AppartmentStatus)))
                .ForMember(dest => dest.Price
                , opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.PeopleCount
                , opt => opt.MapFrom(src => src.PeopleCount))
                .ForMember(dest => dest.Number
                , opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.RoomClassName
                , opt => opt.MapFrom(src => src.RoomClassName))
                .ForMember(dest => dest.ClassDisplayColor
                , opt => opt.MapFrom(src => src.ClassDisplayColor))
                .ForMember(dest => dest.Id
                , opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<RoomClassDTO, RoomClassViewModel>()
                 .ForMember(dest => dest.Name
                , opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Price
                , opt => opt.MapFrom(src => src.Price))
                  .ForMember(dest => dest.DisplayColor
                , opt => opt.MapFrom(src => src.DisplayColor))
                   .ForMember(dest => dest.Info
                , opt => opt.MapFrom(src => src.Info));

            Mapper.CreateMap<SearchSettings, OrderCreator>()
                .ForMember(dest => dest.PeopleFilter
                , opt => opt.MapFrom(src => src.PeopleFilter))
                .ForMember(dest => dest.PriceFilter
                , opt => opt.MapFrom(src => src.PriceFilter))
                .ForMember(dest => dest.StatusFilter
                , opt => opt.MapFrom(src => src.StatusFilter))
                .ForMember(dest => dest.RoomClassFilter
                , opt => opt.MapFrom(src => src.RoomClassFilter))
                .ForMember(dest => dest.BeginningTime
                , opt => opt.MapFrom(src => src.BeginningTime))
                .ForMember(dest => dest.EndingTime
                , opt => opt.MapFrom(src => src.EndingTime))
                .ForMember(dest => dest.Descending
                , opt => opt.MapFrom(src => src.Descending));

            Mapper.CreateMap<RoomWholeInfo, RoomWholeInformationViewModel>()
            .ForMember(dest => dest.Price
            , opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.RoomClassInfo
            , opt => opt.MapFrom(src => src.RoomClassInfo))
            .ForMember(dest => dest.RoomClassName
            , opt => opt.MapFrom(src => src.RoomClassName))
            .ForMember(dest => dest.Id
            , opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number
            , opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.PeopleCount
            , opt => opt.MapFrom(src => src.PeopleCount))
            .ForMember(dest => dest.RoomClassColorToDisplay
            , opt => opt.MapFrom(src => src.RoomClassColorToDisplay))
            .ForMember(dest => dest.RoomClassImageAddress
            , opt => opt.MapFrom(src => src.RoomClassImageAddress));

            Mapper.CreateMap<BookingDTO, BookingViewModel>()
           .ForMember(dest => dest.BegginingTime
           , opt => opt.MapFrom(src => src.BegginingTime))
           .ForMember(dest => dest.DeadLine
           , opt => opt.MapFrom(src => src.DeadLine))
           .ForMember(dest => dest.EndingTime
           , opt => opt.MapFrom(src => src.EndingTime))
           .ForMember(dest => dest.IsConfirmed
           , opt => opt.MapFrom(src => src.IsConfirmed))
           .ForMember(dest => dest.RoomClassName
           , opt => opt.MapFrom(src => src.RoomClassName))
           .ForMember(dest => dest.RoomColorToDisplay
           , opt => opt.MapFrom(src => src.RoomColorToDisplay))
           .ForMember(dest => dest.RoomNumber
           , opt => opt.MapFrom(src => src.RoomNumber))
           .ForMember(dest => dest.PeopleCount
           , opt => opt.MapFrom(src => src.PeopleCount))
           .ForMember(dest => dest.Id
           , opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<UnluckyRequestDTO, UnluckyRequestViewModel>()
            .ForMember(dest => dest.UserName
            , opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.UserEmail
            , opt => opt.MapFrom(src => src.UserEmail))
            .ForMember(dest => dest.StartDate
            , opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.RoomClassName
            , opt => opt.MapFrom(src => src.RoomClassName))
            .ForMember(dest => dest.PeopleCount
            , opt => opt.MapFrom(src => src.PeopleCount))
            .ForMember(dest => dest.FinishDate
            , opt => opt.MapFrom(src => src.FinishDate))
            .ForMember(dest => dest.UnluckyRequestId
            , opt => opt.MapFrom(src => src.UnluckyRequestId));


            Mapper.CreateMap<ApplicationUserDTO, UserViewModel>()
            .ForMember(dest => dest.UserName
            , opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email
            , opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role
            , opt => opt.MapFrom(src => src.Role));

            Mapper.CreateMap<OperationMessage, LogicMessage>()
            .ForMember(dest => dest.IsPositive
             , opt => opt.MapFrom(src => src.IsPositive))
             .ForMember(dest => dest.Text
             , opt => opt.MapFrom(src => src.Text));


            Mapper.CreateMap<LogViewModel, LogDTO>()
            .ForMember(dest => dest.BrowserName
            , opt => opt.MapFrom(src => src.BrowserName))
            .ForMember(dest => dest.BrowserVersion
            , opt => opt.MapFrom(src => src.BrowserVersion))
            .ForMember(dest => dest.JavasriptVersion
            , opt => opt.MapFrom(src => src.JavasriptVersion))
            .ForMember(dest => dest.IsMobile
            , opt => opt.MapFrom(src => src.IsMobile))
            .ForMember(dest => dest.Platform
            , opt => opt.MapFrom(src => src.Platform))
            .ForMember(dest => dest.UserName
            , opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.URL
            , opt => opt.MapFrom(src => src.URL))
            .ForMember(dest => dest.Exeption
            , opt => opt.MapFrom(src => src.Exeption))
            .ForMember(dest => dest.Date
            , opt => opt.MapFrom(src => src.Date));


            Mapper.CreateMap<LogDTO, LogViewModel>()
            .ForMember(dest => dest.BrowserName
            , opt => opt.MapFrom(src => src.BrowserName))
            .ForMember(dest => dest.BrowserVersion
            , opt => opt.MapFrom(src => src.BrowserVersion))
            .ForMember(dest => dest.JavasriptVersion
            , opt => opt.MapFrom(src => src.JavasriptVersion))
            .ForMember(dest => dest.IsMobile
            , opt => opt.MapFrom(src => src.IsMobile))
            .ForMember(dest => dest.Platform
            , opt => opt.MapFrom(src => src.Platform))
            .ForMember(dest => dest.UserName
            , opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.URL
            , opt => opt.MapFrom(src => src.URL))
            .ForMember(dest => dest.Exeption
            , opt => opt.MapFrom(src => src.Exeption))
            .ForMember(dest => dest.Date
            , opt => opt.MapFrom(src => src.Date));

        }
    }
}