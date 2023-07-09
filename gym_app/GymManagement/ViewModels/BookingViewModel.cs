using GymManagement.Commands;
using GymManagement.Models;
using GymManagement.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
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
    public class BookingViewModel : BaseViewModel
    {

        #region Data Fields
        public ContractViewModel contractviewmodel;
        public StaffViewModel staffViewModel;
        
        private ObservableCollection<Booking> _bookingContext { get; set; }

        public ObservableCollection<Booking> BookingContext
        {
            get => _bookingContext;
            set { _bookingContext = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<Staff> _ptContext { get; set; }
        public ObservableCollection<Staff> PtContext
        {
            get
            {
                ObservableCollection<Staff> PtContext = new ObservableCollection<Staff>();
                foreach (Staff staff in _ptContext)
                {
                    if (staff.RoleId == 2)
                    {
                        PtContext.Add(staff);
                    }
                }
                return PtContext;
            }
            set { _ptContext = value; OnPropertyChanged(); }
        }
        private Staff _selectedStaff { get; set; }
        public Staff SelectedStaff
        {
            get => _selectedStaff;
            set
            {
                _selectedStaff = value;
                base.SelectedItem = _selectedStaff;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Chart Properties

        private ISeries[] _series { get; set; }
        public ISeries[] SeriesBooking
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value; OnPropertyChanged();
            }
        }
        public LabelVisual TitleBookingInAWeek { get; set; } 

        public Axis[] XAxesBooking { get; set; }
        

        
        public void InitializeChart()
        {
            TitleBookingInAWeek = LiveChartsService.TitleBookingInAWeek;
            SeriesBooking = LiveChartsService.TotalBookingsToDaySeries();
            XAxesBooking = LiveChartsService.OneWeekXAxes;
        }

        #endregion

        public ICommand CreateBookingCommand { get; set; }
        public BookingViewModel()
        {
            CreateBookingCommand = new CreateBookingCommand(this);
            
        }

    }
}
