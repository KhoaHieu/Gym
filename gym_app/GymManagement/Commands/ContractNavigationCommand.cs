using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class ContractNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private ContractViewModel _contractViewModel { get; set; }


        public ContractNavigationCommand(NavigationStore navigationStore, ContractViewModel contractviewmodel)
        {
            _navigationStore = navigationStore;
            _contractViewModel = contractviewmodel;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_navigationStore.CurrentViewModel.Equals(_contractViewModel) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _contractViewModel;
        }
    }
}
