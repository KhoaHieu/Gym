using GymManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class ListOfContractsForBookingViewModel : BaseViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }
        private GymManagementDbContext DbContext { get; set; }
        private ObservableCollection<Models.Contract> _ptContractList { get; set; }

        public ObservableCollection<Models.Contract> PtContractList
        {
            get => _ptContractList;
            set { _ptContractList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Models.Contract> _contractList { get; set; }

        public ObservableCollection<Models.Contract> ContractList
        {
            get => _contractList;
            set { _contractList = value; OnPropertyChanged(); }
        }
        private Models.Contract _selectedPtContract { get; set; }
        public Models.Contract SelectedPtContract
        {
            get => _selectedPtContract;
            set { _selectedPtContract = value; OnPropertyChanged(); }
        }

        private Task LoadDataAsync(Staff Pt)
        {
            
            return Task.Factory.StartNew(() =>
            {
                using (DbContext = new GymManagementDbContext())
                {
                    foreach(Models.Contract con in Pt.Contracts.Where(s => s.StaffId == Pt.StaffId).ToList())
                    {
                        var course = DbContext.Courses.Where(s => s.CourseId == con.CourseId).FirstOrDefault();
                        var customer = DbContext.Customers.Where(s => s.CustomerId == con.CustomerId).FirstOrDefault();
                        con.Course = course;
                        con.Customer = customer;
                        _ptContractList.Add(con);
                        Pt.Contracts.Add(con);
                    }
                }

            });
        }
        public void LoadData(Staff Pt)
        {
            IsLoading = true;
            _ = LoadDataAsync(Pt);
            
            IsLoading = false;
        }

        
       
        public void AddNewBooking()
        {
            using (DbContext = new GymManagementDbContext())
            {
                Models.Contract ptcontract = SelectedPtContract;
                if (ptcontract != null)
                {
                    var customer = DbContext.Customers.Where(s => s.CustomerId == ptcontract.CustomerId).FirstOrDefault();
                    var staff = DbContext.Staffs.Where(s => s.StaffId == ptcontract.StaffId).FirstOrDefault();
                    var contract = DbContext.Contracts.Where(s => s.ContractId == ptcontract.ContractId).FirstOrDefault();

                    if (contract.DaysLeft > 0)
                    {
                        contract.DaysLeft -= 1;
                        if (contract.DaysLeft == 0)
                        {
                            contract.Active = false;
                        }
                    }
                    Booking booking = new()
                    {
                        StaffId = ptcontract.StaffId,
                        CustomerId = ptcontract.CustomerId,
                        ContractId = ptcontract.ContractId,
                        CreateDate = DateTime.Now,
                    };

                    

                    DbContext.Add<Booking>(booking);
                    DbContext.SaveChanges();
                    booking.Contract = contract;
                    booking.Contract.DaysLeft = contract.DaysLeft;
                    Contractviewmodel.Update(contract);
                    BookingContext.Add(booking);
                    
                }
                else
                {
                    //Flag = false;
                }}
            }

        
        private ObservableCollection<Booking> BookingContext { get; set; }
        private ContractViewModel Contractviewmodel { get; set; }

        public ListOfContractsForBookingViewModel(BookingViewModel viewmodel)
        {
            Contractviewmodel = viewmodel.contractviewmodel;
            BookingContext = viewmodel.BookingContext;
            _ptContractList = new ObservableCollection<Models.Contract>();
            _contractList = viewmodel.contractviewmodel.ContractContext;
            foreach(var contract in _contractList) 
            {
                if(contract.StaffId == viewmodel.SelectedStaff.StaffId)
                {
                    _ptContractList.Add(contract);
                }
            }
            //LoadData(viewmodel.SelectedStaff);
        }
    }
}
