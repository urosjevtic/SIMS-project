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


namespace InitialProject.ViewModels
{
    public class AddingTourViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IImageRepository _imageRepository;
        
        public LocationService _locationService;
        private CheckPointService _checkPointService;   
        private TourService _tourService;
        public Dictionary<string, List<string>> Locations { get; set; }

        private GuideMainViewModel _guideMainWindow;

        public User LoggedUser { get; set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public AddingTourViewModel(User user)
        {
           
            _imageRepository = new ImageRepository();
            
            _checkPointService = new CheckPointService();
            _tourService = new TourService();   
            _guideMainWindow = new GuideMainViewModel(user);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            LoggedUser = user;
            Start = DateTime.Now;
            _locationService = new LocationService();
            Locations = _locationService.GetCountriesAndCities();
            _guideMainWindow.UpdateToursDataGrid();
            _guideMainWindow.UpdateTodayToursDataGrid();
           
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


        private string _checkPoints;
        public string CheckPoints
        {
            get => _checkPoints;
            set
            {
                if (value != _checkPoints)
                {
                    _checkPoints = value;
                    OnPropertyChanged(nameof(CheckPoints));
                }
            }
        }

        public int saveImages(string urls, int entityId)
        {
            Domain.Model.Image images = new Domain.Model.Image();
            images.EntityLd = entityId;
            string[] imagesUrls = SplitString(urls);
            foreach (string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }
            return _imageRepository.ReturnSaved(images).Id;
        }


        private string[] SplitString(string s)
        {
            return s.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public List<CheckPoint> AddCheckPoint(string checkPoints)
        {
            string[] checkPoint = SplitString(checkPoints);
            List<CheckPoint> checkPointsList = new List<CheckPoint>();
            int i = 1;
            foreach (string point in checkPoint)
            {
                checkPointsList.Add(MakeNewCheckPoint(point, i));
                i++;
            }
            return checkPointsList;
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
            _guideMainWindow.LoadData();
            CloseCurrentWindow();
        }

       
        private void Cancel()
        {
            CloseCurrentWindow();
        }

        public ICommand SaveTourCommand => new RelayCommand(Save);


        public void ConfirmAddingTour()
        {
            Tour tour = new Tour();
            tour.Guide.Id = LoggedUser.Id;
            tour.Name = _tourName;
            tour.Location.Id = _locationService.GetLocationId(_country, _city);
            tour.Description = _description;
            tour.Language = _language;
            tour.MaxGuests = Convert.ToInt32(_maxGuests);
            tour.Start = Convert.ToDateTime(_start);
            tour.Duration = Convert.ToInt32(_duration);
            int imagesId = saveImages(_imagesUrl, 0);
            tour.CoverImageUrl.Id = imagesId;
            tour.CheckPoints = AddCheckPoint(_checkPoints);
            tour.IsActive = false;
            _tourService.Save(tour);
            _guideMainWindow.LoadData();
        }
       


    }
}
