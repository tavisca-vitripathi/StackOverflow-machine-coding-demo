using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    interface IQuestionService
    {
        Task AddQuestion(Question Question );
        Task<bool> DeleteQuestion(string userId, string QuestionId );
        Task<bool> CloseQuestion(string questionId, string UserId);
        Task<Question> GetQuestion(string questionId);
        Task<List<Question>> SearchByTitle(string title);
        Task AddBounty(string questionId, int bounty);
        Task AddTags(string question, List<string> tags);
        Task<List<string>> GetFrequentTags(string questionId);
        Task<bool> UnDeleteQuestion(string userId, string questionId);
        Task<List<Question>> GetFlaggedQuestion();
    }
}
