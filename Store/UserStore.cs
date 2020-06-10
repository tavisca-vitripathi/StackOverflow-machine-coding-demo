using DomainModel;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Store
{
    public class UserStore : IUserStore
    {
        private static ConcurrentDictionary<string, User> _userStore = new ConcurrentDictionary<string, User>(StringComparer.OrdinalIgnoreCase);
        public async Task AddUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _userStore.TryAdd(user.Id, user);
        }
        public async Task<User> GetUser(string username, string password)
        {
            var user = _userStore.Values.FirstOrDefault(x => x.UserName == username && x.Password == password);
            return user;
        }

        public async Task<User> GetUser(string userId)
        {
            return _userStore.Values.FirstOrDefault(x => x.Id == userId);
        }

        public async Task UpdateUser(string userId,User user)
        {
            var userToUpdate = await GetUser(userId);
            _userStore.TryUpdate(userId, userToUpdate, user);
        }
    }
}
