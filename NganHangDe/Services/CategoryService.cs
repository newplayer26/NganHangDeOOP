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
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categoryViewModels = new List<CategoryViewModel>();
            var categoryList = await _context.Categories.ToListAsync();
            var topCategories = categoryList.Where(c => c.ParentCategoryId == null);
            foreach (var category in topCategories)
            {
                AddCategoryWithIndentation(category, 0, categoryViewModels, categoryList);
            }

            return categoryViewModels;
        }
        private void AddCategoryWithIndentation(Category category, int level, List<CategoryViewModel> categoryViewModels, List<Category> allCategories)
        {
            categoryViewModels.Add(new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Level = level
            });

            var childCategories = allCategories.Where(c => c.ParentCategoryId == category.Id);

            foreach (var childCategory in childCategories)
            {
                AddCategoryWithIndentation(childCategory, level + 1, categoryViewModels, allCategories);
            }
        }
    }

}
