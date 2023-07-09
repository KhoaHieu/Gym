using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using GymManagement.Stores;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;
using GymManagement.PasswordSecure;
using GymManagement.Commands;
using System.Globalization;

namespace GymManagement.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public bool IsLoggedIn { get; set; }
        private string _UserEmail;
        public string UserEmail { get => _UserEmail; set { _UserEmail = value; OnPropertyChanged(); } }
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand PasswordVisibleCommand { get; set; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            IsLoggedIn = false;
            _navigationStore = navigationStore;
            
            LoginCommand = new LoginCommand(this, _navigationStore);
        }

        
    }
}
