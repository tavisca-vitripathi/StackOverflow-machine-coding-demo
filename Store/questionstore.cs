using DomainModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store
{
    public class QuestionStore : IQuestionStore
    {
        private IUserStore _userStore;
        private static ConcurrentDictionary<string, Question> _store = new ConcurrentDictionary<string, Question>(StringComparer.OrdinalIgnoreCase);
        public QuestionStore(IUserStore userStore)
        {
            _userStore = userStore;
        }
        public Task AddQuestion(Question question)
        {
            question.Id = Guid.NewGuid().ToString();
            _store.TryAdd(question.Id, question);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteQuestion(string userId, string questionId)
        {
            var question = await GetQuestion(questionId);
            var user = await _userStore.GetUser(userId);
            if (question != null && user != null)
            {
                if (question.QuestionOwnerId == userId || (user is Moderator))
                {
                    question.isDeleted = true;
                    await UpdateQuestion(questionId, question);
                    return true;
                }
            }
            return false;
        }

        public async Task<Question> GetQuestion(string questionId)
        {
            Question question;
            var result = _store.TryGetValue(questionId, out question);
            if (result == true && question.isDeleted == false) return question;
            return null;

        }

        public async Task UpdateQuestion(string questionId, Question question)
        {
            var questionToUpdate = _store.Values.FirstOrDefault(x => x.Id.Equals(questionId, StringComparison.OrdinalIgnoreCase));
            _store.TryUpdate(questionId, question, questionToUpdate);

        }

        public async Task<List<Question>> GetAllUnDeletedQuestions()
        {
            var questions = _store.Values.Where(x => x.isDeleted == false);
            return questions.ToList();
        }
    }
}
