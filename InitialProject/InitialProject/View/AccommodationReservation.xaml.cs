using InitialProject.Domain.Model;
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
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservation : Window
    {

        public Accommodation SelectedAccommodation { get; set; }

        public List<Accommodation> Accommodations { get; set; }

        public ObservableCollection<Accommodation> accommodations { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;

        public List<Accommodation> accommodation { get; set; }
        public List<AccommodationReservation> reservations;
        public User LoggedUser { get; set; }
        public List<DateTime> freeDates { get; set; }

        public AccommodationReservation(Accommodation selectedAccommodation, User user)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedAccommodation = selectedAccommodation;
            Accommodations = new List<Accommodation>();
            Accommodations.Add(SelectedAccommodation);
            reservationDataGrid.ItemsSource = Accommodations;


            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            LoggedUser = user;
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            int guestNumber = int.Parse(tbGuestNumber.Text);

            if (int.Parse(tbGuestNumber.Text) > SelectedAccommodation.MaxGuests)
            {
                MessageBox.Show("The number of guests must be less than the maximum number accepted" +
                    "by the accommodation", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SelectedAccommodation.MaxGuests -= guestNumber;
                _accommodationReservationRepository.SaveReservation(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value, LoggedUser, SelectedAccommodation, guestNumber);
                MessageBox.Show("Reservation successful!");
            }
        }
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tbReservationDays.Text) < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("The number of days for the reservation must not be less than the number" +
                    "of days specifid by the owner!" + "(" + SelectedAccommodation.MinReservationDays + ")", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}