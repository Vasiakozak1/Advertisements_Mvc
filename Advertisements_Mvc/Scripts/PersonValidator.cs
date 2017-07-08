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

        private static string[] VALIDATOR_MESSAGE = LanguageResource.GetPersonValidatorStrings(MvcApplication.AppLanguage);
        private const int WRONG_PHONE_NUMBER = 0;
        private const int WRONG_NAME = 1;
        private const int WRONG_EMAIL = 2;
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
            Regex phoneRegExpr = new Regex(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");
            Regex eMailRegExpr = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            StringBuilder buildResult = new StringBuilder();
            bool isRight = true;
            if (person.Name.Length > 20 || person.Name.Any(ch => char.IsNumber(ch))) 
            {
                isRight = false;
                buildResult.AppendLine(VALIDATOR_MESSAGE[WRONG_NAME]);
            }
            else
                buildResult.AppendLine();
            if (!eMailRegExpr.IsMatch(person.Email)) 
            {
                isRight = false;
                buildResult.AppendLine(VALIDATOR_MESSAGE[WRONG_EMAIL]);
            }
            else
                buildResult.AppendLine();
            if (!phoneRegExpr.IsMatch(person.PhoneNumber)) 
            {
                buildResult.AppendLine(VALIDATOR_MESSAGE[WRONG_PHONE_NUMBER]);
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