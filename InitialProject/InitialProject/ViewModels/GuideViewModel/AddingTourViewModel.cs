using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.ViewModel;
using InitialProject.Domain.RepositoryInterfaces;
using System.Windows.Controls;
using System.Windows.Input;
using InitialProject.Utilities;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using InitialProject.ViewModels.MainVeiwModel;

namespace InitialProject.ViewModels
{
    public class AddingTourViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IImageRepository _imageRepository;
        
        public LocationService _locationService;
        private CheckPointService _checkPointService;
        private readonly ShortTourRequestService _shortTourRequestService;
        private TourService _tourService;
        private NotificationService _notificationService;
        public Dictionary<string, List<string>> Locations { get; set; }

        public User LoggedUser { get; set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }   
        public ICommand AddDateTimeCommand { get; private set; }    
        public ObservableCollection<DateTime> StartDates { get; set; }
        public string AllUrls { get; set; }
        public ShortTourRequest Request { get; set;}
  
      
        public AddingTourViewModel(User user,ShortTourRequest request, bool enter)
        {
           
            _imageRepository = new ImageRepository();
            _shortTourRequestService = new ShortTourRequestService();
            _checkPointService = new CheckPointService();
            _tourService = new TourService();  
            _notificationService = new NotificationService();
            //_guideMainWindow = new GuideMainViewModel(user);

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            AddImageCommand = new RelayCommand(AddImage);
            AddDateTimeCommand = new RelayCommand(AddStartDate);
            LoggedUser = user;
            Start = DateTime.Now;
            _locationService = new LocationService();
            Locations = _locationService.GetCountriesAndCities();
            StartDates = new ObservableCollection<DateTime>();
            Languagee = _shortTourRequestService.GetMostWantedLanguage();   
            Country = _shortTourRequestService.GetMostWantedLocation().Country;
            City = _shortTourRequestService.GetMostWantedLocation().City;
            Request = request;
            MakeTourFromRequest(Request);

            if (enter)
            {
                MessageBox.Show("Dati su prrijedlozi za najtrazeniju lokaciju i jezik!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void MakeTourFromRequest(ShortTourRequest request)
        {
            if(request.Language == null)
            {
                return;
            }
            else
            {
                Country = request.Country;
                City = request.City;
                Languagee = request.Language;
                MaxGuests = request.NumberOfPeople;
                Description = request.Description;
                Start = request.From;
                request.Status = RequestStatus.Accepted;
                _shortTourRequestService.Update(request);
            }
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _tourName;
        public string Namee
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(Namee));
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
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
        private int _maxGuests;
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
        private DateTime _start;
        public DateTime Start
        {
            get => _start;
            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string _imagesUrl;
        public string ImagesUrl
        {
            get => _imagesUrl;
            set
            {
                if (value != _imagesUrl)
                {
                    _imagesUrl = value;
                    OnPropertyChanged(nameof(ImagesUrl));
                }
            }
        }


        private string _first;
        public string First
        {
            get => _first;
            set
            {
                if (value != _first)
                {
                    _first = value;
                    OnPropertyChanged(nameof(First));
                }
            }
        }
        private string _last;
        public string Last
        {
            get => _last;
            set
            {
                if (value != _last)
                {
                    _last = value;
                    OnPropertyChanged(nameof(Last));
                }
            }
        }
        private string _other;
        public string Other
        {
            get => _other;
            set
            {
                if (value != _other)
                {
                    _other = value;
                    OnPropertyChanged(nameof(Other));
                }
            }
        }

        

        public List<CheckPoint> MakeCheckPointList()
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            checkPoints.Add(MakeNewCheckPoint(First,1));
            int i = 2;
            string[] checkPoint = SplitString(Other);
            foreach (string point in checkPoint)
            {
                checkPoints.Add(MakeNewCheckPoint(point, i));
                i++;
            }
            checkPoints.Add(MakeNewCheckPoint(Last, i));
            return checkPoints;
        }

        private string[] SplitString(string s)
        {
            return s.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }

      

        public CheckPoint MakeNewCheckPoint(string point, int i)
        {
            CheckPoint checkPoint = new CheckPoint();
            checkPoint.Name = point;
            checkPoint.SerialNumber = i;
            checkPoint.IsChecked = false;
            checkPoint.CurrentGuests = new List<User>();
            return _checkPointService.Save(checkPoint);

        }
        private void Save()
        {
            ConfirmAddingTour();
            //_guideMainWindow.LoadData();
            CloseCurrentWindow();
        }
        private Domain.Model.Image MakeNewImage(Tour tour)
        {
            string[] urls = SplitString(ImagesUrl);
            Domain.Model.Image image = new Domain.Model.Image();
            image.Url = new List<string>();
            image.EntityLd = tour.Id;

            foreach (string url in urls)
            {
                image.Url.Add(url);
            }
            _imageRepository.Save(image);
            return image;   
        }


        private void Cancel()
        {
            CloseCurrentWindow();
        }

        public void AddStartDate()
        {
            DateTime newTourEnd = _start.AddHours(_duration);
            foreach (Tour tour in _tourService.GetAll())
            {
                foreach(DateTime start in tour.StartDates)
                {
                    DateTime end = start.AddHours(tour.Duration);
                    if ((_start >= start && _start < end) || (newTourEnd > start && newTourEnd <= end))
                    {
                        MessageBox.Show("Vodic ima zakazanu turu u tom periodu!");
                        return;
                    }
                }
            }
            StartDates.Add(_start);
        } 
        public void ConfirmAddingTour()
        {
            List<DateTime> dates = new List<DateTime>();
            foreach(DateTime date in StartDates)
            {
                dates.Add(date);
            }
            Tour tour = new Tour();
            tour.Guide.Id = LoggedUser.Id;
            tour.Name = _tourName;
            tour.Location.Id = _locationService.GetLocationId(_country, _city);
            tour.Description = _description;
            tour.Language = _language;
            tour.StartDates = dates;
            tour.MaxGuests = Convert.ToInt32(_maxGuests);
            tour.Duration = Convert.ToInt32(_duration);
            tour.CoverImageUrl = MakeNewImage(tour);
            tour.CheckPoints = MakeCheckPointList();
            tour.IsActive = false;
        
            _tourService.Save(tour);
            _notificationService.SendNotifications(tour);

        }

      

        private void AddImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
          
            op.Multiselect = true;  
            if(op.ShowDialog() == true)
            {
                int i = 0;
                foreach (var imageUrl in op.FileNames)
                {
                    if (i == 1)
                    {
                        AllUrls += imageUrl;
                    }
                    else
                    {
                        AllUrls += "," + imageUrl;
                    }
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    image.Source = new BitmapImage(new Uri(imageUrl));
                    i++;
                }
            }
        }
    }
}
