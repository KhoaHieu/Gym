using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Utils;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GymManagement.Commands
{
    public class GridChangeCommand : BaseAsyncCommand
    {
        private readonly BaseViewModel _viewmodel;
        public override async Task ExecuteAsync(object parameter)
        {
            Decentralization role = Decentralization.GetDecentralization();
            if (role.RoleId == 1)
            {
                _viewmodel.UpdateData();
            }
            else
            {
                var view = new SampleMessageDialog()
                {
                    DataContext = new SampleMessageViewModel("Bạn không được cấp quyền để chỉnh sửa thông tin!")
                };
                DialogHost.Show(view, "RootDialog");
            }
            //Debug.WriteLine("Item: " + _viewmodel.SelectedItem);
            //Customer a = _viewmodel.SelectedItem as Customer;
            //if (a != null)
            //    Debug.WriteLine("Name: " + a.Name);
        }
        
        public GridChangeCommand(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }
        //public override bool CanExecute(object parameter)
        //{
        //    Decentralization role = Decentralization.GetDecentralization();
        //    if (role.RoleId == 1)
        //    {
        //        return true && base.CanExecute(parameter);
        //    }
        //    else
        //    {
        //        var view = new SampleMessageDialog()
        //        {
        //            DataContext = new SampleMessageViewModel("Bạn không được cấp quyền sử dụng chức năng này!")
        //        };
        //        DialogHost.Show(view, "RootDialog");
        //        return false && base.CanExecute(parameter);
        //    }
        //}
        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.SelectedItem))
            {
                OnExecutedChanged();
            }
        }
    }
}
