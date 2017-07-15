using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Advertisements_Mvc.Models;
using Advertisements_Mvc.Controllers;
namespace Advertisements_Mvc.Scripts
{
    public class Databasework
    {
        private const string ConnString = "persistsecurityinfo=True;server=localhost;user id=root;database=database_2;password=Wryw339v!";
        private MySqlConnection conn;
        public Databasework()
        {
            Connect();
        }

        private void Connect()
        {
            conn = new MySqlConnection(ConnString);
            conn.Open();
        }
        public Person GetPersonBy(string field)
        {
            Person personResult = null;
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM person";
            MySqlDataReader dataRead = command.ExecuteReader(CommandBehavior.Default);
            if (dataRead.HasRows)
            {
                while (dataRead.Read())
                {
                    int id = (int)dataRead.GetValue(0);
                    string name = dataRead.GetValue(1).ToString();
                    string phone_number = dataRead.GetValue(2).ToString();
                    string email = dataRead.GetValue(3).ToString();
                    if (id.ToString() == field || name == field || phone_number == field || email == field)
                    {
                        personResult = new Person(id, name, phone_number, email);
                        break;
                    }
                }
            }
            dataRead.Close();
            command.Dispose();
            return personResult;
        }
        public IEnumerable<Person> GetPersonsBy(string field)
        {
            List<Person> persons = new List<Person>();
            MySqlCommand command = HomeController.currentDB.conn.CreateCommand();
            command.CommandText = "SELECT * FROM person";
            MySqlDataReader dataRead = command.ExecuteReader(CommandBehavior.Default);
            if (dataRead.HasRows)
            {
                while (dataRead.Read())
                {
                    int id = (int)dataRead.GetValue(0);
                    string name = dataRead.GetValue(1).ToString();
                    string phone_number = dataRead.GetValue(2).ToString();
                    string email = dataRead.GetValue(3).ToString();
                    if (id.ToString() == field || name == field || phone_number == field || email == field)
                    {
                        persons.Add(new Person(id, name, phone_number, email));
                    }
                }
            }
            dataRead.Close();
            command.Dispose();
            return persons;
        }
        public Advertisment GetAdvertisementBy(string field)
        {
            Advertisment advertisementResult = null;
            Person personResult = null;
            MySqlCommand command = conn.CreateCommand();
            string queryText = string.Format("SELECT advertisement.adname, advertisement.price, advertisement.currency, advertisement.servicetype, advertisement.id"
                + "person.id, person.name, person.phone_number, person.email FROM advertisement INNER JOIN person ON advertisement.person_id = person.id");
            command.CommandText = queryText;
            MySqlDataReader dataRead = command.ExecuteReader(CommandBehavior.Default);
            while (dataRead.Read())
            {
                int price = 0;
                char currency;
                string adname = dataRead.GetValue(0).ToString();
                int.TryParse(dataRead.GetValue(1).ToString(), out price);
                char.TryParse(dataRead.GetValue(2).ToString(), out currency);
                string servType = dataRead.GetValue(3).ToString();
                int adId = (int)dataRead.GetValue(4);
                int personID = (int)dataRead.GetValue(5);
                if (adname == field || price.ToString() == field || currency.ToString() == field || servType == field || personID.ToString() == field)
                {
                    string personName = dataRead.GetValue(6).ToString();
                    string phoneNumber = dataRead.GetValue(7).ToString();
                    string eMail = dataRead.GetValue(8).ToString();
                    personResult = new Person(personID, personName, phoneNumber, eMail);
                    advertisementResult = new Advertisment(adId, adname, Advertisment.GetServiceFromStr(servType), personResult, price == 0 ? "" : price.ToString() + currency);
                }
            }
            dataRead.Close();

            return advertisementResult;
        }
        public IEnumerable<Advertisment> GetAdvertisementsBy(string field)
        {
            List<Advertisment> resultAds = new List<Advertisment>();
            MySqlCommand command = conn.CreateCommand();
            string queryText = string.Format("SELECT advertisement.adname, advertisement.price, advertisement.currency, advertisement.servicetype, advertisement.id,"
               + " person.id, person.name, person.phone_number, person.email FROM advertisement INNER JOIN person ON advertisement.person_id = person.id");
            command.CommandText = queryText;

            MySqlDataReader dataRead = command.ExecuteReader(CommandBehavior.Default);
            while (dataRead.Read())
            {
                int price = 0;
                char currency;
                string adname = dataRead.GetValue(0).ToString();
                int.TryParse(dataRead.GetValue(1).ToString(), out price);
                char.TryParse(dataRead.GetValue(2).ToString(), out currency);
                string servType = dataRead.GetValue(3).ToString();
                int adId = (int)dataRead.GetValue(4);
                int personID = (int)dataRead.GetValue(5);
                if (adname == field || price.ToString() == field || currency.ToString() == field || servType == field || personID.ToString() == field)
                {
                    string personName = dataRead.GetValue(6).ToString();
                    string phoneNumber = dataRead.GetValue(7).ToString();
                    string eMail = dataRead.GetValue(8).ToString();
                    Person tempPerson = new Person(personID, personName, phoneNumber, eMail);
                    Advertisment tempAd = new Advertisment(adId, adname, Advertisment.GetServiceFromStr(servType), tempPerson, price == 0 ? "" : price.ToString() + currency);
                    resultAds.Add(tempAd);
                }
            }
            dataRead.Close();
            command.Dispose();
            return resultAds;
        }

