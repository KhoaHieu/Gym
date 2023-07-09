using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GymManagement.ViewModels
{
    class ContractDetailViewModel : BaseViewModel
    {
        
        private string _customername { get; set; }
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                _customername = value; OnPropertyChanged();
            }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }

        private string _identitynumber;

        public string IdentityNumber
        {
            get { return _identitynumber; }
            set { _identitynumber = value; OnPropertyChanged(); }
        }

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }

        private DateTime _birthDay { get; set; }

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; OnPropertyChanged(); }
        }
        private string _courseType { get; set; }
        public string CourseType
        {
            get
            {
                return _courseType;
            }
            set
            {
                _courseType = value; OnPropertyChanged();
            }
        }
        private string _course { get; set; }
        public string Course
        {
            get
            {
                return _course;
            }
            set
            {
                _course = value; OnPropertyChanged();
            }
        }
        private string _price { get; set; }
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value; OnPropertyChanged();
            }
        }
        private string _description { get; set; }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value; OnPropertyChanged();
            }
        }

        private DateTime _createDate { get; set; }
        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value; OnPropertyChanged();
            }
        }
        private DateTime _finishDate { get; set; }
        public DateTime FinishDate
        {
            get
            {
                return _finishDate;
            }
            set
            {
                _finishDate = value; OnPropertyChanged();
            }
        }
        private GymManagementDbContext Context;
        public ContractDetailViewModel(ContractViewModel viewmodel)
        {
            using (Context = new GymManagementDbContext())
            {
                // thong tin hop dong
                Course = viewmodel.SelectedContract.Course.Name;
                TypesOfCourse type = Context.TypesOfCourses.Where(s => s.TypeId == viewmodel.SelectedContract.Course.Type).FirstOrDefault();
                CourseType = type.Name;
                Price = ((int)viewmodel.SelectedContract.Course.Price).ToString("#,##0" + " VND");
                CreateDate = (DateTime)viewmodel.SelectedContract.CreateDate;
                FinishDate = (DateTime)viewmodel.SelectedContract.FinishDate;
                //var course = Context.Courses.Where(s => s.CourseId == contract.CourseId).FirstOrDefault();

                //Course = course.Name;
                //Price = course.Price.ToString();
                //CreateDate = (DateTime)contract.CreateDate;
                //FinishDate = (DateTime)contract.FinishDate;
                //Description = contract.Description;

                // thong tin khach hang
                var customer = Context.Customers.Where(s => s.CustomerId == viewmodel.SelectedContract.CustomerId).FirstOrDefault();
                CustomerName = customer.Name;
                IdentityNumber = customer.IdentityNumber;
                Gender = customer.Gender;
                BirthDay = (DateTime) customer.Birthday;
                Phone = customer.Phone;

            }

        }
    }
}
