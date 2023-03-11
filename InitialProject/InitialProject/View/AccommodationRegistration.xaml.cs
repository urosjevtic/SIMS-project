using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistration.xaml
    /// </summary>
    public partial class AccommodationRegistration : Window, IDataErrorInfo
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        private OwnerMainWindow _ownerMainWindow;       
        public AccommodationRegistration(OwnerMainWindow ownerMainWindow)
        {
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository(); 
            _ownerMainWindow = ownerMainWindow;
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accommodationType;
        public string AccommodationTypes
        {
            get { return _accommodationType; }
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancelationPeriod;
        public string CancelationPeriod
        {
            get => _cancelationPeriod;
            set
            {
                if (value != _cancelationPeriod)
                {
                    _cancelationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }


        private int getLocationId(string location)
        {
            string[] splitedLocation = splitLocation(location);
            List<Location> locations = _locationRepository.GetAll();
            foreach(Location loc in locations)
            {
                if(loc.Country == splitedLocation[0])
                {
                    if (loc.City == splitedLocation[1])
                        return loc.Id;
                }
            }

            Location newLocation = new Location();
            newLocation.Country = splitedLocation[0];
            newLocation.City = splitedLocation[1];
            return _locationRepository.Save(newLocation).Id;
        }

        private string[] splitLocation(string location)
        {
            return location.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void confirmAccommodationRegistration()
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Name = _accommodationName;
            accommodation.Location.Id = getLocationId(_location);
            switch (_accommodationType)
            {
                case "house":
                    accommodation.Type = AccommodationType.house;
                    break;
                case "cabin":
                    accommodation.Type = AccommodationType.cabin;
                    break;
                case "appartment":
                    accommodation.Type = AccommodationType.appartment;
                    break;
            }
            accommodation.MaxGuests = Convert.ToInt32(_maxGuests);
            accommodation.MinReservationDays = Convert.ToInt32(_minReservationDays);
            accommodation.CancelationPeriod = Convert.ToInt32(_cancelationPeriod);
            _accommodationRepository.Save(accommodation);
            _ownerMainWindow.UpdateDataGrid();
            this.Close();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                confirmAccommodationRegistration();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Input validation
        private Regex locationPattern = new Regex("^[a-zA-Z\\s]+,[\\s]*[a-zA-Z\\s]+$");
        private Regex positiveNumbersPattern = new Regex("^[1-9][0-9]*$");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Location is required";
                    Match match = locationPattern.Match(Location);
                    if (!match.Success)
                        return "Location format should be: Country, City";
                }
                else if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "Accommodation name is required";
                }
                else if (columnName == "AccommodationTypes")
                {
                    if (string.IsNullOrEmpty(AccommodationTypes))
                        return "Accommodation type is required";
                }
                else if (columnName == "MaxGuests")
                {
                    if (string.IsNullOrEmpty(MaxGuests))
                        return "Maximum number of guests is required";
                    Match match = positiveNumbersPattern.Match(MaxGuests);
                    if (!match.Success)
                        return "Maximum guests format should be: positive number";
                }
                else if (columnName == "MinReservationDays")
                {
                    if (string.IsNullOrEmpty(MinReservationDays))
                        return "Minimum reservation days is required";
                    Match match = positiveNumbersPattern.Match(MinReservationDays);
                    if (!match.Success)
                        return "Minimum reservation days format should be: positive number";
                }
                else if (columnName == "CancelationPeriod")
                {
                    if (string.IsNullOrEmpty(CancelationPeriod))
                        return "Cancelation period is required";
                    Match match = positiveNumbersPattern.Match(CancelationPeriod);
                    if (!match.Success)
                        return "Cancelation period format should be: positive number (number of days for cancelation)";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Location" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

    }
}
