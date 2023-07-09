using GymManagement.Stores;
using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class CourseNavigationCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private readonly CourseViewModel _courseViewModel;
        public CourseNavigationCommand(NavigationStore navigationStore, CourseViewModel courseViewModel)
        {
            _navigationStore = navigationStore;
            _courseViewModel = courseViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return !_navigationStore.CurrentViewModel.Equals(_courseViewModel) && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _courseViewModel;
        }
    }
}
