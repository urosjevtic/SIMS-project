using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
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

        public AccommodationRatingsViewModel(User logedInUser, int accommodationId)
        {
            _ratedOwnerService = new RatedOwnerService();
            Ratings = new ObservableCollection<RatedOwner>(_ratedOwnerService.GetByAccommodationId(accommodationId));
            _logedInUser = logedInUser;
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            AccommodationReviewsSelectionWindow reviewsSelection = new AccommodationReviewsSelectionWindow(_logedInUser);
            CloseCurrentWindow();
            reviewsSelection.Show();
        }

    }
}
