using System.Collections.Generic;

namespace DomainModel
{
    public class Answer : Upvotes
    {
        public string Id { get; set; }
        public string AnsweredBy { get; set; }
        public List<string> Answers { get; set; }
        public Flag Flag { get; set; }
    }
}