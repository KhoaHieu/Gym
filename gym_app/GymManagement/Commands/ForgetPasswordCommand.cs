using GymManagement.Dialogs;
using GymManagement.Utils;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymManagement.Commands
{
    class ForgetPasswordCommand : BaseAsyncCommand
    {
        private ForgetPasswordViewModel _forgetPasswordViewModel;
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_forgetPasswordViewModel.Email) && base.CanExecute(parameter);
        }

        public ForgetPasswordCommand(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            _forgetPasswordViewModel = forgetPasswordViewModel;
            _forgetPasswordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ForgetPasswordViewModel.Email))
            {

                OnExecutedChanged();
            }
        }
        public override async Task ExecuteAsync(object parameter)
        {
            
            var isSent = await FirebaseHelper.resetPassword(_forgetPasswordViewModel.Email);
            if (isSent)
            {
                var view = new SampleMessageDialog
                {
                    DataContext = new SampleMessageViewModel("Check your email to reset password")
                };

                await DialogHost.Show(view, "LoginRootDialog");
            }
            else
            {
                var view = new SampleErrorDialog();

                await DialogHost.Show(view, "LoginRootDialog");
            }
                
        }
    }
}

