using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class MakeComplexRequestViewModel : BaseViewModel
    {
        public ICommand AddPartCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand UpButtonCommand { get; private set; }
        public ICommand DownButtonCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        public User LoggedUser { get; set; } = App.LoggedUser;

        public ShortTourRequestService _shortRequestService;
        public ComplexTourRequestService _complexRequestService;
        public List<ShortTourRequest> partsOfRequest { get; set; }
        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }

            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }

            }
        }
        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }

            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }

            }
        }
        private string _nrOfPeople;
        public string NrOfPeople
        {
            get { return _nrOfPeople; }
            set
            {
                if (value != _nrOfPeople)
                {
                    _nrOfPeople = value;
                    OnPropertyChanged(nameof(NrOfPeople));
                }

            }
        }
        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                if (value != _from)
                {
                    _from = value;
                    OnPropertyChanged(nameof(From));
                }

            }
        }
        private string _to;
        public string To
        {
            get { return _to; }
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged(nameof(To));
                }

            }
        }
        public MakeComplexRequestViewModel(NavigationService navService)
        {
            NavigationService = navService;
            AddPartCommand = new RelayCommand(AddPart);
            SubmitCommand = new RelayCommand(Submit);
            GoBackCommand = new RelayCommand(GoBack);
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            _shortRequestService = new ShortTourRequestService();
            _complexRequestService = new ComplexTourRequestService();
            partsOfRequest = new List<ShortTourRequest>();
            NrOfPeople = "0";
        }
        public MakeComplexRequestViewModel(NavigationService navService, List<ShortTourRequest> list)
        {
            NavigationService = navService;
            AddPartCommand = new RelayCommand(AddPart);
            SubmitCommand = new RelayCommand(Submit);
            GoBackCommand = new RelayCommand(GoBack);
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            _shortRequestService = new ShortTourRequestService();
            _complexRequestService = new ComplexTourRequestService();
            partsOfRequest = list;
            NrOfPeople = "0";
        }
        public void AddPart()
        {
            if (Validate())
            {
                ShortTourRequest request = _shortRequestService.Create(LoggedUser, Country, City, Language, NrOfPeople, Description, Convert.ToDateTime(From, CultureInfo.InvariantCulture), Convert.ToDateTime(To, CultureInfo.InvariantCulture));
                partsOfRequest.Add(request);
                MessageBox.Show("Part of complex tour successfully created!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new AddPartOfComplexTour(NavigationService, partsOfRequest));
            }
        }
        public void Submit()
        {
            if (Validate())
            {
                ShortTourRequest request = _shortRequestService.Create(LoggedUser, Country, City, Language, NrOfPeople, Description, Convert.ToDateTime(From, CultureInfo.InvariantCulture), Convert.ToDateTime(To, CultureInfo.InvariantCulture));
                partsOfRequest.Add(request);
                MessageBox.Show("Complex tour successfully created!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                _complexRequestService.SaveComplexRequest(LoggedUser, partsOfRequest);
                GoBack();
            }
        }
        public bool Validate()
        {
            if (int.Parse(NrOfPeople) <= 0)
            {
                MessageBox.Show("Number of people must be greater than zero!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Country == null || Country == "" || City == "" || City == null || Language == "" || Language == null || From == "" || From == null || To == "" || To == null)
            {
                MessageBox.Show("You did not enter all parameters!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Convert.ToDateTime(From, CultureInfo.InvariantCulture) >= Convert.ToDateTime(To, CultureInfo.InvariantCulture))
            {
                MessageBox.Show("From date is bigger than To date!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public void GoBack()
        {
            NavigationService.Navigate(new ShowTourPage(NavigationService));
        }
        private void UpButton()
        {
            int currentNumber = int.Parse(NrOfPeople);
            NrOfPeople = (currentNumber + 1).ToString();
        }
        private void DownButton()
        {
            int currentNumber = int.Parse(NrOfPeople);
            NrOfPeople = (currentNumber - 1).ToString();
        }
    }
}
