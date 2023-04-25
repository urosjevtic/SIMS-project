using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service.StatisticService;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationYearlyStatisticViewModel :BaseViewModel
    {
        public ObservableCollection<AccommodationStatistic> Statistics { get; set; }
        private readonly YearlyAccommodationService _yearlyAccommodationService;

        public MyAccommodationYearlyStatisticViewModel(int accommodationId)
        {
            _yearlyAccommodationService = new YearlyAccommodationService();
            Statistics = new ObservableCollection<AccommodationStatistic>(_yearlyAccommodationService.GetStatisticByAccommodationId(accommodationId));
        }
    }
}
