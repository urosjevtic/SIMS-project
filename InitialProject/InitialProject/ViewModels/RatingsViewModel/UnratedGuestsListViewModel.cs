using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
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

        public NavigationService NavigationService { get; set; }
        public UnratedGuestsListViewModel(User logedInUser, NavigationService navigationService)
        {
            _unratedGuestService = new UnratedGuestService();
            
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuestService.GetUnratedGuestsByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }



        public ICommand OpenRatingWindowCommand => new RelayCommandWithParams(OpenRatingWindow);
        private void OpenRatingWindow(object parameter)
        {
            if (parameter is UnratedGuest selectedGuest)
            {
                NavigationService.Navigate(new GuestRatingFormView(_logedInUser, selectedGuest, NavigationService));
            }
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new RatingsMainView(_logedInUser, NavigationService));
        }



    }
}
