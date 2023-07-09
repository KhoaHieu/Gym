using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagement.Commands
{
    public class BookingNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private BookingViewModel _bookingViewModel { get; set; }

        public BookingNavigationCommand(NavigationStore navigationStore, BookingViewModel bookingviewmodel)
        {
            _navigationStore = navigationStore;
            _bookingViewModel = bookingviewmodel;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_navigationStore.CurrentViewModel.Equals(_bookingViewModel) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _bookingViewModel;
        }
    }
}
