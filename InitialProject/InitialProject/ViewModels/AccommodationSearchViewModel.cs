using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    internal class AccommodationSearchViewModel : BaseViewModel
    {
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        private readonly AccommodationRepository _accommodationRepository;

        private NavigationService _navigationService;

        private ObservableCollection<Accommodation> _accommodations;

        private string _accommodationNameSearchInput;
        private string _accommodationTypeSearchInput;
        private string _cityNameSearchInput;
        private string _countryNameSearchInput;
        private string _guestNumberSearchInput;
        private string _reservationDaysSearchInput;

        public ObservableCollection<Accommodation> Accommodations
        {
            get
            {
                return _accommodations;
            }
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged("Accommodations");
                }
            }
        }

        public Accommodation SelectedAccommodation { get; set; }

        public string AccommodationNameSearchInput
        {
            get
            {
                return _accommodationNameSearchInput;
            }
            set
            {
                if (value != _accommodationNameSearchInput)
                {
                    _accommodationNameSearchInput = value;
                    OnPropertyChanged("AccommodationNameSearchInput");
                }
            }
        }

        public string AccommodationTypeSearchInput 
        {
            get
            {
                return _accommodationTypeSearchInput;
            }
            set
            {
                if (value != _accommodationTypeSearchInput)
                {
                    _accommodationTypeSearchInput = value;
                    OnPropertyChanged("AccommodationTypeSearchInput");
                }
            }
        }

        public string CityNameSearchInput
        {
            get
            {
                return _cityNameSearchInput;
            }
            set
            {
                if (value != _cityNameSearchInput)
                {
                    _cityNameSearchInput = value;
                    OnPropertyChanged("CityNameSearchInput");
                }
            }
        }

        public string CountryNameSearchInput
        {
            get
            {
                return _countryNameSearchInput;
            }
            set
            {
                if (value != _countryNameSearchInput)
                {
                    _countryNameSearchInput = value;
                    OnPropertyChanged("CountryNameSearchInput");
                }
            }
        }

        public string GuestNumberSearchInput
        {
            get
            {
                return _guestNumberSearchInput;
            }
            set
            {
                if (value != _guestNumberSearchInput)
                {
                    _guestNumberSearchInput = value;
                    OnPropertyChanged("GuestNumberSearchInput");
                }
            }
        }

        public string ReservationDaysSearchInput
        {
            get
            {
                return _reservationDaysSearchInput;
            }
            set
            {
                if (value != _reservationDaysSearchInput)
                {
                    _reservationDaysSearchInput = value;
                    OnPropertyChanged("ReservationDaysSearchInput");
                }
            }
        }
  

        public ICommand SearchCommand => new RelayCommand(OnSearch);
        public ICommand ResetSearchCommand => new RelayCommand(OnResetSearch);
        public ICommand ShowAccommodationInfoCommand => new RelayCommand(OnShowAccommodationInfo);

        public AccommodationSearchViewModel()
        {
            _accommodationRepository = new AccommodationRepository();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            ResetSearchFields();
        }

        public AccommodationSearchViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _accommodationRepository = new AccommodationRepository();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        private void ResetSearchFields()
        {
            AccommodationNameSearchInput = string.Empty;
            AccommodationTypeSearchInput = string.Empty;
            CityNameSearchInput = string.Empty;
            CountryNameSearchInput = string.Empty;
            GuestNumberSearchInput = string.Empty;
            ReservationDaysSearchInput = string.Empty;
        }

        public void OnSearch()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            List<Accommodation> searchResults = _accommodations.ToList();

            RemoveByLocation(searchResults);
            RemoveByNumbers(searchResults);

            Accommodations = new ObservableCollection<Accommodation>(searchResults);
        }

        public void OnResetSearch()
        {
            ResetSearchFields();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        public List<Accommodation> RemoveByLocation(List<Accommodation> searchResults)
        {
            string[] searchValues = { AccommodationNameSearchInput, CityNameSearchInput, CountryNameSearchInput, AccommodationTypeSearchInput };
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.Concatenate().ToLower().Contains(value.ToLower()));
            return searchResults;
        }
        public List<Accommodation> RemoveByNumbers(List<Accommodation> searchResults)
        {
            int searchGuestNumber = GuestNumberSearchInput == "" ? -1 : Convert.ToInt32(GuestNumberSearchInput);
            int searchDayNumber = ReservationDaysSearchInput == "" ? -1 : Convert.ToInt32(ReservationDaysSearchInput);
            if (searchGuestNumber > 0) searchResults.RemoveAll(x => x.MaxGuests < searchGuestNumber);
            if (searchDayNumber > 0) searchResults.RemoveAll(x => x.MinReservationDays > searchDayNumber);

            return searchResults;
        }

        private void OnShowAccommodationInfo()
        {
            _navigationService.Navigate(new AccommodationReservationPage(SelectedAccommodation));
        }
    }
}
