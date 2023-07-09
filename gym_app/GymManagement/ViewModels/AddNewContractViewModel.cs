using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Services;
using GymManagement.Views;
using LiveChartsCore;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewContractViewModel : BaseViewModel
    {
        #region Database Field
        private GymManagementDbContext Context { get; set; }
        private ObservableCollection<Contract> ContractContext;
        private ObservableCollection<Customer> CustomerContext;
        private ObservableCollection<TypesOfCourse> TypeOfCourseContext;
        private ObservableCollection<Course> CourseContext;
        private ObservableCollection<Staff> PtContext;

        #endregion

        #region Data Prop
        private ISeries[] SeriesMoney { get; set; }
        private ISeries[] _series { get; set; }
        public ISeries[] SeriesContract
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(nameof(HomeViewModel.SeriesContract));
            }
        }
        private List<string> _genderList { get; set; }

        public List<string> GenderList 
        {
            get => _genderList;
            set 
            {
                _genderList = value; OnPropertyChanged();
            }
        }

        private List<string> _typeList { get; set; }

        public List<string> TypeList
        {
            get => _typeList;
            set { _typeList = value; OnPropertyChanged(); }
        }
        private List<string> _allcourseList { get; set; }

        public List<string> AllCourseList
        {
            get => _allcourseList;
            set { _allcourseList = value; OnPropertyChanged(); }
        }
        private List<string> _courseList { get; set; }

        public List<string> CourseList
        {
            get => _courseList;
            set
            {
                _courseList = value; OnPropertyChanged();
            }
        }
        private List<string> _ptList { get; set; }

        public List<string> PtList
        {
            get => _ptList;
            set { _ptList = value; OnPropertyChanged(); }
        }
        private string _selectedType { get; set; }
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                //OnCourseChange(value)
                _selectedType = value;
                List<string> list = new List<string>();
                foreach (Course course in CourseContext.Where(s => s.TypeNavigation.Name == _selectedType).ToList())
                {
                    list.Add(course.Name);
                }
                CourseList = list;
                OnPropertyChanged();
            }
        }
        private string _selectedCourse { get; set; }
        public string SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        private string _selectedPt { get; set; }
        public string SelectedPt
        {
            get => _selectedPt;
            set { _selectedPt = value; OnPropertyChanged(); }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }


        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _identitynumber;

        public string IdentityNumber
        {
            get { return _identitynumber; }
            set { _identitynumber = value; OnPropertyChanged(); }
        }

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; Debug.WriteLine(value); OnPropertyChanged(); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = true; OnPropertyChanged(); }
        }
        private DateTime _createDate { get; set; }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; OnPropertyChanged(); }
        }
        private DateTime _birthDay = DateTime.Today;

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; OnPropertyChanged(); }
        }

        private DateTime _finishDate { get; set; }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                _finishDate = value;
                OnPropertyChanged();
            }
        }


        private string _customername;

        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; OnPropertyChanged(); }
        }

        private string _coursename;

        public string CourseName
        {
            get { return _coursename; }
            set { _coursename = value; OnPropertyChanged(); }
        }
        private bool _isAvailableCustomer;
        public bool IsAvailableCustomer
        {
            get => _isAvailableCustomer;
            set
            {
                _isAvailableCustomer = value;
                OnPropertyChanged(); 
            }
        }
    
        #endregion

        private Customer CreateNewCustomer(GymManagementDbContext Context)
        {
            Customer newcustomer = new Customer();
            try
            {
                // Add new customer
                
                newcustomer.Name = CustomerName;
                newcustomer.IdentityNumber = IdentityNumber;
                newcustomer.Phone = Phone;
                newcustomer.Email = Email;
                newcustomer.Gender = Gender;
                newcustomer.Birthday = BirthDay;
                newcustomer.Address = Address;
                newcustomer.Active = true;

                Context.Add<Customer>(newcustomer);
                Context.SaveChanges();
                CustomerContext.Add(newcustomer);
            }
            catch (Exception)
            {
                DialogHost.Show(new SampleErrorDialog(), "RootDialog");
            }
            return newcustomer;
        }
        private async Task CreateContract(GymManagementDbContext Context, Customer customer, TypesOfCourse type, Course course, Staff pt)
        {
            // Add new contract
            CreateDate = DateTime.Now;
            int daysLeft;
            if (type.TypeId == 2)
            {
                daysLeft = (int)course.NumberOfSession;
            }
            else
            {
                daysLeft = (int)course.Duration;
            }
            int? staffId;
            if(pt == null)
            {
                 staffId = null;
            }
            else
            {
                 staffId = pt.StaffId;
            }
            Contract contract = new()
            {
                Description = Description,
                CustomerId = customer.CustomerId,
                CourseId = course.CourseId,
                StaffId = staffId,
                Customer = customer,
                Active = true,
                DaysLeft = daysLeft,
                // Auto generated Date

                CreateDate = CreateDate,
                FinishDate = CreateDate.AddDays((double)course.Duration)
            };

            Staff staff = Context.Staffs.Where(s => s.StaffId == staffId).FirstOrDefault();
            if(staff != null)
            {
                for (int i = 0; i < PtContext.Count; i++)
                {
                    if (PtContext[i].StaffId == staff.StaffId)
                        PtContext[i].Contracts.Add(contract);
                }
            }
            Context.Add<Contract>(contract);
            Context.SaveChanges();
            _viewmodel.SeriesContract = LiveChartsService.TotalContractInAWeekSeries();
            _viewmodel.SeriesTotalRevenue = LiveChartsService.TotalRevenueOneYearSeries();
            ContractContext.Add(contract);
            // Add a new contract to Customer Table
        }

        public void AddNewContract()
        {
            _ = AddNewContractAsync();
        }

        private bool _flag = true;
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }

        public Task AddNewContractAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    using (Context = new GymManagementDbContext())
                    {

                        Customer customer;
                        // validate existed customer
                        if (IsAvailableCustomer)
                        {
                            customer = Context.Customers.Where(s => s.Name == CustomerName
                            && s.Phone == Phone).FirstOrDefault();
                        }
                        else
                        {
                            customer = CreateNewCustomer(Context);
                        }

                        var course = Context.Courses.Where(s => s.Name == SelectedCourse).FirstOrDefault();
                        var type = Context.TypesOfCourses.Where(s => s.Name == SelectedType).FirstOrDefault();
                        var pt = Context.Staffs.Where(s => s.Name == SelectedPt).FirstOrDefault();
                        if (course != null && CustomerName != null && Phone != null)
                        {
                            CreateContract(Context, customer, type, course, pt);
                        }
                        else
                        {
                            Flag = false;
                        }
                    }
                }
                catch (Exception)
                {
                    Flag = false;
                }
            });
        }

        public ObservableCollection<Contract> Sync()
        {
            return ContractContext;
        }

        #region LoadData Method
        private async Task<List<string>> LoadCourseName()
        {
            List<string> courseList = new List<string>();
            foreach (Course cor in CourseContext)
            {
                if (cor != null)
                {
                    courseList.Add(cor.Name);
                }
            }
            return courseList;
        }
        private async Task<List<string>> LoadPt()
        {
            List<string> ptCourseList = new List<string>();
            foreach (Staff pt in PtContext)
            {
                if (pt != null)
                {
                    ptCourseList.Add(pt.Name);
                }
            }
            return ptCourseList;
        }

        private async Task<List<string>> LoadType()
        {
            List<string> TypeList = new List<string>();
            foreach (TypesOfCourse type in TypeOfCourseContext)
            {
                if (type != null)
                {
                    TypeList.Add(type.Name);
                }
            }
            return TypeList;

        }

        public async Task LoadData()
        {
            using (Context = new GymManagementDbContext())
            {
                Task<List<string>> getCoursename = LoadCourseName();
                Task<List<string>> getTypeOfCourse = LoadType();
                Task<List<string>> getPtname = LoadPt();
                AllCourseList = await getCoursename;
                TypeList = await getTypeOfCourse;
                PtList = await getPtname;
                CourseList = AllCourseList;
            }
        }
        #endregion

        private HomeViewModel _viewmodel;
        public AddNewContractViewModel(ContractViewModel viewmodel, BookingViewModel ptcontext)
        {
            GenderList = new()
            {
                "Nam",
                "Nữ", "Khác"
            };
            _viewmodel = viewmodel.homeviewmodel;
            _series = viewmodel.homeviewmodel.SeriesContract;
            SeriesMoney = viewmodel.homeviewmodel.SeriesTotalRevenue;
            CustomerContext = viewmodel.CustomerContext;
            ContractContext = viewmodel.ContractContext;
            TypeOfCourseContext = viewmodel.TypeOfCourseContext;
            CourseContext = viewmodel.CourseContext;
            PtContext = ptcontext.PtContext;
            _ = LoadData();
        }
    }
}
