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
using System.Collections.ObjectModel;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        public User LoggedUser { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        public List<Tour> tours;
        public List<Location> locations;
        public Tour SelectedTodayTour { get; set; }

        // DANAS TURE 
        public List<Tour> TodayTours { get; set; }

        public GuideMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedUser = user;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            loadData();                                                  
            ToursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);   
            
            TodayToursDataGrid.ItemsSource = new ObservableCollection<Tour>(TodayTours);
            //UpdateTodayTours();
        }

        private void loadData()
        {
            tours = loadTours();
            locations = LoadLocations();
            AddTourLocation(tours,locations);
            TodayTours = GetTodayTours();
           
        }
        public List<Tour> GetTodayTours()
        {
            var tours = loadTours();
            var locations = LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            AddTourLocation(tours, locations);
            foreach (Tour tour in tours)
            {
                if (tour.Start.Date == DateTime.Today.Date)
                {
                    todayTours.Add(tour);
                }
            }
            return todayTours;
        }
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }

        
        private List<Tour> loadTours() // za svakog vodica da ucita njegove ture
        {
            List<Tour> allTours = new List<Tour>();
            allTours = _tourRepository.GetAll();
            tours = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Guide.Id == LoggedUser.Id)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }
        private void AddTour(object sender, RoutedEventArgs e)
        {
            AddingTour addingTour = new AddingTour(this,LoggedUser);
            addingTour.Show();
            
        }
       

        public void UpdateDataGrid()
        {
            var tours = loadTours();
            var locations  = LoadLocations();
            AddTourLocation(tours, locations);
            ToursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }

        public void UpdateTodayTours()
        {
            var tours = loadTours();
            var locations = LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            AddTourLocation(tours, locations);
            todayTours = GetTodayTours();
            TodayToursDataGrid.ItemsSource = new ObservableCollection<Tour>(todayTours);
        }
       
        
        public void AddTourLocation(List<Tour> tours, List<Location> locations)  //veza lokacije i ture
        {
            foreach (var tour in tours)
            {
                foreach (var location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;

                        break;
                    }
                }
            }
        }

        private void StartTour(object sender, RoutedEventArgs e)
        {
            if (SelectedTodayTour != null)
            {
                StartedTour startedTour = new StartedTour(SelectedTodayTour);
                startedTour.Show();
            }
        }
    }
}
