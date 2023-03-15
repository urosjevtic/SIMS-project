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
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedUser = user;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            loadData();                                                  //PROBATI ZAKOMENTARISATII
            toursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);   //koje tre proslijedjujem

        }

        private void loadData()
        {
            tours = loadTours();
            locations = new List<Location>();
            locations = _locationRepository.GetAll();
            foreach (Tour tour in tours)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;
                        break;
                    }
                }
            }
        }

        
        private List<Tour> loadTours()
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
            var locations = _locationRepository.GetAll();

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

            toursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }




        
    }
}
