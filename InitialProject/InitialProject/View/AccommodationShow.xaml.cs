using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Repository.ReservationRepo;
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
using System.Xaml.Schema;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationShow.xaml
    /// </summary>
    public partial class AccommodationShow : Window
    {
        public User user { get; set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        public List<Accommodation> accommodations;
        public List<Location> locations;

        public User LoggedUser { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationType SelectedType;

        public AccommodationShow(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            LoggedUser = user;
            loadData();
            AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }


        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }


        public void UpdateDataGrid()
        {
            var accommodations = _accommodationRepository.GetAll();
            var locations = LoadLocations();
            AddAccommodationLocation(accommodations, locations);
            AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }

        public void AddAccommodationLocation(List<Accommodation> accommodations, List<Location> locations)   //veza smestaja i lokacije
        {
            foreach (var accommodation in accommodations)
            {
                foreach (var location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }
        private void loadData()
        {
            accommodations = _accommodationRepository.GetAll();
            locations = LoadLocations();
            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearch accommodationSearch = new AccommodationSearch(LoggedUser);
            accommodationSearch.Show();
        }

        private void RatedButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
