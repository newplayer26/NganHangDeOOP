using NganHangDe.Stores;
using NganHangDe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NganHangDe.Commands
{
    public class NavigateCommand<T> : CommandBase where T: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private Type _objectType;
        public NavigateCommand(NavigationStore navigationStore, Type objectType)
        {
            _navigationStore = navigationStore;
            if (objectType == null || !typeof(T).IsAssignableFrom(objectType))
            {
                throw new ArgumentException("Invalid object type");
            }
            _objectType = objectType;
        }

        public override void Execute(object parameter)

        {
            object[] args = new object[] { _navigationStore };
            _navigationStore.CurrentViewModel = (T)Activator.CreateInstance(_objectType, args);   
        }
    }
}
