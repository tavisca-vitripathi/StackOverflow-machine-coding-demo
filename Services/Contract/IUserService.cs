using DomainModel;
using System.Threading.Tasks;

namespace Services
{
    interface IUserService
    {
        Task AddUser(User user);
        Task<bool> AutheticateUser(string username, string password);
        Task AddBadgestToUser(string userId,Badgets Badget);
    }
}