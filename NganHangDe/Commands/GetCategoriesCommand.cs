using NganHangDe.DisplayModel;
using NganHangDe.Services;
using NganHangDe.ViewModels;
using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
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
        private List<CategoryDisplayModel> _collection;
        private Action<List<CategoryDisplayModel>> _func;
        private CategoryService _service;

        public CategoryService Service
        {
            get { return _service; }
            set { _service = value; }
        }

     
        
        public GetCategoriesCommand(Action<List<CategoryDisplayModel>> func)
        {
            //_collection = collection;
            _func = func;
            _service = new CategoryService();
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                List<CategoryDisplayModel> list =  await Service.GetAllCategoriesAsync();
                _func(list);
            }
            catch (Exception)
            {
      
            }
        }
    }
}
