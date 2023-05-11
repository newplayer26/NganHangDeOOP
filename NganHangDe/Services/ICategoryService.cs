﻿using NganHangDe.Models;
using NganHangDe.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<Category> GetFullCategoryById(int categoryId);
    }

}
