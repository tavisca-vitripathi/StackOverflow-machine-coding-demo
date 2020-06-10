using System.Threading.Tasks;
using DomainModel;

namespace Store
{
    public interface IUserStore
    {
        Task AddUser(User user);
        Task<User> GetUser(string username, string password);
        Task<User> GetUser(string userId);
        Task UpdateUser(string userId,User user);
    }
}