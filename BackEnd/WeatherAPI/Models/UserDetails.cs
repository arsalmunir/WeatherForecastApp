using System;
namespace WeatherAPI.Models
{
    public class UserDetails
    {

        public string username { get; set; }
        public string password { get; set; }

        public UserDetails()
        {

        }

        public UserDetails(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}

