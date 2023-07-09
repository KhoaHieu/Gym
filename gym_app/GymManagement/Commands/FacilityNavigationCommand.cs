using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class FacilityNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private  FacilityViewModel _facilityViewModel { get; set; }
        public FacilityNavigationCommand(NavigationStore navigationStore, FacilityViewModel facilityViewModel)
        {
            _navigationStore = navigationStore;
            _facilityViewModel = facilityViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return !_navigationStore.CurrentViewModel.Equals(_facilityViewModel) && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _facilityViewModel;
        }
    }
}
