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
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservation : Window
    {

        public Accommodation SelectedAccommodation { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Accommodation>accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public List<Accommodation>accommodation { get; set; }
        public List<AccommodationReservation> dates { get; set; }
        public User LoggedUser { get; set; }
        public AccommodationReservation(Accommodation selectedAccommodation)
        {
            InitializeComponent();
           this.DataContext=this;
            SelectedAccommodation = selectedAccommodation;
            Accommodations = new List<Accommodation>();
            Accommodations.Add(SelectedAccommodation);
            reservationDataGrid.ItemsSource= Accommodations;
        }


        public AccommodationReservation(List<AccommodationReservation>filteredDates, User user)
        {
            InitializeComponent();
            _accommodationRepository= new AccommodationRepository();
            _accommodationReservationRepository=new AccommodationReservationRepository();
            dates = filteredDates;
            LoggedUser=user;
            resultDataGrid.ItemsSource= new ObservableCollection<AccommodationReservation>(dates);
        }


        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            int guestNumber = int.Parse(tbGuestNumber.Text);
            
            if (int.Parse(tbReservationDays.Text) < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("The number of days for the reservation must not be less than the number" +
                    "of days specifid by the owner!"+"("+SelectedAccommodation.MinReservationDays+")", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (int.Parse(tbGuestNumber.Text) > SelectedAccommodation.MaxGuests)
            {
                MessageBox.Show("The number of guests must be less than the maximum number accepted" +
                    "by the accommodation", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _accommodationReservationRepository.SaveReservation(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value, LoggedUser, SelectedAccommodation, guestNumber);
        }

       // private void rezervisani

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

/*
 if(guestsNumber > 0 && SelectedTour != null)
            {
                int tourSpotsLeft = SelectedTour.SpotsLeft;

                if (tourSpotsLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    DisplaySimilarTours(SelectedTour);
                }
                else if(tourSpotsLeft < guestsNumber)
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourSpotsLeft.ToString());
                }
                else
                {
                    SelectedTour.SpotsLeft -= guestsNumber;
                    var tourReservation = new TourReservation(LoggedInUser.Id, SelectedTour.Id, guestsNumber);
                    reservationRepository.Save(tourReservation);
                    MessageBox.Show("Reservation is successful");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number and select a tour\n" +
                                "in order to make a reservation!");
            }
        }
 




*/