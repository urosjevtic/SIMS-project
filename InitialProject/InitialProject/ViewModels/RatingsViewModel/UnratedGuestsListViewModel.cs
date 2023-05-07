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

        public Navigator Navigator { get; set; }
        public UnratedGuestsListViewModel(User logedInUser, Navigator navigator)
        {
            _unratedGuestService = new UnratedGuestService();
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuestService.GetUnratedGuestsByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
            Navigator = navigator;
        }

        public ICommand OpenRatingWindowCommand => new RelayCommandWithParams(OpenRatingWindow);
        private void OpenRatingWindow(object parameter)
        {
            if (parameter is UnratedGuest selectedGuest)
            {
                Navigator.NavigateTo(new GuestRatingFormView(_logedInUser, selectedGuest, Navigator));
            }
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            Navigator.NavigateTo(new RatingsMainView(_logedInUser, Navigator));
        }



    }
}
