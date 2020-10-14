using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.BLL.Util
{
    public class ServiceReg : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookingPageService>().To<BookingService>();
            Bind<IProfileService>().To<ProfileService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IManageService>().To<ManageService>();
            Bind<IAccountService>().To<AccountService>();
            Bind<ILogService>().To<LogService>();
            Bind<IStaffService>().To<StaffService>();
        }
    }
}