        public void InsertInto(Person person)
        {
            if (person == null)
                throw new Exception("Спроба записати null значення");
            int lastID = GetLastPersonID();

            MySqlCommand command = this.conn.CreateCommand();
            string queryText = string.Format("INSERT INTO person(id,name,phone_number,email) VALUES({0},\"{1}\",\"{2}\",\"{3}\")", lastID + 1, person.Name, person.PhoneNumber, person.Email);
            command.CommandText = queryText;
            int result = command.ExecuteNonQuery();
            if (result == 1)
                Console.WriteLine("Успiшно добавлено в базу даних");
            else
                throw new Exception("Помилка при вставцi");
            command.Dispose();
        }
        public void InsertInto(Advertisment advertisment)
        {
            if (advertisment == null || advertisment.Who == null)
                throw new Exception("Спроба записати null значення");
            InsertInto(advertisment.Who);
            int lastID = GetLastPersonID();
            MySqlCommand command = this.conn.CreateCommand();

            string queryText = string.Format
                ("INSERT INTO advertisement(adname,price,currency,servicetype,person_id) VALUES(\"{0}\",{1},\"{2}\",\"{3}\",{4})"
                , advertisment.NameOfAd, advertisment.Price.Substring(0, advertisment.Price.Length - 1)
                , advertisment.Price[advertisment.Price.Length - 1], advertisment.ServiceType.ToString(), lastID);
            command.CommandText = queryText;
            int result = command.ExecuteNonQuery();
            if (result == 1)
                Console.WriteLine("Успiшно добавлено в базу даних");
            else
                throw new Exception("Помилка при вставцi");
            command.Dispose();
        }

        public void UpdatePerson(Person newPerson)
        {
            MySqlCommand command = this.conn.CreateCommand();
            string queryText = string.Format("UPDATE person SET name = \"{0}\", phone_number = \"{1}\", email = \"{2}\" WHERE id = {3}"
                , newPerson.Name, newPerson.PhoneNumber, newPerson.Email, newPerson.ID);
            command.CommandText = queryText;
            int result = command.ExecuteNonQuery();
            if (result == 1)
                Console.WriteLine("Змiнено {0} рядок", result);
            else
                throw new Exception("Помилка при з'єднаннi");

        }

