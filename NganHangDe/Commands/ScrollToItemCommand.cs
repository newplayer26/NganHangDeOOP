using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using NganHangDe.Models;
using System.Windows.Controls;

namespace NganHangDe.Commands
{
    public class ScrollToItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ObservableCollection<QuestionModel> _itemCollection;
        private ItemsControl _questionItemsControl;
        private ScrollViewer _questionScrollViewer;
        public ScrollToItemCommand(ObservableCollection<QuestionModel> itemCollection, ItemsControl questionItemsControl, ScrollViewer questionScrollViewer)
        {
            _questionScrollViewer   = questionScrollViewer;
            _questionItemsControl = questionItemsControl;
            _itemCollection = itemCollection;
        }
        public bool CanExecute(object parameter)
        {
            // Add your own validation logic here if needed
            return true;
        }
        public void RenewItemsCollection(ObservableCollection<QuestionModel> itemCollection)
        {
            _itemCollection = itemCollection;   
        }

        public void Execute(object parameter)
        {
        
            var button = (Button)parameter;
            QuestionModel item = (QuestionModel)button.DataContext;
            Console.WriteLine(_questionItemsControl);
            //var index = _itemCollection.IndexOf(item);
            var index = 0;
            if (index >= 0)
            {
                //var itemContainer = (FrameworkElement)_questionItemsControl.ItemContainerGenerator.ContainerFromItem(item);
                //var offset = itemContainer.TransformToAncestor(_questionScrollViewer).Transform(new Point(0, 0)).Y;
                _questionScrollViewer.ScrollToVerticalOffset(5);
            }
        }
    }

}
