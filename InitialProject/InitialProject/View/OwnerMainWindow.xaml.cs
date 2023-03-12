using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        public List<Accommodation> accommodations;
        public List<Location> locations;
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedInUser = user;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            loadData();
            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);

        }
      
        private void loadData()
        {
            accommodations = loadAccommodations();
            locations = new List<Location>();
            locations = _locationRepository.GetAll();
            foreach(Accommodation accommodation in accommodations)
            {
                foreach(Location location in locations)
                {
                    if(location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }

        private List<Accommodation> loadAccommodations()
        {
            List<Accommodation> allAccommodations = new List<Accommodation>();
            allAccommodations = _accommodationRepository.GetAll();
            accommodations = new List<Accommodation>();
            foreach(Accommodation accommodation in allAccommodations)
            {
                if(accommodation.Owner.Id == LoggedInUser.Id)
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }
        private void Button_Click_AddAccommodation(object sender, RoutedEventArgs e)
        {
            AccommodationRegistration accommodationRegistration = new AccommodationRegistration(this, LoggedInUser);
            accommodationRegistration.Show();
        }

        public void UpdateDataGrid()
        {
            var accommodations = loadAccommodations();
            var locations = _locationRepository.GetAll();

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

            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }
    }
}
