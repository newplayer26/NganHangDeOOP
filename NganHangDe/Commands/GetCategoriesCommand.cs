using NganHangDe.Models;
using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class GetCategoriesCommand : AsyncCommandBase
    {
        private Action<List<CategoryModel>> _loadCategories;
        private CategoryService _service =  new CategoryService();
        public GetCategoriesCommand(Action<List<CategoryModel>> loadCategories)
        {
            _loadCategories = loadCategories;
             
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                List<CategoryModel> list =  await _service.GetAllCategoriesAsync();
                _loadCategories(list);
            }
            catch (Exception)
            {
      
            }
        }
    }
}
