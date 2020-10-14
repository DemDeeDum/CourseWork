

namespace Hotel.BLL.Services
{
    using System;
    using Hotel.BLL.Interfaces;
    using Hotel.DAL.Context;
    using Hotel.DAL.Interfaces;
    using Hotel.DAL.UoW;

    public class LogService : ILogService
    {
        private IUoW<ApplicationDbContext> db { get; }
        public LogService(IUoW<ApplicationDbContext> db)
        {
            this.db = db ?? new UoW();
        }

        public LogService()
        {
            db = new UoW();
        }

        void ILogService.AddLog(DTOs.LogDTO obj)
        {
            var log = BLLService.BLLMapper.LogMap(obj);      
            try
            {
                db.Logs.Create(log);
                db.Commit();
            }
            catch
            {

            }
        }

        void IDisposable.Dispose()
        {
            db.Dispose();
        }
    }
}
