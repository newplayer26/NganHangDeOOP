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

namespace NganHangDe.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<Category> GetFullCategoryById(int categoryId)
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Categories
                    .Include(c => c.Questions)
                    .ThenInclude(q => q.Answers)
                    .Include(c => c.Questions)
                    .ThenInclude(q => q.QuizQuestions)
                    .Include(c => c.ChildCategories)
                    .Include(c => c.ParentCategory)
                    .SingleOrDefaultAsync(c => c.Id == categoryId);
            }
        }
        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            using (var _context = new AppDbContext ())
            {
                var CategoryModels = new List<CategoryModel>();
                var categoryList = await _context.Categories.Include(c => c.Questions).ToListAsync();
                var topCategories = categoryList.Where(c => c.ParentCategoryId == null);
                foreach (var category in topCategories)
                {
                    AddCategoryWithIndentation(category, "", CategoryModels, categoryList);
                }
                return CategoryModels;
            }
        }
        public async Task<List<CategoryModel>> GetAllCategoriesAsync(int quizId)
        {
            using (var _context = new AppDbContext())
            {
                var CategoryModels = new List<CategoryModel>();
                var categoryList = await _context.Categories.Include(c => c.Questions).ToListAsync();
                var topCategories = categoryList.Where(c => c.ParentCategoryId == null);
                foreach (var category in topCategories)
                {
                    await AddCategoryWithIndentation(category, "", CategoryModels, categoryList, quizId);
                }
                return CategoryModels;
            }
        }
        private async Task AddCategoryWithIndentation(Category category, string level, List<CategoryModel> CategoryModels, List<Category> allCategories, int quizId)
        {
            List<Question> unassignedQuestion = new List<Question>();
            using (var _context = new AppDbContext())
            {
                unassignedQuestion = await _context.Questions
                    .Include(q => q.QuizQuestions)
                    .Where(q => q.CategoryId == category.Id)
                    .Where(q => !q.QuizQuestions.Any(qq => qq.QuizId == quizId))
                    .ToListAsync();
            }
            CategoryModels.Add(new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Level = level,
                QuestionsNumber = unassignedQuestion.Count
            }) ;

            var childCategories = allCategories.Where(c => c.ParentCategoryId == category.Id);

            foreach (var childCategory in childCategories)
            {
                await AddCategoryWithIndentation(childCategory, level + "  ", CategoryModels, allCategories, quizId);
            }
        }
        private void AddCategoryWithIndentation(Category category, string level, List<CategoryModel> CategoryModels, List<Category> allCategories)
        {
            CategoryModels.Add(new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Level = level,
                QuestionsNumber = category.Questions.Count
            }) ;

            var childCategories = allCategories.Where(c => c.ParentCategoryId == category.Id);

            foreach (var childCategory in childCategories)
            {
                AddCategoryWithIndentation(childCategory, level + "  ", CategoryModels, allCategories);
            }
        }

        public async Task CreateCategoryAsync(int? parentCategoryId, string name, string info)
        {
            using (var _context = new AppDbContext())
            {
                _context.Categories.Add(new Category
                {
                    Name = name,
                    Info = info,
                    ParentCategoryId = parentCategoryId,
                });
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> GetNumberofCategoriesAsync()
        {
            using (var _context = new AppDbContext())
            {
                return await _context.Categories.CountAsync();
            }
        }
        
        public async Task<int> GetRandomCategoryIdAsync()
        {
            using (var _context = new AppDbContext())
            {
                Random random = new Random();
                var categoryIds = await _context.Categories
                    .Select(c => c.Id)
                    .ToListAsync();

                return categoryIds[random.Next(categoryIds.Count)];
            }
        }
    }
}
