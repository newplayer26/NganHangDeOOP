using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

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
                    .SingleOrDefaultAsync(q => q.Id == id);
            }
        }
        public async Task<List<QuestionModel>> GetSubcategoriesQuestionsByCategoryIdAsync(int categoryId)
        {
            List<QuestionModel> subcategoriesQuestions = new List<QuestionModel>();
            Category topCategory = await _categoryService.GetFullCategoryById(categoryId);
            await AddQuestionsFromDescendants(topCategory.Id, subcategoriesQuestions);
            return subcategoriesQuestions;
        }
        private async Task AddQuestionsFromDescendants(int topCategoryId, List<QuestionModel> subcategoriesQuestions)
        {
            Category topCategory = await _categoryService.GetFullCategoryById(topCategoryId);
            foreach (Question question in topCategory.Questions)
            {
                subcategoriesQuestions.Add(new QuestionModel { Id = question.Id, Text = question.Text });
            }
            foreach(Category childCategory in topCategory.ChildCategories)
            {
                await AddQuestionsFromDescendants(childCategory.Id, subcategoriesQuestions);
            }
        }
        public async Task CreateQuestionAsync(QuestionModel questionModel, int categoryId, List<AnswerModel> answerModels)
        {
            using (var _context = new AppDbContext())
            {
                var question = new Question
                {
                    Name = questionModel.Name,
                    Text = questionModel.Text,
                    CategoryId = categoryId,
                    Answers = new List<Answer>()
                };
                foreach (var answerModel in answerModels)
                {
                    question.Answers.Add(new Answer
                    {
                        Text = answerModel.Text,
                        Grade = answerModel.Grade,
                    });
                }
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
            }
        }
        public async Task EditQuestionAsync(QuestionModel questionModel, int categoryId, List<AnswerModel> answerModels)
        {
            using (var _context = new AppDbContext())
            {
                var question = await _context.Questions
                    .Include(q => q.Answers)
                    .SingleOrDefaultAsync(q => q.Id == questionModel.Id);
                question.Name = questionModel.Name;
                question.Text = questionModel.Text;
                question.CategoryId = categoryId;
                _context.Answers.RemoveRange(question.Answers);
                question.Answers.Clear();
                foreach (var answerModel in answerModels)
                {
                    question.Answers.Add(new Answer
                    {
                        Text = answerModel.Text,
                        Grade = answerModel.Grade
                    });
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
