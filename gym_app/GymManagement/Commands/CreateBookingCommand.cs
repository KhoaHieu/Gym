using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Services;
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
    public class CreateBookingCommand : BaseCommand
    {

        private readonly BookingViewModel _viewmodel;
        //public override async Task ExecuteAsync(object parameter)
        //{
        //    var view = new ListOfContractsForBookingDialog
        //    {
        //        DataContext = new ListOfContractsForBookingViewModel(_viewmodel)
        //    };
        //    var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
        //}
        public CreateBookingCommand(BookingViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.SelectedItem)
                || e.PropertyName == nameof(BookingViewModel.BookingContext)
                || e.PropertyName == nameof(BookingViewModel.SeriesBooking)
                || e.PropertyName == nameof(BookingViewModel.SelectedStaff))
            {
                OnExecutedChanged();
            }
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
             if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            

            var view = eventArgs.Session.Content as ListOfContractsForBookingDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as ListOfContractsForBookingViewModel;
                if (viewmodel != null)
                {
                    viewmodel.AddNewBooking();
                    //_viewmodel.BookingContext.Add(viewmodel.NewBookingUpdate);
                    _viewmodel.SeriesBooking = LiveChartsService.TotalBookingsToDaySeries();
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

        public override void Execute(object? parameter)
        {
            var view = new ListOfContractsForBookingDialog
            {
                DataContext = new ListOfContractsForBookingViewModel(_viewmodel)
            };
            DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
        }
    }
}
