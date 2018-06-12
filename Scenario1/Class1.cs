using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using RestSharp;
using System;
using System.Net;

namespace Scénario1
{
    [TestFixture]
    public class Users
    {
        private string appUserId = "Hafsa-" + RandomUtil.GetRandomString();
        private string appUserId2 = "Hafsa-" + RandomUtil.GetRandomString();
        private string appUserId3 = "Hafsa-" + RandomUtil.GetRandomString();
        private string appUserIdP = "Hafsa-" + RandomUtil.GetRandomString();
        private string appAccountId = "ss- " + RandomUtil.GetRandomString();
        private string appAccountId2= "ss- " + RandomUtil.GetRandomString();
        private string appAccountIdP= "ss- " + RandomUtil.GetRandomString();
        private string tel = "6" + RandomUtil.RandomPhoneNumber();
        public string User1;
        private string firstname = RandomUtil.GetRandomString();
        private string name = RandomUtil.GetRandomString();
        private string email = RandomUtil.GetRandomString() + "@s-money.fr";

        [Test, Order(1)]
        public void CreateUser1()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appuserid"":""" + appUserId + @""",
        ""profile"":
         {
        ""civility"":""" + 0 + @""",
        ""firstname"":""" + firstname + @""",
        ""lastname"":""" + name + @""",
        ""birthdate"":""1975-01-17"",
        ""address"":
            {
            ""street"":""" + "Rue de " + appUserId + @""",
            ""zipcode"":""75014"",
            ""city"":""Paris"",
            ""country"":""FR""
            },  
        ""email"":""" + email + @""",
        ""phonenumber"":""" + "0" + tel + @"""
          }
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
            var jsonresp = JsonConvert.DeserializeObject<dynamic>(response.Content);
            User1 = jsonresp.AppUserId;

        }

        [Test, Order(2)]
        public void GetUser1()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users" + "/" + appUserId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "OK"), response.StatusCode);
            var jsonresp = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Assert.AreEqual(appUserId, (string)jsonresp.AppUserId);
            Assert.AreEqual("1", (string)jsonresp.Type);
            Assert.AreEqual("1", (string)jsonresp.Role);
            Assert.AreEqual("0", (string)jsonresp.Profile.Civility);
            Assert.AreEqual(firstname, (string)jsonresp.Profile.FirstName);
            Assert.AreEqual(name, (string)jsonresp.Profile.LastName);
            Assert.AreEqual("01/17/1975 00:00:00", (string)jsonresp.Profile.Birthdate);
            Assert.AreEqual("Rue de " + appUserId, (string)jsonresp.Profile.Address.Street);
            Assert.AreEqual("75014", (string)jsonresp.Profile.Address.ZipCode);
            Assert.AreEqual("Paris", (string)jsonresp.Profile.Address.City);
            Assert.AreEqual("FR", (string)jsonresp.Profile.Address.Country);
            Assert.AreEqual("33" + tel, (string)jsonresp.Profile.PhoneNumber);
            Assert.AreEqual(email, (string)jsonresp.Profile.Email);
        }

        [Test, Order(3)]
        public void CreateSubaccountUser1()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/" + appUserId + "/subaccounts");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appaccountid"":""" + appAccountId + @""",     
         ""displayname"":""" + "C'est le sous compte de "+ appUserId + @"""
       }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
        }

        [Test, Order(4)]
        public void CreateBankaccountUser1()
        {

            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/" + appUserId + "/bankaccounts/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""displayname"":""" + "C'est le compte bancaire de " + appUserId + @""",    
         ""bic"":""CMCIFR2A"", 
        ""iban"":""FR7610011000201234567890188"",
         ""ismine"":""true""
       }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
        }

        [Test, Order(5)]
        public void CreateUser2()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appuserid"":""" + appUserId2 + @""",
        ""profile"":
         {
        ""civility"":""" + 0 + @""",
        ""firstname"":""" + firstname + @""",
        ""lastname"":""" + name + @""",
        ""birthdate"":""1975-01-17"",
        ""address"":
            {
            ""street"":""" + "Rue de " + appUserId2 + @""",
            ""zipcode"":""75014"",
            ""city"":""Paris"",
            ""country"":""FR""
            }
          }
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);

        }

        [Test,Order(6)]
        public void EditUser2()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/"+appUserId2);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
      
        ""alias"":""" + "Alias de " + appUserId2 + @""",
        ""address"":
            {
            ""street"":""" + "Nouvelle Rue de " + appUserId2 + @""",
            ""zipcode"":""75015""
            }
          }
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "OK"), response.StatusCode);

        }

        [Test,Order(7)]
        public void CreateSubaccountUser2()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/" + appUserId2 + "/subaccounts");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appaccountid"":""" + appAccountId2 + @""",     
         ""displayname"":""" + "C'est le sous compte de " + appUserId2 + @"""
       }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
        }

        [Test, Order(8)]
        public void CreateUser3()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appuserid"":""" + appUserId3 + @""",
        ""profile"":
         {
        ""civility"":""" + 0 + @""",
        ""firstname"":""" + firstname + @""",
        ""lastname"":""" + name + @""",
        ""birthdate"":""1975-01-17"",
        ""address"":
            {
            ""street"":""" + "Rue de " + appUserId3 + @""",
            ""zipcode"":""75014"",
            ""city"":""Paris"",
            ""country"":""FR""
            }
          }
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);

        }

        [Test ,Order(9)]
        public void ChangeStatusUser3() {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/"+appUserId3);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""Type"":""2"",
        ""company"":
            {
        ""name"":""Intelcom Society"",
        ""SIRET"":""123456789""
            }     
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "OK"), response.StatusCode);
        }

        [Test, Order(10)]
        public void CloseAccountUser3()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/" + appUserId3);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
      
        ""status"":""5""
         }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "OK"), response.StatusCode);

        }

        [Test, Order(11)]
        public void CreateUserPro()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appuserid"":""" + appUserIdP + @""",
        ""profile"":
         {
        ""civility"":""" + 0 + @""",
        ""firstname"":""" + firstname + @""",
        ""lastname"":""" + name + @""",
        ""birthdate"":""1975-01-17"",
        ""address"":
            {
            ""street"":""" + "Rue de " + appUserIdP + @""",
            ""zipcode"":""75014"",
            ""city"":""Paris"",
            ""country"":""FR""
            },
          },
        ""company"":
            {
        ""name"":""Coffee Society"",
        ""SIRET"":""123456789""
            }     
       }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
        }

        [Test, Order(12)]
        public void CreateSubaccountUserPro()
        {
            var client = new RestClient("http://rest-integ.s-money.net/api/b2b/users/" + appUserIdP + "/subaccounts");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "54279142-ed9e-4afd-87c9-41658aba46a4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/vnd.s-money.v1+json");
            request.AddHeader("Accept", "application/vnd.s-money.v1+json");
            request.AddHeader("Authorization", "Bearer Mjs3ODszbH43Wk4zcGlB");
            request.AddParameter("undefined", @"
        {
        ""appaccountid"":""" + appAccountIdP + @""",     
         ""displayname"":""" + "C'est le sous compte de " + appUserIdP + @"""
       }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(Enum.Parse(typeof(HttpStatusCode), "Created"), response.StatusCode);
        }
       
    }
}
