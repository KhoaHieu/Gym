using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class HomeNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private HomeViewModel _homeViewModel { get; set; }
        public NavigationStore navigationStore => _navigationStore;

        public HomeNavigationCommand(NavigationStore navigationStore, HomeViewModel homeViewModel)
        {
            _navigationStore = navigationStore;
            _homeViewModel = homeViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnExecutedChanged();
        }
        public override bool CanExecute(object? parameter)
        {
            return  base.CanExecute(parameter);
        }
        //!navigationStore.CurrentViewModel.Equals(_homeViewModel)
        public override void Execute(object? parameter)
        {
            navigationStore.CurrentViewModel = _homeViewModel;
        }
    }
}
