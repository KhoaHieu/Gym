using GymManagement.Dialogs;
using GymManagement.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    class NotSupportedCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            DialogHost.Show(new SampleMessageDialog() { DataContext = new SampleMessageViewModel("Tính năng này chưa được hỗ trợ!")}, "RootDialog");
        }
    }
}
