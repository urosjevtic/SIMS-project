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

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class RatingsMainViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        public Navigator Navigator { get; set; }
        public RatingsMainViewModel(User logedInUser, Navigator navigator)
        {
            _logedInUser = logedInUser;
            Navigator = navigator;
        }


        public ICommand OpenUnratedGuestsCommand => new RelayCommand(OpenUnratedGuests);

        private void OpenUnratedGuests()
        {
            Navigator.NavigateTo(new UnratedGuestsListView(_logedInUser, Navigator));
        }


        public ICommand OpenOwnerRatingsCommand => new RelayCommand(OpenOwnerRatings);

        private void OpenOwnerRatings()
        {
            Navigator.NavigateTo(new AccommodationReviewsSelectionView(_logedInUser, Navigator));
        }




    }
}
