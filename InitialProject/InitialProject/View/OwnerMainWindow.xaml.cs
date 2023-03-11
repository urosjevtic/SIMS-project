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
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        public List<Accommodation> accommodations;
        public List<Location> locations;
        public OwnerMainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            loadData();
            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);

        }
      
        private void loadData()
        {
            accommodations = new List<Accommodation>();
            accommodations = _accommodationRepository.GetAll();
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
        private void Button_Click_AddAccommodation(object sender, RoutedEventArgs e)
        {
            AccommodationRegistration accommodationRegistration = new AccommodationRegistration(this);
            accommodationRegistration.Show();
        }

        public void UpdateDataGrid()
        {
            var accommodations = _accommodationRepository.GetAll();
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
