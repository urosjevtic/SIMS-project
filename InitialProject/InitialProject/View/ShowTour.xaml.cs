using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ShowTour.xaml
    /// </summary>
    public partial class ShowTour : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        public List<Tour> tours;
        public List<Location> locations;
        public Tour SelectedTour { get; set; }
        public User LoggedUser { get;set; }
        public ShowTour(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _tourReservationRepository = new TourReservationRepository();
            LoggedUser = user;
            loadData();
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }
        private void loadData()
        {
            tours = _tourRepository.GetAll();
            locations = LoadLocations();
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

      
        private void OpenSearchButtonClick(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }
    }
}
