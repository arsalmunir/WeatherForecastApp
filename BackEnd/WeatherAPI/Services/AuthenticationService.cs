using System;
using WeatherAPI.Models;
using WeatherAPI.Models.ForecastWeatherModels;
using WeatherAPI.ExceptionHandling;

namespace WeatherAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {


        IDictionary<string, UserDetails> users = new Dictionary<string, UserDetails>();

        readonly List<UserDetails> userDetailList = new List<UserDetails>();

        public AuthenticationService()
        {
            userDetailList.Add(new UserDetails("arsal", "arsal123"));
            userDetailList.Add(new UserDetails("john" , "john123"));
        }

        public string AuthenticationProcess(string username, string password)
        {
            foreach(var userNumber in userDetailList)
            {
                if(userNumber.username == username && userNumber.password == password)
                {
                    string sessionID = GenerateRandomString();

                    users.Add(sessionID, userNumber);
                    return sessionID;
                }
            }

            throw new HttpResponseException(401, "Unauthorised");
        }
        
        private string GenerateRandomString()
        {
            Random random = new Random();
            int length = 16;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }

            return rString;
        }

        public bool ValidateSession(string sessionID)
        {
            return (users.ContainsKey(sessionID))? true: false;          
        }

    }
}

