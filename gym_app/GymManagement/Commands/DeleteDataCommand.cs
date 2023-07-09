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

namespace GymManagement.Commands
{
    public class DeleteDataCommand : BaseAsyncCommand
    {
        private readonly BaseViewModel _viewmodel;

        public override async Task ExecuteAsync(object parameter)
        {
            Decentralization role = Decentralization.GetDecentralization();
            if (role.RoleId == 1)
            {
                var view = new SampleConfirmDialog();
                var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandlerForDeleteData, null);
            }
            else
            {
                var view = new SampleMessageDialog()
                {
                    DataContext = new SampleMessageViewModel("Bạn không được cấp quyền để xóa thông tin!")
                };
                DialogHost.Show(view, "RootDialog");
            }
        }
        
        private async void ExtendedClosingEventHandlerForDeleteData(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            eventArgs.Cancel();

            
            try
            {
                await _viewmodel.DeleteData(_viewmodel.SelectedItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(0))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());

        }
        public DeleteDataCommand(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.SelectedItem))
            {
                OnExecutedChanged();
            }
        }
    }
}
