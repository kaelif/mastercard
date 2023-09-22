using MasterCard.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasterCard.Services
{
    public class UserService : IUserService
    {
        private readonly IUserData userData;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor; // Add this

        public UserService(IUserData _userData, IHttpContextAccessor httpContextAccessor)
        {
            this.userData = _userData;
            this.httpClient = new HttpClient();
            this.httpContextAccessor = httpContextAccessor; // Injected IHttpContextAccessor
        }

        public async Task<bool> LogIn(string username, string password)
        {
            try
            {
                // Validate the username
                if (!IsValidEmail(username))
                {
                    throw new ArgumentException("Invalid email format for username.");
                }

                // Validate the password
                if (!IsValidPassword(password))
                {
                    throw new ArgumentException("Invalid password format.");
                }

                // Get the user's IP address from the request
                var userIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                // Call ipinfo.io to get geolocation data
                var ipInfoResponse = await httpClient.GetStringAsync($"https://ipinfo.io/{userIpAddress}/json");
                var ipInfoData = Newtonsoft.Json.JsonConvert.DeserializeObject<IpInfoResponse>(ipInfoResponse);

                // Check if the country is Canada
                if (ipInfoData != null && string.Equals(ipInfoData.Country, "CA", StringComparison.OrdinalIgnoreCase))
                {
                    // If both username and password are valid, proceed with the login logic
                    return await userData.LogIn(username, password);
                }
                else
                {
                    throw new UnauthorizedAccessException("Access denied. User is not in Canada.");
                }
            }
            catch (Exception ex)
            {
                // Handle any validation or other exceptions here
                Console.WriteLine($"Login failed: {ex.Message}");
                return false; // or throw an exception or handle it as needed
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            return password.Length > 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private class IpInfoResponse
        {
            public string Country { get; set; }
        }
    }
}
