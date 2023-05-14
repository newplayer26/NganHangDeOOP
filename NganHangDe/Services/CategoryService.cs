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
                    .Include(c => c.ChildCategories)
                    .FirstOrDefaultAsync(c => c.Id == categoryId);
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
                AddCategoryWithIndentation(childCategory, level + "   ", CategoryModels, allCategories);
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
    }
}
