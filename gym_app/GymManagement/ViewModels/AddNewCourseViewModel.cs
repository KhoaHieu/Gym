using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    public class AddNewCourseViewModel : BaseViewModel
    {
        private bool _flag = true;
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }

        private string _courseName;

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; OnPropertyChanged(); }
        }
        private string _price;

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;

                OnPropertyChanged();
            }
        }
        private string _duration;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; OnPropertyChanged(); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        private string _description;



        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private List<string> _typeList;

        public List<string> TypeList
        {
            get { return _typeList; }
            set { _typeList = value; OnPropertyChanged(); }
        }
        private string _selectedType { get; set; }
        public string SelectedType
        {
            get => _selectedType;
            set { _selectedType = value; OnPropertyChanged(); }
        }
        private GymManagementDbContext DbContext { get; set; }



        public void AddNewCourse()
        {
            _ = AddNewCourseAsync();
        }

        public ObservableCollection<Course> Sync()
        {
            return CourseContext;
        }

        public Task AddNewCourseAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (DbContext = new GymManagementDbContext())
                {
                    var type = DbContext.TypesOfCourses.Where(s => s.Name == SelectedType).FirstOrDefault();
                    
                    if (CourseName != null && Price != null && Duration != null)
                    {
                        Course course = new()
                        {
                            Name = CourseName,
                            Price = Int32.Parse(Price),
                            TypeNavigation = type,
                            Type = type.TypeId,
                            Duration = Int32.Parse(Duration),
                            Description = Description,
                            Active = true
                        };

                        DbContext.Add<Course>(course);
                        DbContext.SaveChanges();
                        CourseContext.Add(course);
                    }
                    else
                    {
                        Flag = false;
                    }

                }
            });
        }

        private ObservableCollection<Course> CourseContext;
        public AddNewCourseViewModel(ObservableCollection<Course> _courseContext, List<string> typeList)
        {
            CourseContext = _courseContext;
            _typeList = typeList;
        }
    }
}