        public void UpdateAdvertisement(Advertisment newAdvertisement)
        {
            MySqlCommand command = conn.CreateCommand();
            string price = newAdvertisement.Price.Substring(0, newAdvertisement.Price.Length - 1);
            string currency = newAdvertisement.Price.Substring(newAdvertisement.Price.Length - 1, 1);
            string queryText = string.Format("UPDATE advertisement SET adname = \"{0}\", price = {1}, currency = \"{2}\"" +
                ", servicetype = \"{3}\" WHERE person_id = {4}", newAdvertisement.NameOfAd, price, currency
                , newAdvertisement.ServiceType.ToString(), newAdvertisement.Who.ID);
            command.CommandText = queryText;
            command.ExecuteNonQuery();
            command = conn.CreateCommand();
            queryText = string.Format("UPDATE person SET name = \"{0}\", phone_number = \"{1}\", email = \"{2}\" WHERE id = {3}",
                newAdvertisement.Who.Name, newAdvertisement.Who.PhoneNumber, newAdvertisement.Who.Email, newAdvertisement.Who.ID);
            command.CommandText = queryText;
            command.ExecuteNonQuery();
        }

        public void Delete(Person person)
        {
            MySqlCommand command = conn.CreateCommand();
            string queryText = string.Format("DELETE FROM person WHERE id = {0} AND name = \"{1}\" AND phone_number = \"{2}\" AND email = \"{3}\"",
                person.ID, person.Name, person.PhoneNumber, person.Email);
            command.CommandText = queryText;
            try
            {
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 1451)
                {
                    
                }
            }

        }
        public void Delete(Advertisment advertisement)
        {
            MySqlCommand command = conn.CreateCommand();
            string price = advertisement.Price.Substring(0, advertisement.Price.Length - 1);
            string currency = advertisement.Price.Substring(advertisement.Price.Length - 1, 1);
            string queryText = string.Format("DELETE FROM advertisement WHERE id = {0}", advertisement.ID);
            command.CommandText = queryText;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void DeleteAdsBy(Person person)
        {
            MySqlCommand command = conn.CreateCommand();
            string queryText = string.Format("DELETE FROM advertisement WHERE person_id = {0}", person.ID);
            command.CommandText = queryText;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public IEnumerable<Person> GetAllPersons()
        {
            List<Person> prsns = new List<Person>();
            MySqlCommand command = conn.CreateCommand();
            string queryText = "SELECT * FROM person";
            command.CommandText = queryText;
            MySqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                Person tempPerson = null;
                int id = (int)reader.GetValue(0);
                string name = reader.GetValue(1).ToString();
                string phoneNumber = reader.GetValue(2).ToString();
                string eMail = reader.GetValue(3).ToString();
                tempPerson = new Person(id, name, phoneNumber, eMail);
                prsns.Add(tempPerson);
            }
            reader.Close();
            command.Dispose();
            return prsns;
        }
        public IEnumerable<Advertisment> GetAllAdvertisements()
        {
            List<Advertisment> advs = new List<Advertisment>();
            MySqlCommand command = conn.CreateCommand();
            string queryText = "SELECT * FROM advertisement LEFT JOIN person on(person.id = advertisement.person_id)";
            command.CommandText = queryText;
            MySqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                string adname = reader.GetValue(0).ToString();
                int price = reader.GetValue(1) != null ? (int)reader.GetValue(1) : 0;
                char currency = reader.GetValue(2) != null ? (char)reader.GetValue(2) : char.MinValue;
                ServiceType servType = Advertisment.GetServiceFromStr(reader.GetValue(3).ToString());
                int adId = (int)reader.GetValue(4);
                int id = (int)reader.GetValue(5);
                string name = reader.GetValue(6).ToString();
                string phoneNumber = reader.GetValue(7).ToString();
                string eMail = reader.GetValue(8).ToString();
                Person p = new Person(id, name, phoneNumber, eMail);
                advs.Add(new Advertisment(adId, adname, servType, p, price != 0 ? price.ToString() : ""));
            }
            reader.Close();
            command.Dispose();
            return advs;
        }


        private int GetLastPersonID()
        {
            MySqlCommand command = this.conn.CreateCommand();
            command.CommandText = "SELECT * FROM person";
            int lastID = 1;
            MySqlDataReader dataRead = command.ExecuteReader(CommandBehavior.Default);
            while (dataRead.Read())
            {
                int tempID = (int)dataRead.GetValue(0);
                if (lastID < tempID)
                    lastID = tempID;
            }
            dataRead.Close();
            return lastID;

        }
    }
}