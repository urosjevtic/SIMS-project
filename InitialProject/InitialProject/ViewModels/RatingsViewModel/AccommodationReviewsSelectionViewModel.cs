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
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class AccommodationReviewsSelectionViewModel : BaseViewModel
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public AccommodationReviewsSelectionViewModel(User logedInUser)
        {
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
        }


        public ICommand SeeRatingsCommand => new RelayCommandWithParams(SeeRatings);

        private void SeeRatings(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                // Navigate to the other window passing the selected guest as a parameter
                AccommodationRatings accommodationRatings =
                    new AccommodationRatings(_logedInUser, selectedAccommodation.Id);
                CloseCurrentWindow();
                accommodationRatings.Show();

            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(_logedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }

    }
}
