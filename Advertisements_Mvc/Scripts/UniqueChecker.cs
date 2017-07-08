using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advertisements_Mvc.Models;
using Advertisements_Mvc.Controllers;
using System.Text;
namespace Advertisements_Mvc.Scripts
{
    public static class UniqueChecker
    {
        /// <summary>
        /// Перевіряється унікальність по номеру та емейлу
        /// </summary>
        /// <param name="person"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsUnique(Person person,out string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            string withoutSpcNumber = person.PhoneNumber.RemaoveSpaces();
            IEnumerable<Person> numberCoincidences = HomeController.currentDB.GetPersonsBy(person.PhoneNumber);
            IEnumerable<Person> eMailCoincidences = HomeController.currentDB.GetPersonsBy(person.Email);
            bool flag = true;

            foreach (Person current in eMailCoincidences)
            {
                if (current.ID != person.ID)
                {
                    flag = false;
                    builder.AppendLine("Такий емейл вже існує");
                    break;
                }
            }
            if (flag)
                builder.AppendLine();

            foreach (Person current in numberCoincidences)
            {
                if (current.ID != person.ID)
                {
                    flag = false;
                    builder.AppendLine("Такий номер телефону вже існує"); 
                    break;
                }
            }
            if (flag)
                builder.AppendLine();
            
            message = builder.ToString();
            return flag;
        }
    }
    public static class Extension
    {
        public static string RemaoveSpaces(this string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (!(str[i] == ' ')) 
                    result += str[i];
            }
            return result;
        }
    }

}