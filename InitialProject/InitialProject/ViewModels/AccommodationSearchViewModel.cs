using InitialProject.Domain.Model;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.View;

namespace InitialProject.ViewModels
{
    internal class AccommodationSearchViewModel : BaseViewModel
    {
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        private readonly AccommodationRepository _accommodationRepository;

        private NavigationService _navigationService;

        private ObservableCollection<Accommodation> _accommodations;
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
       
        public ICommand SearchCommand => new RelayCommand(OnSearch);
        public ICommand ShowAccommodationInfoCommand => new RelayCommand(OnShowAccommodationInfo);

        public string AccommodationNameSearchInput { get; set; } = string.Empty;
        public string AccommodationTypeSearchInput { get; set; } = string.Empty;
        public string CityNameSearchInput { get; set; } = string.Empty;
        public string CountryNameSearchInput { get; set; } = string.Empty;
        public string GuestNumberSearchInput { get; set; } = string.Empty;
        public string ReservationDaysSearchInput { get; set; } = string.Empty;


        public AccommodationSearchViewModel()
        {
            _accommodationRepository = new AccommodationRepository();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        public AccommodationSearchViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _accommodationRepository = new AccommodationRepository();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        public void OnSearch()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            List<Accommodation> searchResults = _accommodations.ToList();

            RemoveByLocation(searchResults);
            RemoveByNumbers(searchResults);

            Accommodations = new ObservableCollection<Accommodation>(searchResults);
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
