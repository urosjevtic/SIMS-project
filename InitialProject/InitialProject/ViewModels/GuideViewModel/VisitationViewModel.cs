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
using System.Windows.Data;
using System.Drawing;
using LiveCharts;

namespace InitialProject.ViewModels
{
    public class VisitationViewModel : BaseViewModel
    {
        private readonly TourStatisticService _tourStatisticService;        
        public ObservableCollection<AgeCategory> AgeCategories { get; set; }
        public PieChart PieChart { get; set; }
        public string TourName { get; set; }
        public string Until18years { get; set; }
        public string Between18and50years { get; set; }
        public string Over50years { get; set; }
        public VisitationViewModel(Tour tour,PieChart pieChart)
        {
            _tourStatisticService = new TourStatisticService();
            AgeCategories = new ObservableCollection<AgeCategory>();
            
            AgeCategories.Add(new AgeCategory() { Category = "0-18 years", Count =_tourStatisticService.FindPeopleUntil18(tour) });
            AgeCategories.Add(new AgeCategory() { Category = "18-50 years", Count = _tourStatisticService.FindPeopleBetween18and50(tour) });
            AgeCategories.Add(new AgeCategory() { Category = "50+ years", Count = _tourStatisticService.FindPeopleOver50(tour) });
             
            TourName =  tour.Name + "  statistics";

            PieChart = pieChart;
            Until18years = "Younger than 18:  " + _tourStatisticService.FindPeopleUntil18(tour);
            Between18and50years = "18-50 years: " + _tourStatisticService.FindPeopleBetween18and50(tour);
            Over50years = "Older than 50: " + _tourStatisticService.FindPeopleOver50(tour);
            SeriesCollection psc = new SeriesCollection
{
            new PieSeries
            {
                Values = new ChartValues<double> {_tourStatisticService.FindPeopleWithVoucher(tour)},Title = "With voucher"
            },
            new PieSeries
            {

                Values = new ChartValues<double> {_tourStatisticService.FindPeopleWithoutVoucher(tour)},Title = "Without voucher"
            }
            };

            foreach (PieSeries ps in psc)
            {
                PieChart.Series.Add(ps);
            }

        }
    

        public class AgeCategory
        {
            public string Category { get; set; } 
            public int Count { get; set; }
        }


       


    }
}
