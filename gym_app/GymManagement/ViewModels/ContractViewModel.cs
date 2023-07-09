using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
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
    public class ContractViewModel : BaseViewModel
    {

        #region Data Field


        //private int _daysLeft { get; set; }
        //public int DaysLeft
        //{
        //    get => _daysLeft;
        //    set
        //    {
        //        _daysLeft = value;
        //        OnPropertyChanged(nameof(DaysLeft));
        //    }
        //}
        public HomeViewModel homeviewmodel;
        private Contract _selectedContract { get; set; }
        public Contract SelectedContract
        {
            get => _selectedContract;
            set { _selectedContract = value; OnPropertyChanged(); }
        }
        private GymManagementDbContext Context { get; set; }
        private ObservableCollection<Models.Contract> _contractContext { get; set; }

        public ObservableCollection<Models.Contract> ContractContext
        {
            get => _contractContext;
            set { _contractContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Models.Contract> _contractContextClone { get; set; }

        public ObservableCollection<Models.Contract> ContractContextClone
        {
            get => _contractContextClone;
            set { _contractContextClone = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Customer> _customerContext { get; set; }

        public ObservableCollection<Customer> CustomerContext
        {
            get => _customerContext;
            set { _customerContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TypesOfCourse> _typeOfCourseContext { get; set; }

        public ObservableCollection<TypesOfCourse> TypeOfCourseContext
        {
            get => _typeOfCourseContext;
            set { _typeOfCourseContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Staff> _ptContext { get; set; }

        public ObservableCollection<Staff> PtContext
        {
            get
            {
                ObservableCollection<Staff> PtContext = new ObservableCollection<Staff>();
                foreach (Staff staff in _ptContext)
                {
                    if (staff.RoleId == 2)
                    {
                        PtContext.Add(staff);
                    }
                }
                return PtContext;
            }
            set { _ptContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Course> _courseContext { get; set; }

        public ObservableCollection<Course> CourseContext
        {
            get => _courseContext;
            set { _courseContext = value; OnPropertyChanged(); }
        } 
        #endregion
        public override async Task AddNewData()
        {
            var view = new AddNewContractDialog
            {
                DataContext = new AddNewContractViewModel(this, BookingViewModel)
            };
            var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandler, null);
            
        }
        private async Task Add(Contract replace)
        {
            using (Context = new GymManagementDbContext())
            {
                Context.Add<Contract>(replace);
                Context.SaveChanges();
                _contractContext.Add(replace);
            }
        }

        protected async Task ShowErrorDialog()
        {
            var view = new SampleErrorDialog();
            var result = await DialogHost.Show(view, "RootDialog");
        }
        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddNewContractDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewContractViewModel;
                if (viewmodel != null)
                {

                    await viewmodel.AddNewContractAsync();
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    eventArgs.Cancel();


                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _contractContext = viewmodel.Sync();
                    }

                    //lets run a fake operation for 3 seconds then close this baby.
                    //Task.Delay(TimeSpan.FromSeconds(3))
                    //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    //        TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }
        
        public async Task Update( Contract replace)
        {
            try
            {
                for (int i = 0; i < _contractContext.Count; i++)
                {
                    if (_contractContext[i].ContractId == replace.ContractId)
                        _contractContext[i].DaysLeft -= 1;
                }

                    
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        private async Task Delete(Contract delete)
        {
            using (Context = new GymManagementDbContext())
            {
                Context.Remove<Contract>(delete);
                Context.SaveChanges();
                _contractContext.Remove(delete);
            }
        }

        public BookingViewModel BookingViewModel;
        public ICommand ViewContractDetailCommand { get; set; }
        public ContractViewModel()
        {
            ViewContractDetailCommand = new ViewContractDetailCommand(this);
            
        }
    }
}
