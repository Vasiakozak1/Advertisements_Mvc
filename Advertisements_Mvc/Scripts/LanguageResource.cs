using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Advertisements_Mvc.Models;
using System.Diagnostics;
namespace Advertisements_Mvc.Scripts
{
    public static class LanguageResource
    {
        private static string AD_VALIDATOR_SOURCE = HttpContext.Current.Server.MapPath("App_Data/Validator_strings.xml");
        private static string P_VALIDATOR_SOURCE = HttpContext.Current.Server.MapPath("~/App_Data/PersonValidator_strings.xml");
        
        public static string[] GetPersonValidatorStrings(Languages lang)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            List<string> resultStrings = new List<string>();
            XmlReader read = XmlReader.Create(P_VALIDATOR_SOURCE, settings);

            while (read.Name != lang.ToString())
                read.Read();
            read.Read();
            while (read.Name != lang.ToString())
            {
                switch (read.Value)
                {
                    case "invalid_phone":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "too_long_name":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "wrong_email":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;                    
                    default:
                        read.Read();
                        break;
                }
            }
            return resultStrings.ToArray();
        }

        public static string[] GetValidatorStrings(Languages lang)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            List<string> resultStrings = new List<string>();
            XmlReader read = XmlReader.Create(AD_VALIDATOR_SOURCE, settings);
            while (read.Name != lang.ToString())
                read.Read();
            read.Read();
            while (read.Name != lang.ToString())
            {
                switch (read.Value)
                {
                    case "invalid_phone":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "too_long_name":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "wrong_email":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "ok":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    default:
                        read.Read();
                        break;
                }
            }
            return resultStrings.ToArray();
        }
        public static Languages ParseLanguage(string lang)
        {
            switch (lang)
            {
                case "English":
                    return Languages.English;
                default:
                    return Languages.Ukrainian;
            }

        }
    }
}