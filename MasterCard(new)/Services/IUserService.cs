namespace MasterCard.Services
{
    public interface IUserService
    {
        Task<Boolean> LogIn(string username, string password);
    }
}
