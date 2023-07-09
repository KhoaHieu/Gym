using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    class ViewContractListOfCustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Contract> _contractList { get; set; }

        public ObservableCollection<Models.Contract> ContractList
        {
            get => _contractList;
            set { _contractList = value; OnPropertyChanged(); }
        }
        private CustomerViewModel _viewmodel { get; set; }
        private void LoadData()
        {
            foreach(var contract in _viewmodel.ContractContext)
            {
                if(contract.CustomerId == _viewmodel.SelectedCustomer.CustomerId)
                {
                    _contractList.Add(contract);
                }
            }
        }
        public ViewContractListOfCustomerViewModel(CustomerViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            _contractList = new ObservableCollection<Contract>();
            LoadData();
        }
    }
}
