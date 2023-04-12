using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Utilities;
using System.Windows.Input;
using System.Windows;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels
{
    public class AccommodationRegistrationViewModel : BaseViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        private readonly User _logedInUser;
        public AccommodationRegistrationViewModel(User logedInUser)
        {
            _accommodationService = new AccommodationService();
            _locationService = new LocationService();

            Locations = _locationService.GetCountriesAndCities();
            _logedInUser = logedInUser;
        }
        public Dictionary<string, List<string>> Locations { get; set; }



        private string _accommodationName;
        public string AccommodationName
        {
            get { return _accommodationName; }
            set
            {
                _accommodationName = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                Cities = Locations[_country];
                OnPropertyChanged(nameof(Country));
            }
        }

        private IEnumerable<string> _cities;
        public IEnumerable<string> Cities
        {
            get { return _cities; }
            set
            {
                _cities = Locations[Country];
                OnPropertyChanged(nameof(Cities));
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        private string _accommodationTypes;
        public string AccommodationTypes
        {
            get { return _accommodationTypes; }
            set
            {
                _accommodationTypes = value;
                OnPropertyChanged(nameof(AccommodationTypes));
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        private int _minReservationDays;
        public int MinReservationDays
        {
            get { return _minReservationDays; }
            set
            {
                _minReservationDays = value;
                OnPropertyChanged(nameof(MinReservationDays));
            }
        }
        private string _cancelationPeriod = "1";
        public string CancelationPeriod
        {
            get { return _cancelationPeriod; }
            set
            {
                if (value != _cancelationPeriod)
                {
                    _cancelationPeriod = value;
                    OnPropertyChanged(nameof(CancelationPeriod));
                }
            }
        }

        private string _imagesUrl;
        public string ImagesUrl
        {
            get { return _imagesUrl; }
            set
            {
                if (value != _imagesUrl)
                {
                    _imagesUrl = value;
                    OnPropertyChanged(nameof(ImagesUrl));
                }
            }
        }

        public ICommand ConfirmRegistrationCommand => new RelayCommand(ConfirmRegistration);

        private void ConfirmRegistration()
        {
            _accommodationService.CreateAccommodation
                (_accommodationName, _country, _city, _accommodationTypes, _maxGuests, _minReservationDays, Convert.ToInt32(_cancelationPeriod), _imagesUrl, _logedInUser.Id);

            MyAccommodationsMainWindow myAccommodationsMain = new MyAccommodationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsMain.Show();
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            MyAccommodationsMainWindow myAccommodationsMain = new MyAccommodationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsMain.Show();
        }

    }
}
