using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parking
{
    /// <summary>
    /// информация об автомобиле 
    /// </summary>
    public class AutoParkingDto
    {
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
        public Body BodyType { set; get; }
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
        ///Владелец
        ///</summary>
        public OwnerDto Owner { set; get; }
        ///<summary>
        ///паспортные данные
        ///</summary>
        public PassportDto Passport { set; get; }
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
    /// <summary>
    /// информация о владельце
    /// </summary>
    public class OwnerDto
    {
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
    /// информация о паспорте владельца
    /// </summary>
    public class PassportDto
    {
        ///<summary>
        ///серия паспорта
        ///</summary>
        public string Series { set; get; }
        ///<summary>
        ///номер паспорта
        ///</summary>
        public string Number { set; get; }
    }
}
