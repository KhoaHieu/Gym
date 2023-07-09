using GymManagement.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagement.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private object _selectedItem { get; set; }
        public object SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }
       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void SearchData(String content)
        {
            // Please Implementation this method
        }
        public virtual async Task DeleteData(object model)
        {
            // Please Implementation this method
        }

        public virtual async Task UpdateData()
        {
            // Please Implementation this method
        }

        public virtual async Task AddNewData()
        {
            // Please Implementation this method
        }

    }
}


//protected async Task ShowErrorDialog()
//{
//    var view = new SampleErrorDialog();
//    var result = await DialogHost.Show(view, "RootDialog");
//}
