using NganHangDe.Models;
using NganHangDe.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NganHangDe.Commands
{
    public class GetCategoriesWithUnassignedQuestionsCommand : AsyncCommandBase
    {
        private Action<List<CategoryModel>> _loadCategories;
        private CategoryService _service = new CategoryService();
        private int _quizId;
        public GetCategoriesWithUnassignedQuestionsCommand(Action<List<CategoryModel>> loadCategories, int quizId)
        {
            _loadCategories = loadCategories;
            _quizId = quizId;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                List<CategoryModel> list = await _service.GetAllCategoriesAsync(_quizId);
                _loadCategories(list);
            }
            catch (Exception)
            {

            }
        }
    }
}
