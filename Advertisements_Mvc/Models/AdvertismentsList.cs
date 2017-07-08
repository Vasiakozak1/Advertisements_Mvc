using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Advertisements_Mvc.Scripts;
namespace Advertisements_Mvc.Models
{
    public delegate void WriteFileMethod(AdvertismentsList AdsList, string FileName);

    public class AdvertismentsList : IEnumerable<Advertisment>
    {
        private List<Advertisment> Advertisments;
        private WriteFileMethod MethodForWriting;
        public AdvertismentsList(WriteFileMethod MethodForWriting, params Advertisment[] MyAdvertisments)
        {
            this.MethodForWriting = MethodForWriting;
            Advertisments = new List<Advertisment>();
            ValidateAndAdd(MyAdvertisments);
        }

        private void ValidateAndAdd(params Advertisment[] inputAds)
        {
            foreach (Advertisment ad in inputAds)
            {
                AdvertismentValidator validator = new AdvertismentValidator(ad);
                if (validator.IsValid())
                    Advertisments.Add(ad);
                else
                    Console.WriteLine(validator.Message);
            }
        }

        public void AddAdveertisment(Advertisment ad)
        {
            ValidateAndAdd(ad);
        }

        public void WriteToFile(string FIleName)
        {
            MethodForWriting(this, FIleName);
        }
        public void SetWriteFileMethod(WriteFileMethod MethodForWriting)
        {
            this.MethodForWriting = MethodForWriting;
        }

        public void PrintInConsole()
        {
            foreach (Advertisment ad in this.Advertisments)
            {
                Console.WriteLine(ad.ToString());
            }
        }

        public IEnumerator<Advertisment> GetEnumerator()
        {
            return this.Advertisments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Advertisments.GetEnumerator();
        }

        public void OrderListBy(string field)
        {
            IOrderedEnumerable<Advertisment> orderedList = null;
            switch (field)
            {
                case "NameOfAd":
                    orderedList = from tempAd in this.Advertisments
                                  orderby tempAd.NameOfAd
                                  select tempAd;
                    break;
                case "Price":
                    orderedList = from tmp in Advertisments
                                  orderby int.Parse(tmp.Price.Substring(0, tmp.Price.Length - 1)) ascending
                                  select tmp;
                    break;
                case "ServiceType":
                    orderedList = from tmp in Advertisments
                                  orderby tmp.ServiceType.ToString()
                                  select tmp;
                    break;
                case "PersonName":
                    orderedList = from tmp in Advertisments
                                  orderby tmp.Who.Name ascending
                                  select tmp;
                    break;
            }
            Advertisments = orderedList.ToList() ?? Advertisments;
        }
        public static bool operator ==(AdvertismentsList lst1, AdvertismentsList lst2)
        {
            var lst2Enum = lst2.GetEnumerator();
            Advertisment ad2;
            foreach (Advertisment ad1 in lst1)
            {
                ad2 = lst2Enum.Current;
                if (ad1.NameOfAd != ad2.NameOfAd || ad1.Price != ad2.Price || ad1.Who.Email != ad2.Who.Email
                    || ad1.Who.Name != ad2.Who.Name || ad1.Who.PhoneNumber != ad2.Who.PhoneNumber)
                    return false;
                lst2Enum.MoveNext();
            }
            return true;
        }
        public static bool operator !=(AdvertismentsList lst1, AdvertismentsList lst2)
        {
            var lst2Enum = lst2.GetEnumerator();
            Advertisment ad2;
            foreach (Advertisment ad1 in lst1)
            {
                ad2 = lst2Enum.Current;
                if (ad1.NameOfAd == ad2.NameOfAd && ad1.Price == ad2.Price && ad1.ServiceType == ad2.ServiceType
                    && ad1.Who.Email == ad2.Who.Email && ad1.Who.Name == ad2.Who.Name && ad1.Who.PhoneNumber == ad2.Who.PhoneNumber)
                    return false;
            }
            return true;
        }
        //public override bool Equals(object obj)
        //{
        //    List<Advertisment> ads1 = new List<Advertisment>();
        //    List<Advertisment> ads2 = new List<Advertisment>();
        //    AdvertismentsList lst2 = obj as AdvertismentsList;
        //    foreach (Advertisment ad in this)
        //        ads1.Add(ad);
        //    foreach (Advertisment ad in lst2)
        //        ads2.Add(ad);
        //    for (int i = 0; i < ads1.Count; i++)
        //    {
        //        if (!ads1[i].Equals(ads2[i])) 
        //            return false;
        //    }

        //    return true;
        //}
    }
}