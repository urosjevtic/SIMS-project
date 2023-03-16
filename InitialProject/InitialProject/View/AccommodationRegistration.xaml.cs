﻿using InitialProject.Forms;
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
        private readonly ImageRepository _imageRepository;

        private OwnerMainWindow _ownerMainWindow;

        public User LoggedInUser { get; set; }

        public List<Location> Locations { get; set; }
        public AccommodationRegistration(OwnerMainWindow ownerMainWindow, User user)
        {
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            _ownerMainWindow = ownerMainWindow;
            LoggedInUser = user;
            Locations = new List<Location>();
            Locations = _locationRepository.GetAll();
            InitializeComponent();
            DataContext = this;
            locations = new Dictionary<string, List<string>>();
            updateLocations();
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

        public Dictionary<string, List<string>> locations { get; set; }

        private void updateLocations()
        {
            List<Location> allLocations = _locationRepository.GetAll();

            foreach (Location location in allLocations)
            {
                if (!locations.ContainsKey(location.Country))
                {
                    locations.Add(location.Country, new List<string>());
                }

                locations[location.Country].Add(location.City);
            }
        }


        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                if(value != null)
                {
                    CityComboBox.IsEnabled = true;
                    CityComboBox.ItemsSource = locations[_country];
                }
                else
                {
                    CityComboBox.IsEnabled=false;
                }

                OnPropertyChanged();
            }
            
        }

        private string _city;   

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private Location _location;
        public Location Location
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

        private string _imagesUrl;
        public string ImagesUrl
        {
            get => _imagesUrl;
            set
            {
                if(value != _imagesUrl)
                {
                    _imagesUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        private string[] splitString(string location)
        {
            return location.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
        private int getLocationId(string location)
        {
            string[] splitedLocation = splitString(location);
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

        private int saveImages(string urls, int entityId)
        {
            Model.Image images = new Model.Image();
            images.EntityLd = entityId;
            string[] imagesUrls = splitString(urls);
            foreach(string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }

            return _imageRepository.Save(images).Id;

        }

        private int getLocationId(string country, string city)
        {
            List<Location> allLocations = _locationRepository.GetAll();
            foreach(Location location in allLocations)
            {
                if(location.City == city && location.Country == country)
                {
                    return location.Id;
                }
            }
            throw new Exception("Error has occured");
        }
        private void confirmAccommodationRegistration()
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Owner.Id = LoggedInUser.Id;
            accommodation.Name = _accommodationName;
            accommodation.Location.Id = getLocationId(Country, City);
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
            int imagesId = saveImages(ImagesUrl, 0);
            accommodation.Images.Id = imagesId;
            _accommodationRepository.Save(accommodation);
            _ownerMainWindow.UpdateDataGrid();
            this.Close();
        }

        private void ButtonClickSave(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                confirmAccommodationRegistration();
            }
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Input validation
        private Regex locationPattern = new Regex("^[a-zA-Z\\s]+,[\\s]*[a-zA-Z\\s]+$");
        private Regex positiveNumbersPattern = new Regex("^[1-9][0-9]*$");
        private Regex imagesUrlPattern = new Regex("\\bhttps?://\\S+\\b(?:,\\s*\\bhttps?://\\S+\\b)*");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Select a country";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Select a city";
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
                else if(columnName == "ImagesUrl")
                {
                    if (string.IsNullOrEmpty(ImagesUrl))
                        return "Images are required";
                    Match match = imagesUrlPattern.Match(ImagesUrl);
                    if (!match.Success)
                        return "Images input format should be: url1, url2, ...";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Location", "AccommodationName", "AccommodationTypes", "MaxGuests", "MinReservationDays", "CancelationPeriod", "ImagesUrl", "Country", "City" };

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
