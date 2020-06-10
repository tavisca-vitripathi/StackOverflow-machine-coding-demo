using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
   public  class Upvotes
    {
        public HashSet<string> UpvotedBy { get; set; }
        public int upvotesCount() => UpvotedBy.Count;
    }
}
