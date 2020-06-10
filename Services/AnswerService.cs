using DomainModel;
using Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class AnswerService : IAnswerService, IUpvotes, IFlagService
    {
        private IQuestionStore _store;
        private IUserStore _userStore;
        public AnswerService(IQuestionStore store)
        {
            _store = store;
        }

        public async Task<bool> AddAnswer(string questionid, Answer answer)
        {
            var question = await _store.GetQuestion(questionid);
            if (question != null)
            {
                if (question.Answers == null)
                {
                    answer.Id = Guid.NewGuid().ToString();
                    question.Answers = new List<Answer>();
                }
                question.Answers.Add(answer);
                await _store.UpdateQuestion(questionid, question);
                return true;
            }
            return false;
        }

        public async Task<bool> AddFlag(Flag flag, string questionId, string answerId)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var answer = question.Answers.FirstOrDefault(x => x.Id == answerId);
                if (answer != null)
                {
                    flag.Id = Guid.NewGuid().ToString();
                    answer.Flag = flag;
                    question.IsAnswerFlaged = true;
                    question.FlaggedAnswerId = answerId;
                    await _store.UpdateQuestion(questionId, question);
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> AddUpvote(string UserId, string questionId, string answerId)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var answer = question.Answers.FirstOrDefault(x => x.Id == answerId);
                if (answer != null)
                {
                    if (answer.UpvotedBy == null)
                    {
                        answer.UpvotedBy = new HashSet<string>();
                    }
                    answer.UpvotedBy.Add(UserId);
                    await _store.UpdateQuestion(questionId, question);
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ApproveFlag(string userId, string questionid, string answerid)
        {
            var user = await _userStore.GetUser(userId);
            var question = await _store.GetQuestion(questionid);
            if (user != null && question != null)
            {
                if (user is Moderator && question.Answers != null)
                {
                    var answer = question.Answers.Find(x => x.Id == answerid);
                    if (answer != null)
                    {
                        answer.Flag.IsApproved = true;
                        answer.Flag.ApprovedBy = (Moderator)user;
                        question.IsAnswerFlaged = false;
                        question.FlaggedAnswerId = null;
                        await _store.UpdateQuestion(questionid, question);
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<Answer> GetAnswer(string questionId, string answerid)
        {
            var question = await _store.GetQuestion(questionId);
            if (question != null)
            {
                var answers = question.Answers.Find(x => x.Id == answerid);
                return answers;
            }
            return null;
        }

        public async Task<List<Answer>> GetAnswers(string questionid)
        {
            var question = await _store.GetQuestion(questionid);
            if (question != null)
            {
                var answers = question.Answers;
                return answers;
            }
            return null;
        }

        public async Task<List<Question>> GetFlaggedAnswerQuestions()
        {
            var questions = await _store.GetAllUnDeletedQuestions();
            if (questions != null)
            {
                var result = questions.FindAll(x => x.IsAnswerFlaged == true);
                return result;
            }
            return null;

        }
    }
}
