using NganHangDe.ViewModels.TabbedNavigationTabViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NganHangDe.Views.TabbedNavigationTabViews
{
    /// <summary>
    /// Interaction logic for ImportTab.xaml
    /// </summary>
    public partial class ImportTabView : UserControl
    {
        public ImportTabView()
        {
            InitializeComponent();

        }
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            var viewModel = DataContext as ImportTabViewModel;
            if (viewModel != null && viewModel.DropCommand.CanExecute(e))
            {
                viewModel.DropCommand.Execute(e);
            }
            e.Handled = true;
        }
    }
}
