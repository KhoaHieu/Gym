using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using GymManagement.Commands;

namespace GymManagement.ViewModels
{
    public class WindowTaskBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseDragWindowCommand { get; set; }
        #endregion
        public WindowTaskBarViewModel()
        {

            CloseWindowCommand = new CloseWindowCommand();
            MinimizeWindowCommand = new MinimizeWindowCommand();
            MouseDragWindowCommand = new MouseDragWindowCommand();

        }
    }
}
