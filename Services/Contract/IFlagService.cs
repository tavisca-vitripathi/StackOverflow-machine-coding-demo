using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    interface IFlagService
    {
        Task<bool> AddFlag(Flag flag, string id, string subid);
        Task<bool> ApproveFlag(string userId, string id, string subid);
    }
}
