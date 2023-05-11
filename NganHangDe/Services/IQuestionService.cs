using NganHangDe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IQuestionService
    {
        Task<List<QuestionModel>> GetQuestionsByCategoryIdAsync(int categoryId);
        Task<List<QuestionModel>> GetSubcategoriesQuestionsByCategoryIdAsync(int categoryId);
        Task CreateQuestionAsync(QuestionModel questionModel, int categoryId, List<AnswerModel> answerModels);
    }
}
