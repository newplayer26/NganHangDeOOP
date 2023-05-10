using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface IQuizService
    {
        Task<List<QuizModel>> GetAllQuizzesAsync();
        Task<Quiz> GetFullQuizById(int id);
    }

}
