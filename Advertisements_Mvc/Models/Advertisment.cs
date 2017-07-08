using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advertisements_Mvc.Models
{
    public class Advertisment
    {
        public string NameOfAd { get; set; }
        public string Price { get; set; }
        public ServiceType ServiceType { get; set; }
        public Person Who { get; set; }

        public Advertisment(string NameOfAd, ServiceType ServiceType,
            Person Who, string Price = "")
        {
            this.NameOfAd = NameOfAd;
            this.Price = Price;
            this.ServiceType = ServiceType;
            this.Who = Who;
        }
        public Advertisment() { }

        public override string ToString()
        {
            switch (Price)
            {
                case "":
                    return string.Format("\t{0}\nТип оголошення:{1}\nЗвертатися до:{2}\nНомер телефону:{3}", NameOfAd, ServiceType, Who.Name, Who.PhoneNumber);
                default:
                    return string.Format("\t{0}\nТип оголошення:{1}\nЦiна послуги:{2}\nЗвертатися до:{3}\nНомер телефону:{4}", NameOfAd, ServiceType, Price, Who.Name, Who.PhoneNumber);
            }

        }
        public override bool Equals(object obj)
        {
            Advertisment ad2 = obj as Advertisment;
            return (this.NameOfAd == ad2.NameOfAd && this.Price == ad2.Price && this.ServiceType == ad2.ServiceType
                && this.Who.Equals(ad2.Who));

        }
        public static ServiceType GetServiceFromStr(string str)
        {
            switch (str)
            {
                case "Медицина":
                    return ServiceType.Медицина;
                case "Навчання":
                    return ServiceType.Навчання;
                case "Комерція":
                    return ServiceType.Комерція;
                case "Комерцiя":
                    return ServiceType.Комерція;
                case "Знайомства":
                    return ServiceType.Знайомства;
            }
            throw new Exception("Не вдалося перетворити string в enum");
        }
    }

    public enum ServiceType
    {
        Медицина,
        Навчання,
        Комерція,
        Знайомства
    }
}