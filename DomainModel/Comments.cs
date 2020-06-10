using System.Collections.Generic;

namespace DomainModel
{
    public class Comment:Upvotes
    {
        public string id { get; set; }
        public string  UserId { get; set; }
        public List<string> Comments { get; set; }
        public Flag Flag { get; set; }

    }
}