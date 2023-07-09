using GymManagement.Commands;
using GymManagement.Dialogs;
using GymManagement.Models;
using LiveChartsCore;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagement.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        #region DataFields
        private GymManagementDbContext DbContext { get; set; }
        private ObservableCollection<Course> _courseContext { get; set; }

        public ObservableCollection<Course> CourseContext
        {
            get => _courseContext;
            set { _courseContext = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Course> _courseContextClone { get; set; }

        public ObservableCollection<Course> CourseContextClone
        {
            get => _courseContextClone;
            set { _courseContextClone = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TypesOfCourse> _typeList { get; set; }

        public ObservableCollection<TypesOfCourse> TypeList
        {
            get => _typeList;
            set { _typeList = value; OnPropertyChanged(); }
        }
        private Course _selectedCourse { get; set; }
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                base.SelectedItem = _selectedCourse;
                OnPropertyChanged();
            }
        }

        private IEnumerable<ISeries> _seriesMostFavorite { get; set; }
        public IEnumerable<ISeries> SeriesMostFavorite
        {
            get { return _seriesMostFavorite; }
            set
            {
                _seriesMostFavorite = value; OnPropertyChanged();
            }
        }

        private Course _mostFavoriteCourse { get; set; }

        public Course MostFavoriteCourse
        {
            get
            {
                return _mostFavoriteCourse;
            }
            set
            {
                _mostFavoriteCourse = value; OnPropertyChanged();
            }
        }

        private IEnumerable<ISeries> _seriesMostValueable { get; set; }
        public IEnumerable<ISeries> SeriesMostValueable
        {
            get { return _seriesMostValueable; }
            set
            {
                _seriesMostValueable = value; OnPropertyChanged();
            }
        }

        private Course _mostValueableCourse { get; set; }

        public Course MostValueableCourse
        {
            get
            {
                return _mostValueableCourse;
            }
            set
            {
                _mostValueableCourse = value; OnPropertyChanged();
            }
        }


        #endregion
        protected async Task ShowErrorDialog()
        {
            var view = new SampleErrorDialog();
            var result = await DialogHost.Show(view, "RootDialog");
        }
        private async void ExtendedClosingEventHandlerForAddNewCourse(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddNewCourseDialog;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddNewCourseViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewCourseAsync();
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    //OK, lets cancel the close...
                    eventArgs.Cancel();


                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _courseContext = viewmodel.Sync();
                    }


                    //lets run a fake operation for 3 seconds then close this baby.
                    //Task.Delay(TimeSpan.FromSeconds(3))
                    //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    //        TaskScheduler.FromCurrentSynchronizationContext());

                }

            }
        }
        public override async Task AddNewData()
        {
            using (DbContext = new GymManagementDbContext())
            {
                List<string> TypeList = new List<string>();
                foreach (TypesOfCourse type in DbContext.TypesOfCourses.ToList())
                {
                    if (type != null)
                    {
                        TypeList.Add(type.Name);
                    }
                }
                var view = new AddNewCourseDialog
                {
                    DataContext = new AddNewCourseViewModel(_courseContext, TypeList)
                };
                var result = await DialogHost.Show(view, "RootDialog", null, ExtendedClosingEventHandlerForAddNewCourse, null);
            }
        }

        public override async Task DeleteData(object model)
        {
            using (DbContext = new GymManagementDbContext())
            {
                DbContext.Remove<Course>(model as Course);
                DbContext.SaveChanges();
                _courseContext.Remove(model as Course);
            }
        }
        public override async Task UpdateData()
        {
            try
            {
                using (DbContext = new GymManagementDbContext())
                {
                    Course replace = SelectedCourse;
                    
                    Course current = DbContext.Courses.Where(t => t.CourseId.Equals(SelectedCourse.CourseId)).FirstOrDefault();
                    TypesOfCourse oldtype = DbContext.TypesOfCourses.Where(t => t.TypeId == current.Type).FirstOrDefault();
                    current.TypeNavigation = oldtype;
                    if (current != null)
                    {
                        if (!(current.Name == replace.Name &&
                            current.Price == replace.Price &&
                            current.Type == replace.Type &&
                            current.Duration == replace.Duration &&
                            current.NumberOfSession == replace.NumberOfSession &&
                            current.Active == replace.Active &&
                            current.TypeNavigation == replace.TypeNavigation && 
                            current.Description == replace.Description))
                        {
                            current.Name = replace.Name;
                            current.Price = replace.Price;
                            current.Duration = replace.Duration;
                            current.Active = replace.Active;
                            current.NumberOfSession = replace.NumberOfSession;
                            current.Description = replace.Description;
                            Debug.WriteLine(current.TypeNavigation.Name);
                            Debug.WriteLine(replace.TypeNavigation.Name);

                            if (current.TypeNavigation.Name != replace.TypeNavigation.Name)
                            {
                                TypesOfCourse newtype = DbContext.TypesOfCourses.Where(t => t.TypeId == replace.TypeNavigation.TypeId).FirstOrDefault();
                                current.Type = newtype.TypeId;
                                current.TypeNavigation = replace.TypeNavigation;
                            }
                            else
                            {
                                current.Type = replace.Type;
                            }

                            DbContext.SaveChanges();


                            for (int i = 0; i < _courseContext.Count; i++)
                            {
                                if (_courseContext[i].CourseId == replace.CourseId)
                                    _courseContext[i] = replace;
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }
        public ICommand AddCourseTypeCommand { get; set; }
        public CourseViewModel()
        {
            GridChangeCommand = new GridChangeCommand(this);
            DeleteDataCommand = new DeleteDataCommand(this);
            AddCourseTypeCommand = AddCourseTypeCommandSingleton.Instance(this);
        }


    }
}
