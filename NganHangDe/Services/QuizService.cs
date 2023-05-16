using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using NganHangDe.ViewModels;

namespace NganHangDe.Services
{
    public class QuizService : IQuizService
    {
        public async Task<List<QuizModel>> GetAllQuizzesAsync()
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Quizzes
                    .Select(q => new QuizModel { Id = q.Id, Name = q.Name })
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
        public async Task CreateQuizAsync(string name, string description, TimeSpan timeLimit)
        {
            using (var _context = new AppDbContext())
            {
                _context.Quizzes.Add(new Quiz
                {
                    Name = name,
                    Description = description,
                    TimeLimit = timeLimit
                });
                await _context.SaveChangesAsync();
            }

        }
    }

}
