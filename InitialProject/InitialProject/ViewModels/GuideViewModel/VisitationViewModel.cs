using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InitialProject.Service;
using InitialProject.Domain.Model;
using LiveCharts.Wpf;


namespace InitialProject.ViewModels
{
    public class VisitationViewModel : BaseViewModel
    {
        private readonly TourStatisticService _tourStatisticService;        
        public ObservableCollection<AgeCategory> AgeCategories { get; set; }
        public PieChart PieChart { get; set; }
        public string TourName { get; set; }
        public VisitationViewModel(Tour tour,PieChart pieChart)
        {
            _tourStatisticService = new TourStatisticService();
            AgeCategories = new ObservableCollection<AgeCategory>();
            
            AgeCategories.Add(new AgeCategory() { Category = "Under 18", Count =_tourStatisticService.FindPeopleUntil18(tour) });
            AgeCategories.Add(new AgeCategory() { Category = "18-50", Count = _tourStatisticService.FindPeopleBetween18and50(tour) });
            AgeCategories.Add(new AgeCategory() { Category = "Over 50", Count = _tourStatisticService.FindPeopleOver50(tour) });
             
            TourName = tour.Name;

            PieChart = pieChart;    

            LiveCharts.SeriesCollection psc = new LiveCharts.SeriesCollection
{
            new PieSeries
            {
                Values = new LiveCharts.ChartValues<double> {_tourStatisticService.FindPeopleWithVoucher(tour)},Title = "With voucher"
            },
            new PieSeries
            {

                Values = new LiveCharts.ChartValues<double> {_tourStatisticService.FindPeopleWithoutVoucher(tour)},Title = "Without voucher"
            }
            };

            foreach (PieSeries ps in psc)
            {
                PieChart.Series.Add(ps);
            }
        }

      
        public class AgeCategory
        {
            public string Category { get; set; } // kazem koja je kategorija koju prikazujem
            public int Count { get; set; }  //koliko iammm u njoj
        }



      
    }
}
