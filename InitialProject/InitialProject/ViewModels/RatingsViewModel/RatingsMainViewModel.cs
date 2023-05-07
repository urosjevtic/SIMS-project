using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.Ratings;
using Microsoft.VisualBasic.ApplicationServices;
using User = InitialProject.Domain.Model.User;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.View.OwnerView.Renovations;
using System.Windows.Navigation;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class RatingsMainViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        public NavigationService NavigationService { get; set; }
        public RatingsMainViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }


        public ICommand OpenUnratedGuestsCommand => new RelayCommand(OpenUnratedGuests);

        private void OpenUnratedGuests()
        {
            NavigationService.Navigate(new UnratedGuestsListView(_logedInUser, NavigationService));
        }


        public ICommand OpenOwnerRatingsCommand => new RelayCommand(OpenOwnerRatings);

        private void OpenOwnerRatings()
        {
            NavigationService.Navigate(new AccommodationReviewsSelectionView(_logedInUser, NavigationService));
        }




    }
}
