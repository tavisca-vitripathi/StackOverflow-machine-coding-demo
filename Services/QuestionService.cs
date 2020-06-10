using DomainModel;
using Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionService : IQuestionService, IUpvotes, IFlagService
    {
        private IQuestionStore _store;
        private IUserStore _userStore;
        public QuestionService(IQuestionStore Store, IUserStore userStore)
        {
            _store = Store;
            _userStore = userStore;
        }

        public async Task AddBounty(string questionId, int bounty)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                question.Bounty = bounty;
                await _store.UpdateQuestion(questionId, question);
            }
        }

        public async Task<bool> AddFlag(Flag flag, string questionId, string subid = null)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                flag.Id = Guid.NewGuid().ToString();
                question.Flags = flag;
                question.IsQuestionFlagged = true;
                await _store.UpdateQuestion(questionId, question);
                return true;
            }
            return false;
        }

        public async Task AddQuestion(Question question)
        {
            await _store.AddQuestion(question);
        }

        public async Task AddTags(string questionId, List<string> tags)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                if (question.Tags == null)
                {
                    question.Tags = new HashSet<string>();
                }
                foreach (var tag in tags)
                {
                    question.Tags.Add(tag);
                }
                await _store.UpdateQuestion(questionId, question);
            }

        }

        public async Task<bool> AddUpvote(string UserId, string questionId, string id = null)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                if (question.UpvotedBy == null)
                {
                    question.UpvotedBy = new HashSet<string>();
                }
                question.UpvotedBy.Add(UserId);
                await _store.UpdateQuestion(questionId, question);
                return true;
            }
            return false;
        }

        public async Task<bool> ApproveFlag(string userId, string questionId, string subid = null)
        {
            var question = await _store.GetQuestion(questionId);
            var user = await _userStore.GetUser(userId);
            if (question != null && user is Moderator && question.IsQuestionFlagged == true)
            {
                question.Flags.IsApproved = true;
                question.Flags.ApprovedBy = (Moderator)user;
                question.IsQuestionFlagged = false;
                await _store.UpdateQuestion(questionId, question);
                return true;
            }
            return false;
        }

        public async Task<bool> CloseQuestion(string questionId, string UserId)
        {
            var question = await _store.GetQuestion(questionId);
            var user = await _userStore.GetUser(UserId);
            if (question != null && user is Moderator)
            {
                if (!question.IsClosed)
                {
                    question.IsClosed = true;
                    return true;

                }
            }
            return false;
        }

        public async Task<bool> DeleteQuestion(string userId, string QuestionId)
        {
            var result = await _store.DeleteQuestion(userId, QuestionId);
            return result;
        }

        public async Task<List<Question>> GetFlaggedQuestion()
        {
            var questions = await _store.GetAllUnDeletedQuestions();
            if (questions != null)
            {
                var flaggedQuestions = questions.FindAll(x => x.IsQuestionFlagged == true);
                return flaggedQuestions;
            }
            return null;
        }

        public async Task<List<string>> GetFrequentTags(string questionId)
        {
            var question =await  _store.GetQuestion(questionId);
            var tags = question.Tags;
            var list = new List<string>();
            foreach(var tag in tags)
            {
                list.Add(tag);
            }
            return list;
        }

        public async Task<Question> GetQuestion(string questionId)
        {
           return await  _store.GetQuestion(questionId);
        }

        public async Task<List<Question>> SearchByTitle(string title)
        {
            var question = await _store.GetAllUnDeletedQuestions();
            if (question != null)
            {
                var searchedQuetion = question.FindAll(x => x.Title == title);
                return searchedQuetion;
            }
            return null;
        }

        public async Task<bool> UnDeleteQuestion(string userId, string questionId)
        {
            var question = await _store.GetQuestion(questionId);
            var user = await _userStore.GetUser(userId);
            if(question != null)
            {
                if((question.isDeleted ==false) && user is Moderator)
                {
                    question.isDeleted = false;
                    await _store.UpdateQuestion(questionId, question);
                    return true;
                }
            }
            return false;
        }
    }
}
