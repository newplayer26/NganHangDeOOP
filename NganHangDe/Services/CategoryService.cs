﻿using Microsoft.EntityFrameworkCore;
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
                var categoryViewModels = new List<CategoryModel>();
                var categoryList = await _context.Categories.ToListAsync();
                var topCategories = categoryList.Where(c => c.ParentCategoryId == null);
                foreach (var category in topCategories)
                {
                    AddCategoryWithIndentation(category, "", categoryViewModels, categoryList);
                }
                return categoryViewModels;
            }
        }
        private void AddCategoryWithIndentation(Category category, string level, List<CategoryModel> categoryViewModels, List<Category> allCategories)
        {
            categoryViewModels.Add(new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Level = level
            });

            var childCategories = allCategories.Where(c => c.ParentCategoryId == category.Id);

            foreach (var childCategory in childCategories)
            {
                AddCategoryWithIndentation(childCategory, level + "   ", categoryViewModels, allCategories);
            }
        }
    }
}
