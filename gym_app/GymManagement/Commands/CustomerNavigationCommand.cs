using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Stores;
using GymManagement.Utils;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class CustomerNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private CustomerViewModel _customerViewModel { get; set; }
        
        public NavigationStore navigationStore => _navigationStore;

        public CustomerNavigationCommand(NavigationStore navigationStore, CustomerViewModel customerviewmodel)
        {
            
            _navigationStore = navigationStore;
            _customerViewModel = customerviewmodel;
        }
        private Decentralization role = Decentralization.GetDecentralization();
        public override bool CanExecute(object? parameter)
        {
            //if (role.RoleId == 1)
            //{
            //    _customerViewModel.ShowMessageDialog();

                
            //}
            //else
            //{

            //    return false;
            //}
            return !navigationStore.CurrentViewModel.Equals(_customerViewModel) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            navigationStore.CurrentViewModel = _customerViewModel;
        }
    }
}
