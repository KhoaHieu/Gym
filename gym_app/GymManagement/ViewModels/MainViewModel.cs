using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Services;
using GymManagement.Stores;
using GymManagement.Utils;
using HarfBuzzSharp;
using LiveChartsCore.SkiaSharpView;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Input;

namespace GymManagement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region commands
        public ICommand HomeNavigationCommand { get; set; }
        public ICommand CustomerNavigationCommand { get; set; }
        public ICommand StaffNavigationCommand { get; set; }
        public ICommand FacilityNavigationCommand { get; set; }
        public ICommand ContractNavigationCommand { get; set; }
        public ICommand CourseNavigationCommand { get; set; }
        public ICommand BookingNavigationCommand { get; set; }
        public ICommand BacktoLoginCommand { get; set; }
        public ICommand NotSupportedCommand { get; set; }

        #endregion
        private readonly NavigationStore _navigationStore;
        
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;


        #region datafield
        private GymManagementDbContext _dbcontext;
        
        private HomeViewModel homeViewModel { get; set; }
        private CustomerViewModel customerViewModel { get; set; }
        private FacilityViewModel facilityViewModel { get; set; }
        private CourseViewModel courseViewModel { get; set; }
        private ContractViewModel contractViewModel { get; set; }
        private StaffViewModel staffViewModel { get; set; }
        private BookingViewModel bookingViewModel { get; set; }
        #endregion
        

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        #region Synchronized Data

        private object _lockMutex = new object();
        private ObservableCollection<Models.Contract> ContractContext { get; set; }
        private ObservableCollection<Models.Contract> ContractContextClone { get; set; }
        private ObservableCollection<Customer> CustomerContext { get; set; }
        private ObservableCollection<Customer> CustomerContextClone { get; set; }
        private ObservableCollection<Booking> BookingContext { get; set; }
        private ObservableCollection<Course> CourseContext { get; set; }
        private ObservableCollection<Course> CourseContextClone { get; set; }
        private ObservableCollection<Staff> StaffContext { get; set; }
        private ObservableCollection<Staff> StaffContextClone { get; set; }
        private ObservableCollection<Facility> FacilityContext { get; set; }
        private ObservableCollection<Facility> FacilityContextClone { get; set; }
        private ObservableCollection<TypesOfCourse> TypeOfCourseContext { get; set; }
        private ObservableCollection<TypesOfFacility> TypeOfFacilityContext { get; set; }
        #endregion



        private string _userEmail;
        private int _roleId;
        public MainViewModel(string UserEmail, NavigationStore navigationStore)
        {
            
            _userEmail = UserEmail;

            InitializeData();
            
            #region BindingOperations
            BindingOperations.EnableCollectionSynchronization(CustomerContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(BookingContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(ContractContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(CourseContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(StaffContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(FacilityContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(TypeOfCourseContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(TypeOfFacilityContext, _lockMutex);

            #endregion

            LoadData();
            

            _navigationStore = navigationStore;
            
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _navigationStore.CurrentViewModel = homeViewModel;



            #region CommandNavigation
            HomeNavigationCommand = new HomeNavigationCommand(_navigationStore, homeViewModel);
            CustomerNavigationCommand = new CustomerNavigationCommand(_navigationStore, customerViewModel);
            StaffNavigationCommand = new StaffNavigationCommand(_navigationStore, staffViewModel);
            FacilityNavigationCommand = new FacilityNavigationCommand(_navigationStore, facilityViewModel);
            ContractNavigationCommand = new ContractNavigationCommand(_navigationStore, contractViewModel);
            CourseNavigationCommand = new CourseNavigationCommand(_navigationStore, courseViewModel);
            BookingNavigationCommand = new BookingNavigationCommand(_navigationStore, bookingViewModel);
            BacktoLoginCommand = new BacktoLoginCommand(_navigationStore);
            NotSupportedCommand = new NotSupportedCommand();
            #endregion
        }


        private void InitializeData()
        {
            ContractContext = new ObservableCollection<Models.Contract>();
            ContractContextClone = new ObservableCollection<Models.Contract>();
            BookingContext = new ObservableCollection<Booking>();
            CustomerContext = new ObservableCollection<Customer>();
            CustomerContextClone = new ObservableCollection<Customer>();
            CourseContext = new ObservableCollection<Course>();
            CourseContextClone = new ObservableCollection<Course>();
            StaffContext = new ObservableCollection<Staff>();
            StaffContextClone = new ObservableCollection<Staff>();
            FacilityContext = new ObservableCollection<Facility>();
            FacilityContextClone = new ObservableCollection<Facility>();
            TypeOfCourseContext = new ObservableCollection<TypesOfCourse>();
            TypeOfFacilityContext = new ObservableCollection<TypesOfFacility>();
            homeViewModel = new HomeViewModel()
            {
                //SeriesTotalRevenue = LiveChartsService.TotalRevenueOneYearSeries(),
                //SeriesContract = LiveChartsService.TotalContractInAWeekSeries()
            };
            customerViewModel = new CustomerViewModel
            {
                ContractContext = ContractContext,
                CustomerContext = CustomerContext,
                CustomerContextClone = CustomerContextClone
            };
            facilityViewModel = new FacilityViewModel
            {
                TypeList = TypeOfFacilityContext,
                FacilityContext =  FacilityContext,
                FacilityContextClone = FacilityContextClone
            };
            staffViewModel = new StaffViewModel
            {
                StaffContext =  StaffContext,
                StaffContextClone = StaffContextClone
            };
            courseViewModel = new CourseViewModel
            {
                TypeList = TypeOfCourseContext,
                CourseContext = CourseContext,
                CourseContextClone = CourseContextClone
            };
            bookingViewModel = new BookingViewModel
            {
                staffViewModel = staffViewModel,
                contractviewmodel = contractViewModel,
                BookingContext = BookingContext,
                PtContext = StaffContext,
            };
            contractViewModel = new ContractViewModel
            {
                homeviewmodel = homeViewModel,
                CustomerContext = CustomerContext,
                BookingViewModel = bookingViewModel,
                CourseContext = CourseContext,
                TypeOfCourseContext = TypeOfCourseContext,
                PtContext = StaffContext,
                ContractContext = ContractContext,
                ContractContextClone = ContractContextClone
            };
        }


        #region Load Data For Pages

        
        private async Task LoadDataForHomePageAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                homeViewModel.InitializeChart();
                
            });
                
        }

        private async Task LoadDataForFaciityPageAsync(GymManagementDbContext _dbcontext)
        {
            foreach (Facility fac in _dbcontext.Facilities.ToList())
            {
                TypesOfFacility type = _dbcontext.TypesOfFacilities.Where(s => s.TypeId == fac.TypeId).FirstOrDefault();
                fac.Type = type;
                
                facilityViewModel.FacilityContext.Add(fac);
                facilityViewModel.FacilityContextClone.Add(fac);
            }
            foreach(TypesOfFacility type in _dbcontext.TypesOfFacilities.ToList())
            {
                facilityViewModel.TypeList.Add(type);
            }

        }
        
        private async Task LoadDataForBookingPageAsync(GymManagementDbContext _dbcontext)
        {
            
            bookingViewModel.InitializeChart();
            
            bookingViewModel.contractviewmodel = contractViewModel;
            foreach (Booking booking in _dbcontext.Bookings.Where(s => s.CreateDate.Value.Equals(DateTime.Today)).ToList())
            {
                Customer customer = _dbcontext.Customers.Where(s => s.CustomerId == booking.CustomerId).FirstOrDefault();
                Staff staff = _dbcontext.Staffs.Where(s => s.StaffId == booking.StaffId).FirstOrDefault();
                Models.Contract contract = _dbcontext.Contracts.Where(s => s.ContractId == booking.ContractId).FirstOrDefault();
                booking.Customer = customer;
                booking.Staff = staff;
                booking.Contract = contract;
                bookingViewModel.BookingContext.Add(booking);
            }
            
            //foreach(Staff pt in _dbcontext.Staffs.Where(s => s.Role.RoleId == 2)
            //    .Include(s => s.Bookings).ToList())
            //{
            //    bookingViewModel.PtContext.Add(pt);
            //}
            
        }
        
        private async Task LoadDataForContractPageAsync(GymManagementDbContext _dbcontext)
        {
            foreach (Models.Contract con in _dbcontext.Contracts.ToList())
            {
                Customer customer = _dbcontext.Customers.Where(s => s.CustomerId == con.CustomerId).FirstOrDefault();
                Course course = _dbcontext.Courses.Where(s => s.CourseId == con.CourseId).FirstOrDefault();
                con.Customer = customer;
                con.Course = course;
               
                if(con.Course.Type == 1)
                {
                    DateTime finishDate = (DateTime) con.FinishDate;
                    DateTime now = DateTime.Now;
                    TimeSpan daysLeft = finishDate - now;
                    con.DaysLeft = (Int32) daysLeft.TotalDays + 1;
                }
                if (con.DaysLeft <= 0)
                {
                    con.Active = false;
                }
                else
                {
                    con.Active = true;
                }
                contractViewModel.ContractContext.Add(con);
                contractViewModel.ContractContextClone.Add(con);
            }
            
        }

        private async Task LoadDataForStaffPageAsync(GymManagementDbContext _dbcontext)
        {
            
            foreach (Staff staff in _dbcontext.Staffs.Include(s => s.Bookings).ToList())
            {
                
                Role role = _dbcontext.Roles.Where(s => s.RoleId == staff.RoleId).FirstOrDefault();
                staff.Role = role;
                staffViewModel.StaffContext.Add(staff);

                staffViewModel.StaffContextClone.Add(staff);
            }
        }

        private async Task LoadDataForCoursePageAsync(GymManagementDbContext _dbcontext)
        {
            
            int pricemax = 0;
            int numofcontracts = 0;
            foreach (TypesOfCourse type in _dbcontext.TypesOfCourses.ToList())
            {
                courseViewModel.TypeList.Add(type);
            }
            foreach (Course cou in _dbcontext.Courses.Include(s => s.Contracts).ToList())
            {
                
                TypesOfCourse type = _dbcontext.TypesOfCourses.Where(s => s.TypeId == cou.Type).FirstOrDefault();
                
                cou.TypeNavigation.Name = type.Name;
                
                if (numofcontracts < cou.Contracts.Count)
                {
                    numofcontracts = cou.Contracts.Count;
                    courseViewModel.MostFavoriteCourse = cou;
                }

                if (pricemax < cou.Price)
                {
                    pricemax = (int)cou.Price;
                    courseViewModel.MostValueableCourse = cou;
                }
                courseViewModel.CourseContext.Add(cou);
                courseViewModel.CourseContextClone.Add(cou);

            }
            //courseViewModel.SeriesMostValueable = LiveChartsService.SeriesMostValueable;
            courseViewModel.SeriesMostValueable = new GaugeBuilder().WithMaxColumnWidth(5).AddValue(pricemax).BuildSeries();
            courseViewModel.SeriesMostFavorite = new GaugeBuilder().WithMaxColumnWidth(5).AddValue(numofcontracts).BuildSeries();
        }


        private async Task LoadDataForCustomerPageAsync(GymManagementDbContext _dbcontext)
        {
            //foreach (Customer cus in _dbcontext.Customers.Include(s => s.Contracts)
            //.Include(s => s.Bookings).ToList())
            //{
            //    customerViewModel.CustomerContext.Add(cus);
            //}

            int MostNumberOfTotalcontracts = 0;
            int MostNumberOfPtcontracts = 0;

            int MostMoneySpend = 0;
            
            
            // List of course that is A Personal Trainer course
            List<Course> Ptcourses = _dbcontext.Courses.Where(s => s.Type == 2).ToList();
            List<int> ptcourseID = new List<int>();
            foreach(Course cou in Ptcourses)
            {
                ptcourseID.Add(cou.CourseId);
            }
            foreach (Customer cus in _dbcontext.Customers.Include(s => s.Contracts).Include(s => s.Bookings).ToList())
            {

                int TotalMoneySpend = 0;
                int NumberOfPtcontract = 0;

                int counting = 0;
                // Total Money spend of each customer
                foreach (Models.Contract con in cus.Contracts)
                {
                    var course = _dbcontext.Courses.Where(s => s.CourseId == con.CourseId).FirstOrDefault();
                    TotalMoneySpend += (int)course.Price;

                    if (ptcourseID.Contains((int) con.CourseId) == true)
                    {
                        counting += 1;
                    }
                }

                

                // Define customer with most money spend
                if (MostMoneySpend < TotalMoneySpend)
                {
                    MostMoneySpend = TotalMoneySpend;
                    customerViewModel.MostMoneyCustomer = cus;
                }

                int numberoftotalcontractcounting = cus.Contracts.Count();
                // Define customer with the most number of total contracts
                if (MostNumberOfTotalcontracts < numberoftotalcontractcounting)
                {
                    MostNumberOfTotalcontracts = numberoftotalcontractcounting;
                    customerViewModel.MostContractCustomer = cus;
                }

                
                
                
                int numberofptcontractscounting = counting;
                // Define customer with the most number of PT contracts
                if (MostNumberOfPtcontracts < numberofptcontractscounting)
                {
                    MostNumberOfPtcontracts = numberofptcontractscounting;
                    customerViewModel.MostPtContractCustomer = cus;
                }
                customerViewModel.CustomerContext.Add(cus);
                customerViewModel.CustomerContextClone.Add(cus);
            }

            customerViewModel.TotalMoney = MostMoneySpend;
            customerViewModel.SeriesContract = new GaugeBuilder().WithMaxColumnWidth(5).AddValue(MostNumberOfTotalcontracts).BuildSeries();
            customerViewModel.SeriesPtContract = new GaugeBuilder().WithMaxColumnWidth(5).AddValue(MostNumberOfPtcontracts).BuildSeries();
            customerViewModel.SeriesMostMoney = new GaugeBuilder().WithMaxColumnWidth(5).AddValue(MostMoneySpend).BuildSeries();


        }
        #endregion


        private async Task SetRole(GymManagementDbContext _dbcontext)
        {

            try
            {
               
                    //Account account = _dbcontext.Accounts.Where(s => s.AccountName == UserEmail).FirstOrDefault();
                    //Staff staff = _dbcontext.Staffs.Where(s => s.StaffId == account.StaffId).FirstOrDefault();

                    Staff staff = _dbcontext.Staffs.Where(s => s.Email == _userEmail).FirstOrDefault();

                    _roleId =  (int)staff.RoleId;
                Decentralization Role = Decentralization.GetDecentralization();
                Role.RoleId = _roleId;  
                
            }
            catch (Exception)
            {

                var view = new SampleErrorDialog();
                DialogHost.Show(view, "RootDialog");
            }
        }


        public async Task LoadData()
        {
            
            //if(IsLoading == false && count>0)
            //{
            //    using (_dbcontext = new GymManagementDbContext())
            //    {
            //        await Task.Factory.StartNew(() =>
            //        {
            //            SetRole(_dbcontext);
            //            LoadDataForContractPageAsync(_dbcontext);
            //        });
            //    }
            //}
            //else
            //{
                IsLoading = true;
                using (_dbcontext = new GymManagementDbContext())
                {
                    await Task.Factory.StartNew(() =>
                    {
                        SetRole(_dbcontext);
                        LoadDataForHomePageAsync();
                        LoadDataForFaciityPageAsync(_dbcontext);
                        LoadDataForCustomerPageAsync(_dbcontext);
                        LoadDataForCoursePageAsync(_dbcontext);
                        LoadDataForContractPageAsync(_dbcontext);
                        LoadDataForStaffPageAsync(_dbcontext);
                        LoadDataForBookingPageAsync(_dbcontext);
                    });

                    //await Task.WhenAll(tasks);
                }
                IsLoading = false;
            
            
        }

        

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
