using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
     public class RegularUser:User
    {
        public override Role UserRole ()=> Role.RegularUser;
    }
}
