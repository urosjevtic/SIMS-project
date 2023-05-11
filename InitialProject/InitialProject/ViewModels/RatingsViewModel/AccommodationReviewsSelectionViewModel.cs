using InitialProject.Domain.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Service.RatingServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class AccommodationReviewsSelectionViewModel : BaseViewModel
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly SuperOwnerService _superOwnerService;
        private readonly User _logedInUser;
        public NavigationService NavigationService { get; set; }
        public AccommodationReviewsSelectionViewModel(User logedInUser, NavigationService navigationService)
        {
            _accommodationService = new AccommodationService();
            _superOwnerService = new SuperOwnerService();
            _logedInUser = logedInUser;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            NavigationService = navigationService;
        }


        private bool _superOwner;
        public bool IsSuperOwner
        {
            get { return _superOwnerService.IsSuperOwner(_logedInUser.Id); }
            set
            {
                if (value != _superOwner)
                {
                    _superOwner = value;
                    OnPropertyChanged(nameof(IsSuperOwner));
                }
            }
        }



        private double _avrageRating;

        public double AvrageRating
        {
            get { return _superOwnerService.GetAvrageRating(_logedInUser.Id); }
            set
            {
                if (value != _avrageRating)
                {
                    _avrageRating = value;
                    OnPropertyChanged(nameof(AvrageRating));
                }
            }
        }
        public ICommand SeeRatingsCommand => new RelayCommandWithParams(SeeRatings);

        private void SeeRatings(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                // Navigate to the other window passing the selected guest as a parameter
                NavigationService.Navigate(new AccommodationRatingsView(_logedInUser, selectedAccommodation, NavigationService));

            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new RatingsMainView(_logedInUser, NavigationService));
        }

    }
}
