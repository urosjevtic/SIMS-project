using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AddingTour.xaml
    /// </summary>
    public partial class AddingTour : Window
    {


        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;
        private readonly CheckPointRepository _checkPointRepository;

        private GuideMainWindow _guideMainWindow;

        
        public User LoggedUser { get; set; }
        public AddingTour(GuideMainWindow guideMainWindow, User user)

        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            _checkPointRepository = new CheckPointRepository();
            _guideMainWindow = guideMainWindow;
            LoggedUser = user;
            Start = DateTime.Now;
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                if (value != _imagesUrl)
                {
                    _imagesUrl = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        
        private int saveImages(string urls, int entityId)
        {
            Model.Image images = new Model.Image();
            images.EntityLd = entityId;
            string[] imagesUrls = SplitString(urls);  
            foreach (string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }
            return _imageRepository.Save(images).Id;
        }

        private string[] SplitString(string s)
        {
            return s.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        private List<CheckPoint> AddCheckPoint(string checkPoints)
        {
            string[] checkPoint = SplitString(checkPoints);
            List<CheckPoint> checkPointsList = new List<CheckPoint>();
            int i = 1;
            foreach(string point in checkPoint)
            {
                checkPointsList.Add(MakeNewCheckPoint(point, i));
                i++;
            }
            return checkPointsList;
        }

        private CheckPoint MakeNewCheckPoint(string point, int i)
        {
            CheckPoint checkPoint = new CheckPoint();   
            checkPoint.Name = point;
            checkPoint.SerialNumber = i;
            checkPoint.IsChecked = false;
            checkPoint.CurrentGuests = new List<User>();
            return _checkPointRepository.Save(checkPoint);

        }

       
        private void confirmAddingTour()
        {
            Tour tour = new Tour();
            tour.Guide.Id = LoggedUser.Id;
            tour.Name = _tourName;
            tour.Location.Id = _locationRepository.GetLocationId(_location);
            tour.Description = _description;
            tour.Language = _language;
            tour.MaxGuests = Convert.ToInt32(_maxGuests);
            tour.Start = Convert.ToDateTime(_start);
            tour.Duration = Convert.ToInt32(_duration);
            int imagesId = saveImages(_imagesUrl, 0);
            tour.CoverImageUrl.Id = imagesId;
            tour.CheckPoints = AddCheckPoint(_checkPoints);
            tour.IsActive = false;
            _tourRepository.Save(tour);
            _guideMainWindow.UpdateToursDataGrid();
            _guideMainWindow.UpdateTodayToursDataGrid();
            this.Close();
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            confirmAddingTour();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
