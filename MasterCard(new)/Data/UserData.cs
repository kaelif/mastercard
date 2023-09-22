using Amazon.DirectConnect.Model;
using MasterCard.Objects;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MasterCard.Data
{
    public class UserData
    {
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-FJE7043P;Integrated Security=True");
        public UserData() { }

        public async Task<bool> LogIn(string username, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "");

                String queryString = "EXECUTE [dbo].[ConfirmSignIn] @Username, @Password";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);

                command.Connection.Open();
                return await command.ExecuteReaderAsync().Result.ReadAsync();
            }
        }
    }
}
