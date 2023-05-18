using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class RequestsViewModel : BaseViewModel
    {
        private readonly LocationService _locationService;
        private readonly ShortTourRequestService _shortTourRequestService;
        public Dictionary<string, List<string>> Locations { get; set; }
        public User LoggedUser { get; set; }
        public ObservableCollection<ShortTourRequest> Requests { get; set; }
        public ObservableCollection<ShortTourRequest> SearchResult { get; set; }
        public ShortTourRequest SelectedRequest { get; set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AcceptRequestCommand { get; private set; }  
        public RequestsViewModel(User user)
        {
            _shortTourRequestService = new ShortTourRequestService();
            _locationService = new LocationService();
            SearchCommand = new RelayCommand(Search);
            AcceptRequestCommand = new RelayCommand(AcceptRequest);
            LoggedUser = user;
            Locations = _locationService.GetCountriesAndCities();
            Requests = new ObservableCollection<ShortTourRequest>(_shortTourRequestService.GetAll());
            SearchResult = new ObservableCollection<ShortTourRequest>();
            ResetCommand = new RelayCommand(ResetSearch);
            From = DateTime.Now;
            To = DateTime.Now;
        }

        private void ResetSearch()
        {
            Requests.Clear();
            foreach(ShortTourRequest request in _shortTourRequestService.GetAll())
            {
                Requests.Add(request);
            }
            Language = "";
            MaxGuests = -1;
            From = DateTime.Now.Date;
            To = DateTime.Now.Date;
        }

        private void AcceptRequest()
        {
            if(SelectedRequest != null)
            {
                AddingTour addingTourWindow = new AddingTour(LoggedUser, SelectedRequest, false);
                addingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("");
            }
        }


        private string _country="";
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
        private string _city ="";
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _language = "";
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }
        private int _maxGuests = -1;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }
        private DateTime _from;
        public DateTime From
        {
            get => _from;
            set
            {
                if (value != _from)
                {
                    _from = value;
                    OnPropertyChanged(nameof(From));
                }
            }
        }

        private DateTime _to;
        public DateTime To
        {
            get => _to;
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged(nameof(To));
                }
            }
        }

        private void Search()
        {
            SearchResult.Clear();
            ShortTourRequest shortTourRequest = new ShortTourRequest(Country, City, Language,MaxGuests,From,To);
            foreach (ShortTourRequest request in _shortTourRequestService.GetAll())
            {
                if (IsCountryValid(request,shortTourRequest) 
                    && IsCityValid(request,shortTourRequest) 
                    && IsLanguageValid(request,shortTourRequest)
                    && IsGuestNumberValid(request,shortTourRequest))
                {
                    if (To.DayOfYear >= From.DayOfYear)
                    {
                        if (IsAppropriateDateRange(request,shortTourRequest))
                        {
                            SearchResult.Add(request);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Date range is not valid!");
                        break;
                    }
                }                

            }
            Requests.Clear();
            foreach(ShortTourRequest request1 in SearchResult)
            {
                Requests.Add(request1);
            } 
        }

        private bool IsCountryValid(ShortTourRequest request, ShortTourRequest shortTourRequest)
        {
            return (request.Country.Equals(shortTourRequest.Country) || shortTourRequest.Country.Equals(""));
        }

        private bool IsCityValid(ShortTourRequest request, ShortTourRequest shortTourRequest)
        {
            return (request.City.Equals(shortTourRequest.City) || shortTourRequest.City.Equals(""));
        }

        private bool IsLanguageValid (ShortTourRequest request, ShortTourRequest shortTourRequest)
        {
            return (request.Language.ToLower().Contains(shortTourRequest.Language.ToLower()) || shortTourRequest.Language.Equals(""));
        }
        private bool IsGuestNumberValid(ShortTourRequest request, ShortTourRequest shortTourRequest)
        {
            return (request.NumberOfPeople >= shortTourRequest.NumberOfPeople || shortTourRequest.NumberOfPeople == -1);
        }
        private bool IsAppropriateDateRange(ShortTourRequest request, ShortTourRequest shortTourRequest)
        {
            return ((request.From.DayOfYear >= shortTourRequest.From.DayOfYear) && (request.To.DayOfYear <= shortTourRequest.To.DayOfYear) || (shortTourRequest.From.DayOfYear == DateTime.Now.DayOfYear && shortTourRequest.To.DayOfYear == DateTime.Now.DayOfYear));
        }

    }
}
