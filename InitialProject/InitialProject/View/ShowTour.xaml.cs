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
        public List<Tour> tours;
        public List<Location> locations;
        public Tour SelectedTour;
        public User LoggedUser { get;set; }
        public ShowTour()
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = null;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            loadData();
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }
        public void UpdateDataGrid()
        {
            var tours = _tourRepository.GetAll();
            var locations = LoadLocations();
            AddTourLocation(tours, locations);
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
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
        public void SearchUpdateDataGrid(List<Tour> tour)
        {
            tourDataGrid.Items.Clear();
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tour);
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
            TourSearch tourSearch = new TourSearch();
            tourSearch.Show();
        }

        private void ReserveButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = (Tour)tourDataGrid.SelectedItem;
            ТоurReservation reservation = new ТоurReservation();

            if (SelectedTour == null)
            {
                MessageBox.Show("You did not select any tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (nrOfPeopleTextBox.Text == String.Empty)
            {
                MessageBox.Show("You did not type number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (int.Parse(nrOfPeopleTextBox.Text) > SelectedTour.MaxGuests - 1)
            {
                MessageBox.Show("There is no enough free seats! Change number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
