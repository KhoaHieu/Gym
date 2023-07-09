using GymManagement.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.ViewModels
{
    class ContractsCountChartViewModel : BaseViewModel
    {
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
        public LabelVisual TitleContract { get; set; } = LiveChartsService.TitleContract;
        public ContractsCountChartViewModel()
        {
            SeriesContract = LiveChartsService.TotalContractInAWeekSeries();
        }
    }
}
