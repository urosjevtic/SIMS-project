using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    class PreviousTripViewModel : BaseViewModel
    {
        public UnratedOwner SelectedUnratedOwner; /// { get; set; }
        public ObservableCollection<UnratedOwner> UnratedOwners { get; set; }
        private readonly UnratedOwnerService _unratedOwnerService;
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        private NavigationService _navigationService { get; set; }

        //public PreviousTripViewModel()
        //{
        //    _unratedOwnerService = new UnratedOwnerService();
        //    UnratedOwners = new ObservableCollection<UnratedOwner>(_unratedOwnerService.GetAllUnratedOwners();
        //}

        public PreviousTripViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            //_accommodationReservationService = new AccommodationReservationService();
            // LoadAllReservations();
            //LoggedUser = logedInUser;
            _unratedOwnerService = new UnratedOwnerService();
           // UnratedOwners = new ObservableCollection<UnratedOwner>(_unratedOwnerService.GetUnratedOwnerByGuestId(LoggedUser.Id));
        UnratedOwners = new ObservableCollection<UnratedOwner>(_unratedOwnerService.GetAllUnratedOwners());
        }

        public ICommand RateAccommodationInfoCommand => new RelayCommandWithParams(OnRateAccommodation);
        public void OnRateAccommodation(object parameter)
        {
            if(parameter is UnratedOwner selectedOwner)
            _navigationService.Navigate(new AccommodationRatingFormPage(selectedOwner, _navigationService));
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
