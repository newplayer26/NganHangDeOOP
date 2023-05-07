using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ViewModels;

namespace NganHangDe.Services
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _context;

        public QuizService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuizViewModel>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes
                .Select(q => new QuizViewModel { Id = q.Id, Name = q.Name })
                .ToListAsync();
        }
        public async Task<Quiz> GetFullQuizById(int id)
        {
            return await _context.Quizzes
                .Include(q => q.QuizQuestions)
                .ThenInclude(qq => qq.Question)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }

}
