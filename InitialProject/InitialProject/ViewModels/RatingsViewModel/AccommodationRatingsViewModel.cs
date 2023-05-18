using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RatingsViewModel
{
    class AccommodationRatingsViewModel : BaseViewModel
    {

        public ObservableCollection<RatedOwner> Ratings { get; set; }
        private readonly RatedOwnerService _ratedOwnerService;
        private readonly User _logedInUser;
        public Accommodation Accommodation { get; }

        public NavigationService NavigationService { get; set; }

        public AccommodationRatingsViewModel(User logedInUser, Accommodation accommodation, NavigationService navigationService)
        {
            _ratedOwnerService = new RatedOwnerService();
            Accommodation = accommodation;
            Ratings = new ObservableCollection<RatedOwner>(_ratedOwnerService.GetFilteredRatingsByAccommodationId(Accommodation.Id));
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new AccommodationReviewsSelectionView(_logedInUser, NavigationService));
        }

    }
}
