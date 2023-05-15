using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.View;
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

namespace InitialProject.ViewModels
{
    class PreviousTripViewModel
    {
        public ObservableCollection<UnratedOwner> UnratedOwners { get; set; }
        private readonly UnratedOwnerService _unratedOwnerService;
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        private NavigationService _navigationService { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }


        public PreviousTripViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            //_accommodationReservationService = new AccommodationReservationService();
            // LoadAllReservations();
            //LoggedUser = logedInUser;
            _unratedOwnerService = new UnratedOwnerService();
            UnratedOwners = new ObservableCollection<UnratedOwner>(_unratedOwnerService.GetUnratedOwnerByGuestId(LoggedUser.Id));
        }

        public ICommand RateAccommodationInfoCommand => new RelayCommand(OnRateAccommodation);
        public void OnRateAccommodation()
        {
            _navigationService.Navigate(new AccommodationRatingFormPage(SelectedReservation));
        }

        public ICommand ReviewsFromOwnersInfoCommand => new RelayCommand(OnShowRatings);

        public void OnShowRatings()
        {
            _navigationService.Navigate(new GuestRatingView(_navigationService));
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}


        //private readonly AccommodationReservationService _accommodationReservationService;
        //private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _trip;



        //public ObservableCollection<AccommodationReservation> Trips
        //{
        //    get
        //    {
        //        return _trip;
        //    }

        //    set
        //    {
        //        if (value != _trip)
        //        {
        //            _trip = value;
        //            OnPropertyChanged("Reservations");
        //        }
        //    }
        //}
        // public UnratedOwner UnratedOwner { get; set; }


        //public PreviousTripViewModel(NavigationService navigationService)
        //{
        //    _navigationService = navigationService;
        //    //_accommodationReservationService = new AccommodationReservationService();
        //    // LoadAllReservations();
        //    _unratedOwnerService = new UnratedOwnerService();
        //    UnratedOwners = new ObservableCollection<UnratedOwner>(_unratedOwnerService.GetUnratedOwnerByGuestId(LoggedUser.Id));
        //}

        //private void LoadAllReservations()
        //{
        //    Trips = new ObservableCollection<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationService.GetPastReservations());
        //}
    }
}
