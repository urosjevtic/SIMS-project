using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class TourRequestsStatisticViewModel :INotifyPropertyChanged
    {
        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        public Dictionary<string, List<string>> Locations { get; set; }
        public ICommand SearchCommand { get; private set; } 
        public TourRequestsStatisticViewModel()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            Locations = _locationService.GetCountriesAndCities();
            SearchCommand = new RelayCommand(Search);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

       
        private string _language;

       
        public new string Languagee
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Languagee));
                }
            }
        }


        private void Search()
        {
            List<Tour> tours = new List<Tour>();
            List<Tour> Tours = new List<Tour>();
            List<Tour> allTours = _tourService.GetAll();
            if(_country != null)
            {
                if(_city!= null)
                {
                    tours = FindToursByLocation(_locationService.GetLocationId(_country, _city));
                }
                else
                {
                   tours = FindToursByCountry(_country);
                }
            } 
            if(Languagee != null)
            {
                foreach(Tour tour in tours)
                {
                    if (!tour.Language.Contains(Languagee))
                    {
                       tours.Remove(tour);
                    }
                }
            }
            
        }

        private List<Tour> FindToursByLocation(int locationId)
        {
            List<Tour> allTours = _tourService.GetAll();
            List<Tour> tours = new List<Tour>();
            Location location = _locationService.GetById(locationId);
            foreach (Tour tour in allTours)
            {
                if(tour.Location == location)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        private List<Tour> FindToursByCountry(string country)
        {
            List<Tour> allTours = _tourService.GetAll();
            List<Tour> tours = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Location.Country == country)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

    }
}
