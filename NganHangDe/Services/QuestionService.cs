using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;
        public QuestionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionViewModel>> GetQuestionsByCategoryIdAsync(int categoryId)
        {
            return await _context.Questions
                .Where(q => q.CategoryId == categoryId)
                .Select(q => new QuestionViewModel { Id = q.Id, Text = q.Text })
                .ToListAsync();
        }
        public async Task<Question> GetFullQuestionById (int id)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
