using GymManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Commands
{
    public class AddCourseTypeCommandSingleton
    {
        private static AddCourseTypeCommand _Instance;
        private static readonly object _lockObject = new object();

        private AddCourseTypeCommandSingleton()
        {
        }

        public static AddCourseTypeCommand Instance(CourseViewModel viewmodel)
        {
            lock (_lockObject)
            {
                if (_Instance == null)
                {
                    _Instance = AddCourseTypeCommand.Instance(viewmodel);
                }
                return _Instance;
            }
        }
    }

}
