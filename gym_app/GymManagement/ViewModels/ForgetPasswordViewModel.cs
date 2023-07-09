using GymManagement.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GymManagement.Commands;

namespace GymManagement.ViewModels
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public ICommand ForgetPasswordCommand { get; set; }

        public ForgetPasswordViewModel()
        {
            ForgetPasswordCommand = new ForgetPasswordCommand(this);
            //QuenMatKhauCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, async (p) =>
            //{
            //    Debug.WriteLine(Email);
            //    var isSent = await FirebaseHelper.resetPassword(Email);
            //    if (isSent)
            //    {
            //        MessageBox.Show("Check your email to reset password");
            //    }
            //    else
            //        MessageBox.Show("Check your information again!");
            //}
            //);
            
        }
    }
}
