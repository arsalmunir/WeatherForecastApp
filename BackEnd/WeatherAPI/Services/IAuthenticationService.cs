using System;
namespace WeatherAPI.Services
{
    public interface IAuthenticationService
    {
        string AuthenticationProcess(string username, string password);
        bool ValidateSession(string sessionID);

    }
}

