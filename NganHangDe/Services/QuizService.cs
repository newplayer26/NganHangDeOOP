using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ViewModels;
using NganHangDe.ViewModels.StartUpViewModels;

namespace NganHangDe.Services
{
    public class QuizService : IQuizService
    {
        public async Task<List<QuizViewModel>> GetAllQuizzesAsync()
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Quizzes
                    .Select(q => new QuizViewModel { Id = q.Id, Name = q.Name })
                    .ToListAsync();
            }
        }
        public async Task<Quiz> GetFullQuizById(int id)
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Quizzes
                    .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Question)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }
        }

    }

}
