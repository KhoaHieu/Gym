using GymManagement.Dialogs;
using GymManagement.Models;
using GymManagement.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewStaffViewModel : BaseViewModel
    {
        private GymManagementDbContext Context { get; set; }
        #region Data Fields
        private List<string> _roleList { get; set; }

        public List<string> RoleList
        {
            get => _roleList;
            set { _roleList = value; OnPropertyChanged(); }
        }
        private string _selectedRole { get; set; }
        public string SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        private DateTime _birthDay = DateTime.Today;

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; OnPropertyChanged(); }
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

        private string _staffname;

        public string StaffName
        {
            get { return _staffname; }
            set { _staffname = value; OnPropertyChanged(); }
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
            set { _gender = value; OnPropertyChanged(); }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = true; OnPropertyChanged(); }
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
        #endregion



        public void AddNewStaff()
        {
            _ = AddNewStaffAsync();
        }

        public Task AddNewStaffAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Decentralization role = Decentralization.GetDecentralization();
                if (role.RoleId != 1)
                {
                    var view = new SampleMessageDialog()
                    {
                        DataContext = new SampleMessageViewModel("Bạn không được cấp quyền để chỉnh sửa thông tin!")
                    };
                    DialogHost.Show(view, "RootDialog");

                }

                else using (Context = new GymManagementDbContext())
                    {

                        try
                        {
                            var Role = Context.Roles.Where(s => s.Name == SelectedRole).FirstOrDefault();

                            if (Role != null)
                            {
                                // This will validate whether a Staff have exists ?




                                //// Add new staff
                                Staff staff = new()
                                {
                                    Name = StaffName,
                                    Role = Role,
                                    RoleId = Role.RoleId,
                                    IdentityNumber = IdentityNumber,
                                    Phone = Phone,
                                    Email = Email,
                                    Gender = Gender,
                                    Birthday = BirthDay,
                                    Address = Address,
                                    Active = true,
                                };


                                Context.Add<Staff>(staff);
                                Context.SaveChanges();
                                StaffContext.Add(staff);
                            }
                            else
                            {
                                Flag = false;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
            });
        }

        private Task LoadDataAsync(List<string> roleList)
        {
            return Task.Factory.StartNew(() =>
            {
                _roleList = roleList;
            });
        }

        public async Task LoadData(List<string> roleList)
        {
            _ = LoadDataAsync(roleList);
        }

        public ObservableCollection<Staff> Sync()
        {
            return StaffContext;
        }

        private ObservableCollection<Staff> StaffContext;
        public AddNewStaffViewModel(ObservableCollection<Staff> _staffContext, List<string> roleList)
        {
            GenderList = new()
            {
                "Nam",
                "Nữ", "Khác"
            };
            StaffContext = _staffContext;
            LoadData(roleList);
        }
    }
}
