using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Bookings = new List<Booking>(); 
            UnluckyRequests = new List<UnluckyRequest>();
        }
        public virtual ICollection<Booking> Bookings { get; set; }// add one-to many connections
        public virtual ICollection<UnluckyRequest> UnluckyRequests { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
}