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
            //SelectedTour = null;
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
            try
            {
                int numberOfPeople = int.Parse(nrOfPeopleTextBox.Text);
                if(numberOfPeople < (SelectedTour.MaxGuests - _tourReservationRepository.CountUnreservedSeats(SelectedTour)))
                {
                    _tourReservationRepository.SaveReservation(SelectedTour, numberOfPeople, LoggedUser);
                    MessageBox.Show("Successfully reserved!", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }else if(SelectedTour.MaxGuests == _tourReservationRepository.CountUnreservedSeats(SelectedTour))
                {
                    MessageBox.Show("Tour is completely reserved! Now there are shown other tours on this location.", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    FindAlternatives(SelectedTour);
                }
                else
                {
                    MessageBox.Show("There is no enough free seats! Change number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
   
            }
            catch (Exception ex)
            {
                if (SelectedTour == null)
                {
                    MessageBox.Show("You did not select any tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (nrOfPeopleTextBox.Text == String.Empty)
                {
                    MessageBox.Show("You did not type number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (int.Parse(nrOfPeopleTextBox.Text) > SelectedTour.MaxGuests)
                {
                    MessageBox.Show("There is no enough free seats! Change number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
        public void FindAlternatives(Tour tour)
        {
            List<Tour> tours = _tourRepository.FindAllAlternatives(tour);
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
    }
}
