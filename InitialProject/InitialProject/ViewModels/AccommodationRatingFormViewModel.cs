using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.RenovationServices;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.Ratings;
using System.Windows.Input;
using InitialProject.Utilities;
using System.Windows;

namespace InitialProject.ViewModels
{
    public class AccommodationRatingFormViewModel :BaseViewModel
    {
        public UnratedOwner UnratedOwner { get; set; }

        public readonly OwnerRatingService _ownerRatingService;
        public readonly RenovationRecommendationService _renovationRecommendationService;
        private readonly RecommendationService _recommendationService;
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        public NavigationService NavigationService { get; set; }

        // public AccommodationReservation Reservation { get; set; }

        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        

        public AccommodationRatingFormViewModel(UnratedOwner unratedOwner, NavigationService navigationService)
        {
            UnratedOwner = unratedOwner;
            _ownerRatingService = new OwnerRatingService();
            _renovationRecommendationService=new RenovationRecommendationService();
            NavigationService = navigationService;
            _recommendationService = new RecommendationService();
        }

        private string _additionalComment;
        public string AdditionalComment
        {
            get { return _additionalComment; }
            set
            {
                if (value != _additionalComment)
                {
                    _additionalComment = value;
                    OnPropertyChanged(nameof(AdditionalComment));
                }
            }
        }

        private int _ownerCorrectness = 1;
        public int OwnerCorrectness
        {
            get { return _ownerCorrectness; }
            set
            {
                if (value != _ownerCorrectness)
                {
                    _ownerCorrectness = value;
                    OnPropertyChanged(nameof(OwnerCorrectness));
                }
            }
        }

        private int _cleanlinessRating = 1;
        public int CleanlinessRating
        {
            get { return _cleanlinessRating; }
            set
            {
                if (value != _cleanlinessRating)
                {
                    _cleanlinessRating = value;
                    OnPropertyChanged(nameof(CleanlinessRating));
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(AdditionalComment));
                }
            }
        }

        public ICommand RateAOwnerCommand => new RelayCommand(RateAOwner);
        private void RateAOwner()
        {
            _ownerRatingService.SubmitRating(UnratedOwner, _ownerCorrectness, _cleanlinessRating, _additionalComment, _imageUrl);
            MessageBox.Show("Assessment passed successfully!");
            //NavigationService.Navigate(new UnratedGuestsListView(_logedInUser, NavigationService));
        }

        private string _recommendation;
        public string Recommendation
        {
            get { return _recommendation; }
            set
            {
                if (value != _recommendation)
                {
                    _recommendation = value;
                    OnPropertyChanged(nameof(Recommendation));
                }
            }
        }

        private int _urgencyLevel = 1;
        public int UrgencyLevel
        {
            get { return _urgencyLevel; }
            set
            {
                if (value != _urgencyLevel)
                {
                    _urgencyLevel = value;
                    OnPropertyChanged(nameof(UrgencyLevel));
                }
            }
        }

        public ICommand LeaveARecommendation => new RelayCommand(LeaveRecommendation);
        private void LeaveRecommendation()
        {
            _recommendationService.SubmitRating(UnratedOwner, _urgencyLevel, _recommendation);
            MessageBox.Show("You have successfuly submitted a renovaation recommendation!");
            //NavigationService.Navigate(new UnratedGuestsListView(_logedInUser, NavigationService));
        }
    }
}
