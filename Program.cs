using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace TestTaskClient
{
    class Program
    {
        private const string GETPEOPLE = "https://localhost:5001/home/getpeople";
        private const string SAVEPERSON = "https://localhost:5001/home/saveperson";
        private const string GETSPLITSTRING = "https://localhost:5001/home/GetSplitString?stringtosplit=";
        static void Main(string[] args)
        {
            ServicePointManager
                .ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

            var peopleToSave = Feed();

            foreach (var person in peopleToSave)
            {
                Console.WriteLine(PostPerson(person));
            }

            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcdsvcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd s dcdsvcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcdsvcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdc dsvcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcdsvcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcd svcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcds vcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdcds vcd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, "s dcsd sdc dsv cd dfv "));
            Console.WriteLine(GetRequest(GETSPLITSTRING, ""));
            Console.WriteLine(GetRequest(GETPEOPLE));

        }

        private static List<Person> Feed()
        {
            return new List<Person>{
                    new Person{BirthDate= DateTime.Now,FamilyName = "Stirling", Name="Lindsey",PatreonicName="Chack", IIN="08a51db7-7866-4ceb-8c43-56211547e619"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Marshall", Name="Matters",PatreonicName="Steve", IIN="6dd3d03d-0319-41f7-aa08-5761102ca5d1"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Marlin", Name="Mandson", IIN="08c3b0a9-4b7d-49fc-8b54-86c4ac96d3b3"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Dana", Name="White", IIN="4f86a576-7ddf-4e9f-9867-57de8cf39938"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Stirling", Name="Lindsey", IIN="08a51db7-7866-4ceb-8c43-56211547e619"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Sabakuno", Name="Gaara", IIN="1e1da78c-579d-4650-9311-ba24c800315f"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Triple", Name="G", IIN="dc475010-7641-46ba-92ba-82468f3557b8"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Kanagawa", Name="Prefecture", IIN="12345565"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Watanabe", Name="Kira", IIN="1e1da78c-579d-4650-9311-ba24c800315f"},
                    new Person{BirthDate= DateTime.Now,FamilyName = "Nursultan", Name="Nazarbayev", IIN="ed2e277e-38ce-4fe8-ba3a-2684549ee6f1"},
                };
        }

        public static string PostPerson(Person person)
        {
            try
            {
                using var client = new WebClient();
                var content = new NameValueCollection();
                content["IIN"] = person.IIN;
                content["Name"] = person.Name;
                content["FamilyName"] = person.FamilyName;
                content["PatreonicName"] = person.PatreonicName;
                content["BirthDate"] = person.BirthDate.ToString();
                var response = client.UploadValues(SAVEPERSON, "POST", content);
                string resp = Encoding.UTF8.GetString(response);
                return resp;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string GetRequest(string getRequestUri, string query = "")
        {
            using var client = new WebClient();
            return client.DownloadString(getRequestUri + query);
        }
    }

    public class Person
    {
        public string IIN { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string PatreonicName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
