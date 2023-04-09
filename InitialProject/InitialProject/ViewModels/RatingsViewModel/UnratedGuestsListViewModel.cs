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
using InitialProject.View;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class UnratedGuestsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UnratedGuest> UnratedGuests { get; set; }
        private readonly UnratedGuestService _unratedGuestService;
        public UnratedGuestsListViewModel(User logedInUser)
        {
            _unratedGuestService = new UnratedGuestService();
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuestService.GettUnratedGuestsByOwnerId(logedInUser.Id));
        }

        public ICommand OpenRatingWindowCommand => new RelayCommandWithParams(OpenRatingWindow);
        private void OpenRatingWindow(object parameter)
        {
            if (parameter is UnratedGuest selectedGuest)
            {
                // Navigate to the other window passing the selected guest as a parameter
                GuestRatingForm guestRatingForm = new GuestRatingForm(selectedGuest);
                guestRatingForm.Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
