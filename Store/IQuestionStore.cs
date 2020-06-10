using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IQuestionStore
    {
        Task AddQuestion(Question question);
        Task<Question> GetQuestion(string questionId);
        Task<bool> DeleteQuestion(string userId, string questionId);
        Task UpdateQuestion(string questionId, Question question);
        Task<List<Question>> GetAllUnDeletedQuestions();
    }
}