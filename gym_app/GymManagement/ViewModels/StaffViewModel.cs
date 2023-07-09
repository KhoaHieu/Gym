using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Utils;
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
    public class StaffViewModel : BaseViewModel
    {
        

        #region DataFields
        private GymManagementDbContext DbContext { get; set; }
        private ObservableCollection<Staff> _staffContext { get; set; }

        public ObservableCollection<Staff> StaffContext
        {
            get => _staffContext;
            set { _staffContext = value; OnPropertyChanged(); }
        }

        private Staff _selectedStaff { get; set; }
        public Staff SelectedStaff
        {
            get => _selectedStaff;
            set
            {
                _selectedStaff = value;
                base.SelectedItem = _selectedStaff;
                OnPropertyChanged();
            }
        }
        #endregion

        protected async Task ShowErrorDialog()
        {
            var view = new SampleErrorDialog();
            var result = await DialogHost.Show(view, "RootDialog");
        }
        private async void ExtendedClosingEventHandlerForAddNewStaff(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddNewStaffDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewStaffViewModel;
                if (viewmodel != null)
                {
                    try
                    {
                        await viewmodel.AddNewStaffAsync();
                    }
                    catch(Exception ex) { viewmodel.Flag = false; }
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    //OK, lets cancel the close...
                    eventArgs.Cancel();


                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _staffContext = viewmodel.Sync();
                    }


                    //lets run a fake operation for 3 seconds then close this baby.
                    //Task.Delay(TimeSpan.FromSeconds(3))
                    //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    //        TaskScheduler.FromCurrentSynchronizationContext());

                }

            }
        }

        public override async Task DeleteData(object model)
        {
            Debug.WriteLine("We got here to delete!");
            //using (DbContext = new GymManagementDbContext())
            //{
            //    DbContext.Remove<Staff>(model as Staff);
            //    DbContext.SaveChanges();
            //    _staffContext.Remove(model as Staff);
            //}
        }

        public override async Task AddNewData()
        {
            using (DbContext = new GymManagementDbContext())
            {
                List<string> RoleList = new List<string>();
                foreach (Role role in DbContext.Roles.ToList())
                {
                    if (role != null)
                    {
                        RoleList.Add(role.Name);
                    }
                }
                var view = new AddNewStaffDialog
                {
                    DataContext = new AddNewStaffViewModel(_staffContext, RoleList)
                };
                var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandlerForAddNewStaff, null);
            }
        }
        public override async Task UpdateData()
        {
            try
            {
                using (DbContext = new GymManagementDbContext())
                {
                    Staff replace = SelectedStaff;
                    
                    Staff current = DbContext.Staffs.Where(t => t.StaffId.Equals(SelectedStaff.StaffId)).FirstOrDefault();
                    if (current != null)
                    {
                        if (!(current.Name == replace.Name &&
                            current.IdentityNumber == replace.IdentityNumber &&
                            current.Gender == replace.Gender &&
                            current.Phone == replace.Phone &&
                            current.RoleId == replace.RoleId &&
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
                            current.RoleId = replace.RoleId;
                            current.Birthday = replace.Birthday;
                            current.Address = replace.Address;
                            current.Active = replace.Active;
                            DbContext.SaveChanges();
                            //_customerContext.Remove(current);
                            //_customerContext.Add(replace);
                            for (int i = 0; i < _staffContext.Count; i++)
                            {
                                if (_staffContext[i].StaffId == replace.StaffId)
                                    _staffContext[i] = replace;
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
        private ObservableCollection<Staff> _staffContextClone;
        public ObservableCollection<Staff> StaffContextClone
        {
            get => _staffContextClone;
            set
            {
                _staffContextClone = value;
                OnPropertyChanged();
            }
        }
        public override void SearchData(string Content)
        {
            //ObservableCollection<Customer> tempContext = (ObservableCollection<Type>)CustomerContext;
            //_customerContextClone = _customerContext;
            StaffContext = this._staffContextClone;
            SearchHelper<Staff> searchHelper = new SearchHelper<Staff>(StaffContext);
            ObservableCollection<Staff> searchResult = searchHelper.Result(Content);
            //searchHelper.Result(Content, "Customer");
            if (searchResult != null)
            {
                StaffContext = searchResult;
            }
            else
            {
                StaffContext = _staffContextClone;
            }
        }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }
        public StaffViewModel()
        {
            GridChangeCommand = new GridChangeCommand(this);
            DeleteDataCommand = new DeleteDataCommand(this);
        }

    }
}

