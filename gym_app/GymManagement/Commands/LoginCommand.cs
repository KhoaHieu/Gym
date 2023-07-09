using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GymManagement.ViewModels;
using System.Diagnostics;
using GymManagement.Stores;
using GymManagement.PasswordSecure;
using System.Runtime.InteropServices;
using System.Security;
using GymManagement.Utils;
using GymManagement.Dialogs;
using MaterialDesignThemes.Wpf;
using GymManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Commands
{
    public class LoginCommand : BaseAsyncCommand
    {
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginViewModel;

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        private GymManagementDbContext _dbcontext;
        private async Task<int> SetRole(string UserEmail)
        {

            try
            {
                using (_dbcontext = new GymManagementDbContext())
                {
                    //Account account = _dbcontext.Accounts.Where(s => s.AccountName == UserEmail).FirstOrDefault();
                    //Staff staff = _dbcontext.Staffs.Where(s => s.StaffId == account.StaffId).FirstOrDefault();

                    Staff staff = _dbcontext.Staffs.Where(s => s.Email == UserEmail).FirstOrDefault();

                    return (int)staff.RoleId;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Login(object parameter)
        {
            Debug.WriteLine("email: " + _loginViewModel.UserEmail);
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                _loginViewModel.Password = ConvertToUnsecureString(secureString);
                Debug.WriteLine("password: " + _loginViewModel.Password);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_loginViewModel.UserEmail) && !string.IsNullOrEmpty(_loginViewModel.Password) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Debug.WriteLine(_loginViewModel.UserEmail, _loginViewModel.Password);
            var isLogin = await FirebaseHelper.loginWithEmailAndPasswordAsync(_loginViewModel.UserEmail, _loginViewModel.Password);
            //var isLogin = true;
            Login(parameter);

            if (isLogin)
            {
                //var roleId = await SetRole(_loginViewModel.UserEmail);
                //var roleId = 1;
                _navigationStore.CurrentViewModel = null ;
                

                FrameworkElement window = GetWindowParent(parameter as UserControl);
                var w = window as Window;
                if (w != null)
                {
                    _loginViewModel.IsLoggedIn = true;
                    MainWindow mainWindow = new MainWindow()
                    {
                        DataContext = new MainViewModel(_loginViewModel.UserEmail, _navigationStore)
                    };

                    mainWindow.Show();
                    w.Close();
                }
            }
            else
            {
                var view = new SampleErrorDialog();
                
                await DialogHost.Show(view, "LoginRootDialog");
            }
        }
        public LoginCommand(LoginViewModel loginViewModel, NavigationStore navigationStore)
        {
            _loginViewModel = loginViewModel;
            _navigationStore = navigationStore;
            _loginViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.UserEmail) || e.PropertyName == nameof(LoginViewModel.Password))
            {
                
                OnExecutedChanged();
            }
        }
    }
}
