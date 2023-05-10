using NganHangDe.DisplayModel;
using NganHangDe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDisplayModel>> GetAllCategoriesAsync();
        Task<Category> GetFullCategoryById(int categoryId);
    }

}
