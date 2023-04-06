using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using InitialProject.Domain.Model;

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

        public Dictionary<string, List<string>> Locations { get; set; }
        private User LoggedInUser { get; set; }

        public AccommodationRegistration(OwnerMainWindow ownerMainWindow, User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            _ownerMainWindow = ownerMainWindow;
            LoggedInUser = user;

            Locations = new Dictionary<string, List<string>>();
            LoadLocations();
        }

        private void LoadLocations()
        {
            List<Location> allLocations = _locationRepository.GetAll();

            foreach (Location location in allLocations)
            {
                if (!Locations.ContainsKey(location.Country))
                {
                    Locations.Add(location.Country, new List<string>());
                }

                Locations[location.Country].Add(location.City);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private string _accommodationName;
        public string AccommodationName
        {
            get { return _accommodationName; }
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                if (value != null)
                {
                    CityComboBox.IsEnabled = true;
                    CityComboBox.ItemsSource = Locations[_country];
                }
                else
                {
                    CityComboBox.IsEnabled = false;
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
            get { return _maxGuests; }
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minimumReservationDays;
        public string MinReservationDays
        {
            get { return _minimumReservationDays; }
            set
            {
                if (value != _minimumReservationDays)
                {
                    _minimumReservationDays = value;
                    OnPropertyChanged();
                }
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }



        private void RegistrateNewAccommodation()
        {
            Accommodation accommodation = new Accommodation();
            CreateNewAccommodation(accommodation);
            _accommodationRepository.Save(accommodation);
        }

        private void CreateNewAccommodation(Accommodation accommodation)
        {
            accommodation.Owner.Id = LoggedInUser.Id;
            accommodation.Name = _accommodationName;
            accommodation.Location.Id = GetLocationId(Country, City);
            accommodation.Type = GetType();
            accommodation.MaxGuests = Convert.ToInt32(_maxGuests);
            accommodation.MinReservationDays = Convert.ToInt32(_minimumReservationDays);
            accommodation.CancelationPeriod = Convert.ToInt32(_cancelationPeriod);
            SaveImages(_imagesUrl, 0);
            accommodation.Images.Id = GetImagesId(_imagesUrl);
        }

        private AccommodationType GetType()
        {
            switch (_accommodationType)
            {
                case "house":
                    return AccommodationType.house;
                case "cabin":
                    return AccommodationType.cabin;
                case "appartment":
                    return AccommodationType.appartment;
                default:
                    throw new Exception("Error has occured");
            }
        }

        private int GetLocationId(string country, string city)
        {
            List<Location> allLocations = _locationRepository.GetAll();
            foreach (Location location in allLocations)
            {
                if (location.City == city && location.Country == country)
                {
                    return location.Id;
                }
            }
            throw new Exception("Error has occured");
        }

        private void SaveImages(string urls, int entityId)
        {
            Image images = new Image();
            images.EntityLd = entityId;
            string[] imagesUrls = SplitStringByComma(urls);
            foreach (string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }

            _imageRepository.ReturnSaved(images);

        }
        private int GetImagesId(string urls)
        {
            List<Image> allImages = _imageRepository.GetAll();
            string[] imageUrls = SplitStringByComma(urls);
            foreach (Image image in allImages)
            {
                if (image.Url.SequenceEqual(imageUrls))
                {
                    return image.Id;
                }
            }
            throw new Exception("Error has occured");
        }

        private string[] SplitStringByComma(string str)
        {
            return str.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                RegistrateNewAccommodation();
                _ownerMainWindow.UpdateAccommodations();
                this.Close();
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Input validation
        private readonly Regex _positiveNumbersPattern = new Regex("^[1-9][0-9]*$");
        private readonly Regex _imagesUrlPattern = new Regex("\\bhttps?://\\S+\\b(?:,\\s*\\bhttps?://\\S+\\b)*");
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
                    Match match = _positiveNumbersPattern.Match(MaxGuests);
                    if (!match.Success)
                        return "Maximum guests format should be: positive number";
                }
                else if (columnName == "MinReservationDays")
                {
                    if (string.IsNullOrEmpty(MinReservationDays))
                        return "Minimum reservation days is required";
                    Match match = _positiveNumbersPattern.Match(MinReservationDays);
                    if (!match.Success)
                        return "Minimum reservation days format should be: positive number";
                }
                else if (columnName == "CancelationPeriod")
                {
                    if (string.IsNullOrEmpty(CancelationPeriod))
                        return "Cancelation period is required";
                    Match match = _positiveNumbersPattern.Match(CancelationPeriod);
                    if (!match.Success)
                        return "Cancelation period format should be: positive number (number of days for cancelation)";
                }
                else if (columnName == "ImagesUrl")
                {
                    if (string.IsNullOrEmpty(ImagesUrl))
                        return "Images are required";
                    Match match = _imagesUrlPattern.Match(ImagesUrl);
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
