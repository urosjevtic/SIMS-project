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
using InitialProject.Domain.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        public List<Tour> tours { get; set; }

        public Tour SelectedTour;
        public User LoggedUser { get; set; }
        public SearchResult(List<Tour> filteredTours, User user)
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            tours = filteredTours;
            LoggedUser = user;
            resultDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = (Tour)resultDataGrid.SelectedItem;
            try
            {
                int numberOfPeople = int.Parse(nrOfPeopleTextBox.Text);
                int freeSeats = SelectedTour.MaxGuests - _tourReservationRepository.CountUnreservedSeats(SelectedTour);
                if (numberOfPeople <= freeSeats)
                {
                    _tourReservationRepository.SaveReservation(SelectedTour, numberOfPeople, LoggedUser);
                    MessageBox.Show("Successfully reserved!", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else if (SelectedTour.MaxGuests == _tourReservationRepository.CountUnreservedSeats(SelectedTour))
                {
                    MessageBox.Show("Tour is completely reserved! Now there are shown other tours on this location.", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    FindAlternatives(SelectedTour);
                }
                else
                {
                    MessageBox.Show("There is no enough free seats! Change number of people! Number of free seats: " + freeSeats, "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }

        }
        public void FindAlternatives(Tour tour)
        {
            List<Tour> tours = _tourRepository.FindAllAlternatives(tour);
            resultDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
    }
}

