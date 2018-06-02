using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Parking.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<AutoParkingDto> Autoparking { get; set; }
    }

    public class AutoParkingDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        ///<summary>
        ///Дата заполнения
        ///</summary>
        public DateTime Filled { set; get; }
        ///<summary>
        ///Марка автомобиля
        ///</summary>
        public string AutoName { set; get; }
        ///<summary>
        ///Номер автомобиля
        ///</summary>
        public string AutoNumber { set; get; }
        ///<summary>
        ///Тип Кузова
        ///</summary>
        public Body Type { set; get; }
        ///<summary>
        ///Место на парковке
        ///</summary>
        public int ParkingNumber { set; get; }
        ///<summary>
        ///стоймость парковки
        ///</summary>
        public decimal Price { set; get; }
        ///<summary>
        ///окончание аренды места
        ///</summary>
        public DateTime TimeOut { set; get; }
        ///<summary>
        ///серия паспорта
        ///</summary>
        public string Series { set; get; }
        ///<summary>
        ///номер паспорта
        ///</summary>
        public string Number { set; get; }
        ///<summary>
        ///ФИО владельца автомобиля
        ///</summary>
        public string FullName { set; get; }
        ///<summary>
        ///
        ///</summary>
        //////<summary>
        ///Контактный номер
        ///</summary>
        public string PhoneNumber { set; get; }
    }
    /// <summary>
    /// тип кузова
    /// </summary>
    public enum Body
    {
        Седан,
        Универсал,
        Фургон,
        Хэтчбек,
        Купе,
        Пикап,
        Минивэн,
        Кроссовер
    }
}