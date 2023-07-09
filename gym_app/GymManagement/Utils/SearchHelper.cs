using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Utils
{
    enum Models
    {
        Customer,
        Staff,
        Facility,
        Contract,
        Course,
        Booking,
        Role
    }
    class SearchHelper<T>
    {
        private ObservableCollection<T> _context;
        private Type setModel()
        {
            return _context.GetType();
        }
        public SearchHelper(ObservableCollection<T> context)
        {
            _context = context;
            //Debug.WriteLine(setModel());
            //string _model = this._context.GetType().AssemblyQualifiedName.ToString().Split(",")[0];
            //int startIndex = _model.IndexOf("1[") + 3;
            //int _length = _model.Length - startIndex;
            //Debug.WriteLine(_model.Substring(startIndex, _length));
        }

        public ObservableCollection<T> Result(string Content)
        {
            if (Content != "")
            {
                ObservableCollection<T> searchResult = new ObservableCollection<T>();
                foreach (var item in _context)
                {
                    bool isContain = isTrue(item, Content);
                    if (isContain)
                        searchResult.Add(item);
                }
                return searchResult;
            }
            return null;
        }

        public bool isTrue(T item, string content)
        {

            switch (item.GetType().Name)
            {
                case "Customer":
                    Customer customer = item as Customer;
                    if (customer != null)
                    {
                        if (customer.Name.IndexOf(content) != -1)
                        {
                            return true;
                        }
                    }
                    break;
                case "Staff":
                    Staff staff = item as Staff;
                    if (staff != null)
                    {
                        if (staff.Name.IndexOf(content) != -1)
                        {
                            return true;
                        }
                    }
                    break;
                case "Contract":
                    Contract contract = item as Contract;
                    if (contract != null)
                    {
                        if (contract.Customer.Name.IndexOf(content) != -1)
                        {
                            return true;
                        }
                    }
                    break;
                case "Facility":
                    Facility facility = item as Facility;
                    if (facility != null)
                    {
                        if (facility.Name.IndexOf(content) != -1)
                        {
                            return true;
                        }
                    }
                    break;
                case "Course":
                    Course course = item as Course;
                    if (course != null)
                    {
                        if (course.Name.IndexOf(content) != -1)
                        {
                            return true;
                        }
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
