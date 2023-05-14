using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NganHangDe.Commands;
using NganHangDe.Models;
using NganHangDe.Services;
using NganHangDe.ViewModels.StartUpViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace NganHangDe.ViewModels.Components
{
    public class NewQuestionViewModel : DependencyObject
    {
        public ObservableCollection<UIElement> Choices
        {
            get { return (ObservableCollection<UIElement>)GetValue(ChoicesProperty); }
            set { SetValue(ChoicesProperty, value); }
        }

        public static readonly DependencyProperty ChoicesProperty =
            DependencyProperty.Register("Choices", typeof(ObservableCollection<UIElement>),
                                        typeof(NewQuestionViewModel), new PropertyMetadata(null));

        public ICommand AddChoicesCommand { get; }

        public NewQuestionViewModel()
        {
            Choices = new ObservableCollection<UIElement>();

            AddChoicesCommand = new RelayCommand<object>(AddChoices);
        }

        private void AddChoices(object? parameter)
        {
            for (int i = 0; i < 3; i++)
            {
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) }); 
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Choice " + (i + 1);
                textBlock.FontSize = 16;
                textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                textBlock.Margin = new Thickness(0, 10, 10, 0);
                Grid.SetColumn(textBlock, 0);
                grid.Children.Add(textBlock);

                TextBox textBox = new TextBox();
                textBox.Width = 250;
                textBox.HorizontalAlignment = HorizontalAlignment.Left;
                textBox.Margin = new Thickness(20, 10, 0, 0);
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.AcceptsReturn = true;
                Grid.SetColumn(textBox, 1);
                grid.Children.Add(textBox);

                Choices.Add(grid);
            }

        }
    }
}
