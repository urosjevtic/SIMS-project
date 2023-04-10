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
    public class ShowTourRatingsViewModel
    {

        private RatedGuideTourService _ratedGuideTourService;
        private TourService _tourService;
        public List<Tour> EndedTours { get; set; }
        public Tour SelectedTour { get; set; }
        public List<RatedGuideTour> TourGuests { get; set; }
        public RatedGuideTourService _ratedTourGuideService;
        public Tour Tour { get; set; }

        public ShowTourRatingsViewModel()
        {
            _ratedGuideTourService = new RatedGuideTourService();
            _tourService = new TourService();
            EndedTours = _tourService.FindAllEndedTours();
            ShowRatingsCommand = new RelayCommand(Show);

            Tour = SelectedTour;
            //_commmentRepository = new CommentRepository();
            _ratedTourGuideService = new RatedGuideTourService();
            TourGuests = _ratedTourGuideService.FindAllTourRatings(Tour);
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
