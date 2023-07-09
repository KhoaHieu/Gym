using CommunityToolkit.Mvvm.Input;
using GymManagement.Stores;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GymManagement.Models;
using GymManagement.Services;
using System.Globalization;

namespace GymManagement.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        

        public ICommand OneWeekCommand { get; set; }
        public ICommand OneMonthCommand { get; set; }
        public ICommand OneYearCommand { get; set; }

        private static GymManagementDbContext _dbcontext;
        private ISeries[] _seriesTotalRevenue { get; set; }
        public ISeries[] SeriesTotalRevenue
        {
            get
            {
                return _seriesTotalRevenue;
            }
            set
            {
                _seriesTotalRevenue = value; OnPropertyChanged();
            }
        }




        public LabelVisual TitleTotalRevenue { get; set; } = LiveChartsService.TitleTotalRevenue;

        

        public static int TotalContractsCountByDay(int day)
        {
            using (_dbcontext = new GymManagementDbContext())
            {
                return _dbcontext.Contracts.Where(s => s.CreateDate.Value.Day == day).Count();
            }
        }


        public static int TotalContractsCountByMonth(int month)
        {
            using (_dbcontext = new GymManagementDbContext())
            {
                return _dbcontext.Contracts.Where(s => s.CreateDate.Value.Month == month).Count();
            }
        }

        public LabelVisual TitleContract { get; set; } = LiveChartsService.TitleContract;
           

        private ISeries[] OneWeekSeries()
        {
            return new ISeries[]
            {
                 new ColumnSeries<DateTimePoint>
            {
                TooltipLabelFormatter = (chartPoint) =>
                    $"{new DateTime((long) chartPoint.SecondaryValue):dddd}: {chartPoint.PrimaryValue}",
                Values = LiveChartsService.TotalContractInAWeek()
            }
            };
        }



        private ISeries[] OneMonthSeries()
        {
            return new ISeries[]
            {
                 new ColumnSeries<DateTimePoint>
            {
                TooltipLabelFormatter = (chartPoint) =>
                    $"{new DateTime((long) chartPoint.SecondaryValue):MMM dd}: {chartPoint.PrimaryValue}",
                Values = DaysInMonth()
            }
            };
        }

        private ISeries[] OneYearSeries()
        {
            return new ISeries[]
            {
                new ColumnSeries<DateTimePoint>
            {
                TooltipLabelFormatter = (chartPoint) =>
                    $"{new DateTime((long) chartPoint.SecondaryValue):MMMM}: {chartPoint.PrimaryValue}",

                Values = MonthsInYear()
            }
            };
        }
        private static IEnumerable<DateTimePoint> DaysInWeek()
        {
            int numberofday = 7;
            var Value = new ObservableCollection<DateTimePoint>();
            for (int i = 0; i < numberofday; i++)
            {
                Value.Add(new DateTimePoint(DateTime.Today.AddDays(-i), TotalContractsCountByDay(DateTime.Today.AddDays(-i).Day)));
            }
            return Value;
        }
        private static IEnumerable<DateTimePoint> DaysInMonth()
        {
            int numberofday = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var Value = new ObservableCollection<DateTimePoint>();
            for (int i = 0; i < numberofday; i++)
            {
                Value.Add(new DateTimePoint(DateTime.Today.AddDays(-i), TotalContractsCountByDay(DateTime.Today.AddDays(-i).Day)));
            }
            return Value;
        }
        private static IEnumerable<DateTimePoint> MonthsInYear()
        {
            int numberofmonths = 12;
            var Value = new ObservableCollection<DateTimePoint>();
            for (int i = 0; i < numberofmonths; i++)
            {
                Value.Add(new DateTimePoint(DateTime.Today.AddMonths(-i), TotalContractsCountByMonth(DateTime.Today.AddMonths(-i).Month)));
            }
            return Value;
        }
        public ISeries[] SeriesTotalRevenueOneYear { get; set; } =
        {

                new LineSeries<DateTimePoint>
                            {

                                TooltipLabelFormatter = (chartPoint) =>
                    $"{new DateTime((long) chartPoint.SecondaryValue):MMMM}: {chartPoint.PrimaryValue}",

                                Values = new ObservableCollection<DateTimePoint>
                                {
                                    new DateTimePoint(DateTime.Today.AddMonths(-11),19),
                    new DateTimePoint(DateTime.Today.AddMonths(-10),12),
                    new DateTimePoint(DateTime.Today.AddMonths(-9),21),
                    new DateTimePoint(DateTime.Today.AddMonths(-8),10),
                    new DateTimePoint(DateTime.Today.AddMonths(-7),8),
                    new DateTimePoint(DateTime.Today.AddMonths(-6),23),
                    new DateTimePoint(DateTime.Today.AddMonths(-5),27),
                    new DateTimePoint(DateTime.Today.AddMonths(-4),22),
                    new DateTimePoint(DateTime.Today.AddMonths(-3),19),
                    new DateTimePoint(DateTime.Today.AddMonths(-2),31),
                    new DateTimePoint(DateTime.Today.AddMonths(-1),19),
                    new DateTimePoint(DateTime.Today,9)

                                }
                    }



        };

        

        private ISeries[] _series { get; set; }
        public ISeries[] SeriesContract
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value;
                OnPropertyChanged(nameof(SeriesContract));
            }
        }
        public Axis[] XAxesContract { get; set; } = LiveChartsService.OneWeekXAxes;
        private Axis[] _xAxes { get; set; }
        public Axis[] XAxes
        {
            get { return _xAxes; }
            set { _xAxes = value; OnPropertyChanged(); }
        }

        public Axis[] XAxesTotalRevenue { get; set; } = LiveChartsService.OneYearXAxes;

        public void InitializeChart()
        {
            SeriesTotalRevenue = LiveChartsService.TotalRevenueOneYearSeries();
            
            SeriesContract = LiveChartsService.TotalContractInAWeekSeries();
            
        }

        //public async Task MainSeries()
        //{
        //    SeriesMoney = OneYearSeriesMoney();
        //    if (Flag == 1)
        //    {

        //        Series = OneWeekSeries();
        //    }
        //    else if (Flag == 2)
        //    {
        //        Series = OneMonthSeries();
        //    }
        //    else if (Flag == 3)
        //    {
        //        Series = OneYearSeries();
        //    }
        //}

        private Axis[] OneWeekXAxes { get; set; } =
        {

        new Axis
        {
            Labeler = value => new DateTime((long) value).ToString("dddd"),
            LabelsRotation = 10,
            Padding = new LiveChartsCore.Drawing.Padding(0, 0, 0, 0),
            
             UnitWidth = TimeSpan.FromDays(1).Ticks,
            
            MinStep = TimeSpan.FromDays(1).Ticks
        }
    };
        private Axis[] OneMonthXAxes { get; set; } =
        {
            new Axis
            {


                Labeler = value => new DateTime((long) value).ToString("dd"),
                LabelsRotation = 10,
            
                Padding = new LiveChartsCore.Drawing.Padding(0,0,0,0),
                UnitWidth = TimeSpan.FromDays(1).Ticks,
            
            
                MinStep = TimeSpan.FromDays(1).Ticks
            }
        };

        private Axis[] OneYearXAxes { get; set; } =
        {
            new Axis
            {
                Labeler = value => new DateTime((long) value).ToString("MMMM"),
                LabelsRotation = 10,
                Padding = new LiveChartsCore.Drawing.Padding(0,0,0,0),
            
            
            
                UnitWidth = TimeSpan.FromDays(30.4375).Ticks,
            
                MinStep = TimeSpan.FromDays(30.4375).Ticks
            }
        };

        private int Flag;

        
        //public async Task MainXAexs()
        //{
        //    XAxesTotalRevenue = OneYearXAxes;
        //    if (Flag == 1)
        //    {
        //        XAxesContract = OneWeekXAxes;
        //    }
        //    else if (Flag == 2)
        //    {
        //        XAxesContract = OneMonthXAxes;
        //    }
        //    else if (Flag == 3)
        //    {
        //        XAxesContract = OneYearXAxes;
        //    }
        //}
        public HomeViewModel()
        {
            Flag = 1;
            //MainSeries();
            //MainXAexs();
            //    OneWeekCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            //    {
            //        if (Flag == 1)
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            Flag = 1;
            //            MainSeries();
            //            MainXAexs();
            //        }
            //    }
            //    );
            //    OneMonthCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            //    {
            //        if (Flag == 2)
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            Flag = 2;
            //            MainSeries();
            //            MainXAexs();
            //        }
            //    }
            //    );
            //    OneYearCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            //    {
            //        if (Flag == 3)
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            Flag = 3;
            //            MainSeries();
            //            MainXAexs();
            //        }
            //    }
            //    );
        }
    }

}
