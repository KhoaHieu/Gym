using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
using LiveChartsCore;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagement.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        #region DataFields
        private GymManagementDbContext DbContext { get; set; }

        public int roleId;
        private ObservableCollection<Customer> _customerContext { get; set; }

        public ObservableCollection<Customer> CustomerContext
        {
            get => _customerContext;
            set { _customerContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Contract> _contractContext { get; set; }

        public ObservableCollection<Contract> ContractContext
        {
            get => _contractContext;
            set { _contractContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Customer> _customerContextClone { get; set; }

        public ObservableCollection<Customer> CustomerContextClone
        {
            get => _customerContextClone;
            set { _customerContextClone = value; OnPropertyChanged(); }
        }

        private Customer _selectedCustomer { get; set; }
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set 
            { 
                _selectedCustomer = value; 
                base.SelectedItem= _selectedCustomer; 
                OnPropertyChanged(); 
            }
        }

        #region ChartProperties
        private IEnumerable<ISeries> _seriesContract { get; set; }
        public IEnumerable<ISeries> SeriesContract
        {
            get { return _seriesContract; }
            set
            {
                _seriesContract = value; OnPropertyChanged();
            }
        }
        private Customer _mostContractCustomer { get; set; }
        public Customer MostContractCustomer
        {
            get { return _mostContractCustomer; }

            set
            {
                _mostContractCustomer = value; OnPropertyChanged();
            }
        }

        private IEnumerable<ISeries> _seriesPtContract { get; set; }
        public IEnumerable<ISeries> SeriesPtContract
        {
            get { return _seriesPtContract; }
            set
            {
                _seriesPtContract = value; OnPropertyChanged();
            }
        }
        private Customer _mostPtContractCustomer { get; set; }
        public Customer MostPtContractCustomer
        {
            get { return _mostPtContractCustomer; }

            set
            {
                _mostPtContractCustomer = value; OnPropertyChanged();
            }
        }
        private IEnumerable<ISeries> _seriesMostMoney { get; set; }
        public IEnumerable<ISeries> SeriesMostMoney
        {
            get { return _seriesMostMoney; }
            set
            {
                _seriesMostMoney = value; OnPropertyChanged();
            }
        }
        private Customer _mostMoneyCustomer { get; set; }
        public Customer MostMoneyCustomer
        {
            get { return _mostMoneyCustomer; }

            set
            {
                _mostMoneyCustomer = value; OnPropertyChanged();
            }
        }
        private int _totalMoney { get; set; }
        public int TotalMoney
        {
            get { return _totalMoney; }
            set { _totalMoney = value; OnPropertyChanged(); }
        }
        #endregion


        #endregion

        #region CRUD methods

        

        public override async Task UpdateData()
        {
            try
            {
                using (DbContext = new GymManagementDbContext())
                {
                    Customer replace = SelectedCustomer;
                    Debug.WriteLine("Name: " + SelectedCustomer.CustomerId);
                    Customer current = DbContext.Customers.Where(t => t.CustomerId.Equals(SelectedCustomer.CustomerId)).FirstOrDefault();
                    if (current != null)
                    {
                        if (!(current.Name == replace.Name &&
                            current.IdentityNumber == replace.IdentityNumber &&
                            current.Gender == replace.Gender &&
                            current.Phone == replace.Phone &&
                            current.Email == replace.Email &&
                            current.Birthday == replace.Birthday &&
                            current.Address == replace.Address &&
                            current.Active == replace.Active))
                        {
                            current.Name = replace.Name;
                            current.IdentityNumber = replace.IdentityNumber;
                            current.Gender = replace.Gender;
                            current.Phone = replace.Phone;
                            current.Email = replace.Email;
                            current.Birthday = replace.Birthday;
                            current.Address = replace.Address;
                            current.Active = replace.Active;
                            DbContext.SaveChanges();
                            //_customerContext.Remove(current);
                            //_customerContext.Add(replace);
                            for (int i = 0; i < _customerContext.Count; i++)
                            {
                                if (_customerContext[i].CustomerId == replace.CustomerId)
                                    _customerContext[i] = replace;
                            }
                            //_customerContext.Clear();
                            //await LoadData(Flag);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        } 
        #endregion

        public ICommand GridChangeCommand { get; set; }
        public ICommand ViewContractListOfCustomerCommand { get; set; }
        public CustomerViewModel() 
        {
            GridChangeCommand = new GridChangeCommand(this);
            ViewContractListOfCustomerCommand = new ViewContractListOfCustomerCommand(this);
        }
        public override void SearchData(string content)
        {
            Debug.WriteLine(content);
        }
        public void ShowMessageDialog()
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageViewModel("Bạn không được cấp quyền sử dụng chức năng này!")
            };
            DialogHost.Show(view, "RootDialog");
        }
    }
}
