using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Advertisements_Mvc.Models;
using System.Text;
using Advertisements_Mvc.Controllers;
using System.Configuration;
namespace Advertisements_Mvc.Scripts
{
    public class PersonValidator : Validator
    {

        private string resultMessage;
        private Person person;
        public string Message
        {
            get
            {
                if (resultMessage == string.Empty)
                    return "";
                else
                    return this.resultMessage;
            }

        }

        public PersonValidator(Person person)
        {
            this.person = person;
            this.resultMessage = string.Empty;
        }

        public bool IsValid()
        {
            Regex eMailRegExpr = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex phoneRegExpr = new Regex(@"^\+[0-9]{3}\s+[0-9]{2}\s+[0-9]{7}$");
            StringBuilder buildResult = new StringBuilder();
            bool isRight = true;
            if (person.Name.Length > 20 || person.Name.Any(ch => char.IsNumber(ch))) 
            {
                isRight = false;
                buildResult.AppendLine(App_LocalResources.PerVal.too_long_name);
            }
            else
                buildResult.AppendLine();
            if (!eMailRegExpr.IsMatch(person.Email)) 
            {
                isRight = false;
                buildResult.AppendLine(App_LocalResources.PerVal.wrong_email);
            }
            else
                buildResult.AppendLine();

            string[] numbersDigits = person.PhoneNumber.Split(new char[] { '(', ')', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder buildedNumber = new StringBuilder();
            for (int i = 0; i < numbersDigits.Length; i++)
            {
                if (i <= 1)
                    buildedNumber.Append(numbersDigits[i] + " ");
                else
                    buildedNumber.Append(numbersDigits[i]);
            }

            if (!(phoneRegExpr.IsMatch(buildedNumber.ToString())))  
            {
                buildResult.AppendLine(App_LocalResources.PerVal.invalid_phone);
                isRight = false;
            }
            else
                buildResult.AppendLine();
            if (isRight)
                return true;
            else
            {
                this.resultMessage = buildResult.ToString();
                return false;
            }
        }
    }
}