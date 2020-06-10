using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class User
    {

        public string Id { get; set; }
        public string Password { get; set; }
        public string  UserName { get; set; }
        public List<Badgets> BadgestsEarned { get; set; }
        public string Email { get; set; }
        public virtual Role UserRole() => Role.Undefined;
    }
}
