using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ICategoryService _categoryService;
        public QuestionService()
        {
            _categoryService = new CategoryService();
        }
        public async Task<List<QuestionModel>> GetQuestionsByCategoryIdAsync(int categoryId)
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Questions
                    .Where(q => q.CategoryId == categoryId)
                    .Select(q => new QuestionModel { Id = q.Id, Text = q.Text })
                    .ToListAsync();
            }
            
        }
        public async Task<Question> GetFullQuestionById(int id)
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Questions
                    .Include(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }
        }
        public async Task<List<QuestionModel>> GetSubcategoriesQuestionsByCategoryIdAsync(int categoryId)
        {
            List<QuestionModel> subcategoriesQuestions = new List<QuestionModel>();
            Category topCategory = await _categoryService.GetFullCategoryById(categoryId);
            AddQuestionsFromDescendants(topCategory, subcategoriesQuestions);
            return subcategoriesQuestions;
        }
        private void AddQuestionsFromDescendants(Category topCategory, List<QuestionModel> subcategoriesQuestions)
        {
            foreach(Question question in topCategory.Questions)
            {
                subcategoriesQuestions.Add(new QuestionModel { Id = question.Id, Text = question.Text });
            }
            foreach(Category childCategory in topCategory.ChildCategories)
            {
                AddQuestionsFromDescendants(childCategory, subcategoriesQuestions);
            }
        }
    }
}
