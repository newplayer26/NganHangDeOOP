using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IQuestionService
    {
        Task<Question> GetFullQuestionById(int id);
        Task<List<QuestionModel>> GetQuestionsByCategoryIdAsync(int categoryId);
        Task<List<QuestionModel>> GetSubcategoriesQuestionsByCategoryIdAsync(int categoryId);
        Task<Question> CreateQuestionAsync(QuestionModel questionModel, int categoryId, List<AnswerModel> answerModels);
        Task<Question> EditQuestionAsync(QuestionModel questionModel, int categoryId, List<AnswerModel> answerModels);
        Task<List<QuestionModel>> GetUnassignedQuestionsAsync(int quizId, int categoryId);
        Task<List<QuestionModel>> GetUnassignedSubcategoriesQuestionsAsync(int quizId, int categoryId);

    }
}
