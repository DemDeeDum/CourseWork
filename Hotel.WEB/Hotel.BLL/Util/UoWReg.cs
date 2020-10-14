using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;
using Hotel.DAL.Context;
using Ninject.Modules;

namespace Hotel.BLL.Util
{
    public class UoWReg : NinjectModule
    {
        public override void Load()
        {
            Bind<IUoW<ApplicationDbContext>>().To<UoW>();
        }
    }
}