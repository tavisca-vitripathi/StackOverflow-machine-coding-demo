using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    interface ICommentService
    {
        Task AddComment( string questionId , Comment comment);
        Task<List<Comment>> GetComments(string questionId);
        Task<Comment> GetComment(string questionId, string commentId);
        Task<List<Question>> GetFlaggedCommentedQuestions();
    }
}
