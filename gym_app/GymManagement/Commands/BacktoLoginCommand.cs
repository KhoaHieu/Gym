using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GymManagement.Commands
{
    public class BacktoLoginCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        public BacktoLoginCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {

                _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore);
                Window p = parameter as Window;
                LoginWindow login = new LoginWindow()
                {
                    DataContext = _navigationStore.CurrentViewModel
                };
                login.Show();
                p.Close();
            }
        }
    }
}
