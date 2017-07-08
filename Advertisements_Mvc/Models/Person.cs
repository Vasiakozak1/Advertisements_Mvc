using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advertisements_Mvc.Scripts;
namespace Advertisements_Mvc.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }

        public Person(int ID, string Name, string PhoneNumber, string Email)
        {
            this.ID = ID;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;

        }
        public Person() { }
        public bool IsValid(out string message)
        {
            PersonValidator validator = new PersonValidator(this);
            bool flag = validator.IsValid();
            message = validator.Message;
            return flag;
        }
        public override bool Equals(object obj)
        {
            Person per2 = obj as Person;
            if (this.Email == per2.Email && this.Name == per2.Name && this.PhoneNumber == per2.PhoneNumber)
                return true;
            return false;
        }
    }
}