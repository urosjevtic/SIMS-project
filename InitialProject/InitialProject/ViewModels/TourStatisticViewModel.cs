using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Domain.Model;
using InitialProject.View;

namespace InitialProject.ViewModels
{
    public class TourStatisticViewModel
    {
        public TourStatisticService _tourStatisticService;
        public TourService _tourService; 
        public List<Tour> EndedTours { get; set; }  
        public Tour SelectedTour { get; set; }  

        public TourStatisticViewModel()
        {
            _tourService = new TourService();
            _tourStatisticService = new TourStatisticService();
            EndedTours = _tourService.FindAllEndedTours();

        }

       public void ShowStatistics()
        {
            if(SelectedTour != null)
            {
                OneTourStatistic oneTourStatistic = new OneTourStatistic(SelectedTour);
                oneTourStatistic.Show();
            }
        }




    }
}
