using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class StaffNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private StaffViewModel _staffViewModel { get; set; }

        public NavigationStore navigationStore => _navigationStore;

        public StaffNavigationCommand(NavigationStore navigationStore, StaffViewModel staffviewmodel)
        {
            _navigationStore = navigationStore;
            _staffViewModel = staffviewmodel;
        }

        public override bool CanExecute(object? parameter)
        {
            return !navigationStore.CurrentViewModel.Equals(_staffViewModel) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            navigationStore.CurrentViewModel = _staffViewModel;
        }
    }
}
