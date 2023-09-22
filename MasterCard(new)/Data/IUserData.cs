using MasterCard.Objects;

namespace MasterCard.Data
{
    public interface IUserData
    {
        Task<Boolean> LogIn(string username, string password);
    }
}
