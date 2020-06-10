using DomainModel;
using Store;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserStore _userStore;
        public UserService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task AddBadgestToUser(string userId, Badgets Badget)
        {
            var user = await _userStore.GetUser(userId);
            user.BadgestsEarned.Add(Badget);
            await _userStore.UpdateUser(userId,user);
        }

        public async Task AddUser(User user)
        {
            await _userStore.AddUser(user);

        }

        public async Task<bool> AutheticateUser(string username, string password)
        {
            var user = await _userStore.GetUser(username, password);
            if (user != null) return true;
            return false;
        }
    }
}
