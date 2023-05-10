using Microsoft.EntityFrameworkCore;
using NganHangDe.DataAccess;
using NganHangDe.DisplayModel;
using NganHangDe.Models;
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
        public async Task<List<CategoryDisplayModel>> GetAllCategoriesAsync()
        {
            using (var _context = new AppDbContext ())
            {
                var categoryDisplayModels = new List<CategoryDisplayModel>();
                var categoryList = await _context.Categories.ToListAsync();
                var topCategories = categoryList.Where(c => c.ParentCategoryId == null);
                foreach (var category in topCategories)
                {
                    AddCategoryWithIndentation(category, "", categoryDisplayModels, categoryList);

                }
                return categoryDisplayModels;
            }
        }
        private void AddCategoryWithIndentation(Category category, string level, List<CategoryDisplayModel> categoryDisplayModels, List<Category> allCategories)
        {
            categoryDisplayModels.Add(new CategoryDisplayModel
            {
                Id = category.Id,
                Name = category.Name,
                Level = level + "  "
            }) ;

            var childCategories = allCategories.Where(c => c.ParentCategoryId == category.Id);

            foreach (var childCategory in childCategories)
            {
                AddCategoryWithIndentation(childCategory, level + 1, categoryDisplayModels, allCategories);
            }
        }

        Task<List<CategoryDisplayModel>> ICategoryService.GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }

}
