using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class UnratedGuestsListViewModel : BaseViewModel
    {
        public ObservableCollection<UnratedGuest> UnratedGuests { get; set; }
        private readonly UnratedGuestService _unratedGuestService;
        private readonly User _logedInUser;
        public UnratedGuestsListViewModel(User logedInUser)
        {
            _unratedGuestService = new UnratedGuestService();
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuestService.GetUnratedGuestsByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
        }

        public ICommand OpenRatingWindowCommand => new RelayCommandWithParams(OpenRatingWindow);
        private void OpenRatingWindow(object parameter)
        {
            if (parameter is UnratedGuest selectedGuest)
            {
                // Navigate to the other window passing the selected guest as a parameter
                GuestRatingForm guestRatingForm = new GuestRatingForm(_logedInUser, selectedGuest);
                CloseCurrentWindow();
                guestRatingForm.Show();
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
