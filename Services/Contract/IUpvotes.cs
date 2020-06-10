using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{ 
    interface IUpvotes
    {
        Task<bool> AddUpvote(string UserId,string Id,string subId);
    }
}
