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
    class ViewContractListOfCustomerCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            Customer customer = _viewmodel.SelectedCustomer;
            var view = new ViewContractListOfCustomerDialog
            {
                DataContext = new ViewContractListOfCustomerViewModel(_viewmodel)
            };
            DialogHost.Show(view, "RootDialog");
            
        }
        
        private CustomerViewModel _viewmodel { get; set; }
        public ViewContractListOfCustomerCommand(CustomerViewModel viewmodel)
        {
            _viewmodel= viewmodel;
        }
    }
}
