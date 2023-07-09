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
    public class AddCourseTypeCommand : BaseAsyncCommand
    {
        private static AddCourseTypeCommand _instance;
        private static readonly object _lockObject = new object();

        private CourseViewModel _viewmodel;

        private AddCourseTypeCommand()
        {
            _viewmodel = null;
        }

        private AddCourseTypeCommand(CourseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public static AddCourseTypeCommand Instance(CourseViewModel viewmodel)
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = new AddCourseTypeCommand(viewmodel);
                }
                return _instance;
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var view = new AddNewTypeOfCourseDialog()
            {
                DataContext = new AddNewCourseTypeViewModel(_viewmodel)
            };
            await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CourseViewModel.TypeList))
            {
                OnExecutedChanged();
            }
        }

        protected async Task ShowErrorDialog()
        {
            var view = new SampleErrorDialog();
            var result = await DialogHost.Show(view, "RootDialog");
        }

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
               parameter == false) return;

            var view = eventArgs.Session.Content as AddNewTypeOfCourseDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewCourseTypeViewModel;
                if (viewmodel != null)
                {
                    viewmodel.AddNewTypeOfCourse();

                    eventArgs.Cancel();

                    eventArgs.Session.UpdateContent(new SampleProgressDialog());

                    //Task.Delay(TimeSpan.FromSeconds(3))
                    //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    //        TaskScheduler.FromCurrentSynchronizationContext());
                }
            }

            eventArgs.Cancel();

            eventArgs.Session.UpdateContent(new SampleProgressDialog());

            //Task.Delay(TimeSpan.FromSeconds(3))
            //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
            //        TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
