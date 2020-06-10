using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    interface IAnswerService
    {
        Task<bool> AddAnswer(string questionid, Answer answer);
        Task<List<Answer>> GetAnswers(string questionid);
        Task<Answer> GetAnswer(string qustionId, string answerid);
        Task<List<Question>> GetFlaggedAnswerQuestions();
    }
}
