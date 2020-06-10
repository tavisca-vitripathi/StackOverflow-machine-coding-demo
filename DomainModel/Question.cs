using System.Collections.Generic;

namespace DomainModel
{
    public class Question : Upvotes
    {
        public  string Title;
        public string Id { get; set; }
        public HashSet<string> Tags { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Answer> Answers { get; set; }
        public string QuestionOwnerId { get; set; }
        public bool IsClosed { get; set; }
        public int Bounty { get; set; }
        public Flag Flags { get; set; } = null;
        public bool IsQuestionFlagged { get; set; }
        public bool IsAnswerFlaged { get; set; } = false;
        public bool IsCommentFlagged { get; set; } = false;
        public string FlaggedCommentId { get; set; } = null;
        public string FlaggedAnswerId { get; set; } = null;
        public bool isDeleted { get; set; }

    }
}
