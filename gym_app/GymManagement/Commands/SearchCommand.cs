using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GymManagement.ViewModels;

namespace GymManagement.Commands
{
    public class SearchCommand : BaseAsyncCommand
    {
        public readonly SearchBarViewModel _searchViewModel;

        public SearchCommand(SearchBarViewModel searchbarviewmodel)
        {
            _searchViewModel = searchbarviewmodel;
            _searchViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchBarViewModel.SearchingContent) )
            {
                OnExecutedChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            FrameworkElement usercontrol = GetWindowParent(parameter as UserControl);
            var w = usercontrol as UserControl;
            if (w != null)
            {
                var ContextSearch = w.DataContext as BaseViewModel;
                ContextSearch.SearchData(_searchViewModel.SearchingContent);
            }
        }

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
