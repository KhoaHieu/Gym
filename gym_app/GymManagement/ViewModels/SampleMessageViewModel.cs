using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    class SampleMessageViewModel : BaseViewModel
    {
        private string _message { get; set; }
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }
        public SampleMessageViewModel(String message)
        {
            _message = message;
        }
    }
}
