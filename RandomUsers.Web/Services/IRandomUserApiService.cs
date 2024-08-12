using RandomUsers.Web.Models;

namespace RandomUsers.Web.Services
{
    public interface IRandomUserApiService
    {
        Task<List<UserModel>> GetUsers();
    }
}
