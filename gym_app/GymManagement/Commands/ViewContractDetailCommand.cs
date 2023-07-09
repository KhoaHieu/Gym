using GymManagement.Dialogs;
using GymManagement.Services;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    class ViewContractDetailCommand : BaseCommand
    {
        private ContractViewModel _viewmodel;
        public override void Execute(object? parameter)
        {
            ContractDetail mainWindow = new ContractDetail()
            {
                DataContext = new ContractDetailViewModel(_viewmodel)
            };

            mainWindow.Show();
           
            //var view = new ContractDetailDialog
            //{
            //    DataContext = new ContractDetailViewModel(_viewmodel)
            //};
            //var result = DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
        }
        public ViewContractDetailCommand(ContractViewModel viewmodel)
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
        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
               parameter == false) return;



            var view = eventArgs.Session.Content as ContractDetailDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as ContractDetailViewModel;
                if (viewmodel != null)
                {
                    
                    //_viewmodel.BookingContext.Add(viewmodel.NewBookingUpdate);
                    
                }

            }


            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
