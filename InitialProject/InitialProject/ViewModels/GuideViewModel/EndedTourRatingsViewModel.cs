using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Utilities;
using InitialProject.Domain.Model;
using InitialProject.Service;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InitialProject.View;

namespace InitialProject.ViewModels
{
    public class EndedTourRatingsViewModel 
    {

        private TourService _tourService;
        public List<Tour> EndedTours { get; set; }
        public Tour SelectedTour { get; set; }
        public List<RatedGuideTour> TourGuests { get; set; }
        public RatedGuideTourService _ratedGuideTourService;
        public Tour Tour { get; set; }

        public EndedTourRatingsViewModel()
        {
            _ratedGuideTourService = new RatedGuideTourService();
            _tourService = new TourService();
            EndedTours = _tourService.FindAllEndedTours();
            ShowRatingsCommand = new RelayCommand(Show);

        }

        public ICommand ShowRatingsCommand { get; private set; }

        private void Show()
        {
            if(SelectedTour != null)
            {
                TourGuestRating tourGuestRating = new TourGuestRating(SelectedTour);
                tourGuestRating.Show();
            }
        }


    }
}
