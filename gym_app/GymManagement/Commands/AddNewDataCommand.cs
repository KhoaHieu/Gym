using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GymManagement.ViewModels;

namespace GymManagement.Commands
{
    public class AddNewDataCommand : BaseAsyncCommand
    {
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {

                FrameworkElement usercontrol = GetWindowParent(parameter as UserControl);
                var w = usercontrol as UserControl;
                if (w != null)
                {
                    var basevm = w.DataContext as BaseViewModel;
                    await basevm.AddNewData();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
