using DomainModel;
using Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService : ICommentService, IUpvotes,IFlagService
    {

        private IQuestionStore _store;
        private IUserStore _userStore;
        public CommentService(IQuestionStore store ,IUserStore userStore)
        {
            _store = store;
            _userStore = userStore;
        }

        public async Task AddComment(string questionId, Comment comment)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                if (question.Comments == null)
                {
                    question.Comments = new List<Comment>();
                }
                comment.id = Guid.NewGuid().ToString();
                question.Comments.Add(comment);
                await _store.UpdateQuestion(questionId, question);
            }

        }

        public async Task<bool> AddFlag(Flag flag, string questionid, string commentid)
        {
            var question = await _store.GetQuestion(questionid);
            if(question != null)
            {
                var comment = question.Comments.Find(x => x.id == commentid);
                flag.Id = Guid.NewGuid().ToString();
                comment.Flag = flag;
                question.IsCommentFlagged = true;
                question.FlaggedCommentId = commentid;
                await _store.UpdateQuestion(questionid, question);
                return true;
            }
            return false;
        }
        public async Task<bool> AddUpvote(string UserId, string questionId,string commentId)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var comment = question.Comments.Find(x => x.id == commentId);
                if (comment != null)
                {
                    if(comment.UpvotedBy == null)
                    {
                        comment.UpvotedBy = new HashSet<string>();
                    }
                    comment.UpvotedBy.Add(UserId);
                    await _store.UpdateQuestion(questionId, question);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> ApproveFlag(string userId, string questionid, string commentId)
        {
            var user = await _userStore.GetUser(userId);
            var question = await _store.GetQuestion(questionid);
            if (user != null && question != null)
            {
                if (user is Moderator && question.Answers != null)
                {
                    var comment = question.Comments.Find(x => x.id == commentId);
                    if (comment != null)
                    {
                        comment.Flag.IsApproved = true;
                        comment.Flag.ApprovedBy = (Moderator)user;
                        question.IsCommentFlagged = false;
                        question.FlaggedCommentId = null;
                        await _store.UpdateQuestion(questionid, question);
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<Comment> GetComment(string questionId, string commentId)
        {

            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var comment = question.Comments.Find(x => x.id == commentId);
                return comment;
            }
            return null;
        }

        public async  Task<List<Comment>> GetComments(string questionId)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var comment = question.Comments;
                return comment;
            }
            return null;
        }

        public async Task<List<Question>> GetFlaggedCommentedQuestions()
        {
            var questions = await _store.GetAllUnDeletedQuestions();
            if (questions != null)
            {
                var result = questions.FindAll(x => x.IsCommentFlagged == true);
                return result;
            }
            return null;

        }
    }
}
