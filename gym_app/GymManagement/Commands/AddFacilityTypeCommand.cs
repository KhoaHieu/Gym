using GymManagement.Dialogs;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class AddFacilityTypeCommand : BaseAsyncCommand
    {
        private FacilityViewModel _viewmodel;
        public override async Task ExecuteAsync(object parameter)
        {
            var view = new AddNewTypeOfFacilityDialog()
            {
                DataContext = new AddNewFacilityTypeViewModel(_viewmodel)
            };
            await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
        }
        public AddFacilityTypeCommand(FacilityViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseViewModel.TypeList))
            {
                OnExecutedChanged();
            }
        }
        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
               parameter == false) return;



            var view = eventArgs.Session.Content as AddNewTypeOfFacilityDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewFacilityTypeViewModel;
                if (viewmodel != null)
                {
                    viewmodel.AddNewTypeOfFacility();
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

