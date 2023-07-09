using GymManagement.Models;
using GymManagement.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewCourseTypeViewModel : BaseViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }

        }
        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        private GymManagementDbContext _dbcontext;
        private ObservableCollection<TypesOfCourse> TypeOfCourseContext;
        public bool Flag { get; set; }

        public void AddNewTypeOfCourse()
        {
            
                using (_dbcontext = new GymManagementDbContext())
                {
                if (Name != null)
                {
                    Flag = true;
                    TypesOfCourse type = new TypesOfCourse();
                    type.Name = Name;
                    _dbcontext.TypesOfCourses.Add(type);
                    TypeOfCourseContext.Add(type);
                    _dbcontext.SaveChanges();
                }
                else Flag = false;
                }
        }
        public AddNewCourseTypeViewModel(CourseViewModel viewmodel)
        {
            TypeOfCourseContext = viewmodel.TypeList;
        }
    }
}
