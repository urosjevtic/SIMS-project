using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.ViewModels;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationPage.xaml
    /// </summary>
    public partial class AccommodationReservationPage : Page
    {
       
        public AccommodationReservationPage(Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationReservationViewModel(accommodation);
          //  Accommodations = new List<Accommodation>();
            SelectedAccommodation = accommodation;
           // Accommodations.Add(SelectedAccommodation);

            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            //_accommodationReservationRepository = new AccommodationReservationRepository();
            ReservationDates = new ObservableCollection<AccommodationReservation>();
        }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;

        public Accommodation SelectedAccommodation { get; set; }
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;

        public List<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> reservations;
        public ObservableCollection<Accommodation> accommodations { get; set; }
        public ObservableCollection<AccommodationReservation> ReservationDates { get; set; }
        public AccommodationReservation SelectedDate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;


        int recursion = 0;

        //private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (dpEnd.SelectedDate < DateTime.Now.Date)
        //    {
        //        string sMessageBoxText = $"You have not chosen valid end date!";
        //        string sCaption = "Input error: End date";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;


        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        dpEnd.SelectedDate = DateTime.Now.Date;
        //    }

        //}

        //private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (dpStart.SelectedDate < DateTime.Now.Date)
        //    {
        //        string sMessageBoxText = $"You have not chosen valid start date!";
        //        string sCaption = "Input error: Start date";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;


        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        dpStart.SelectedDate = DateTime.Now.Date;
        //    }

        //}

        //private bool CheckMaxGuestsLimit(int numOfGuests)
        //{
        //    if (numOfGuests > SelectedAccommodation.MaxGuests)
        //    {
        //        string sMessageBoxText = $"Maximum number of guests for this accommodation is {SelectedAccommodation.MaxGuests}!";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;

        //        string sCaption = "Exceeded number of guests";
        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        return false;
        //    }

        //    return true;
        //}

        //private bool CheckMinReservationLimit(int numOfDays)
        //{
        //    if (numOfDays < SelectedAccommodation.MinReservationDays)
        //    {
        //        string sMessageBoxText = $"Minimal reservation for this accommodation is {SelectedAccommodation.MinReservationDays} days!";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;

        //        string sCaption = "Minimal reservation limit";
        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        return false;
        //    }

        //    return true;
        //}

        //private bool IsEndBeforeStart()
        //{
        //    if (dpStart.SelectedDate > dpEnd.SelectedDate)
        //    {
        //        string sMessageBoxText = $"Start date cannot be before end date!";
        //        string sCaption = "Date not valid";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;


        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        dpStart.SelectedDate = EndDate;

        //        return true;
        //    }

        //    return false;
        //}

        //private bool IsEndDateValid(double numOfDays)
        //{
        //    if (StartDate.Date.AddDays(numOfDays) > EndDate.Date)
        //    {
        //        string sMessageBoxText = $"Chosen start and end date does not match entered numer of days!";
        //        string sCaption = "Date not valid";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Error;


        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        dpStart.SelectedDate = EndDate;

        //        return false;
        //    }

        //    return true;
        //}

        //private bool CheckConditions()
        //{

        //    int numOfGuests = Convert.ToInt32(tbGuestNumber.Text);

        //    if (!CheckMaxGuestsLimit(numOfGuests)) return false;

        //    double numOfDays = Convert.ToDouble(tbReservationDays.Text);

        //    if (!CheckMinReservationLimit((int)numOfDays)) return false;

        //    // Date check
        //    if (IsEndBeforeStart()) return false;

        //    if (!IsEndDateValid(numOfDays)) return false;

        //    return true;
        //}

        //private List<Domain.Model.Reservations.AccommodationReservation> GetReservationsInDateRange()
        //{
        //    List<Domain.Model.Reservations.AccommodationReservation> reservations = new List<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationRepository.GetAll());
        //    List<Domain.Model.Reservations.AccommodationReservation> reservationsInRange = new List<Domain.Model.Reservations.AccommodationReservation>();

        //    foreach (var reservation in reservations)
        //    {
        //        if (reservation.AccommodationId == SelectedAccommodation.Id)
        //        {
        //            if ((reservation.StartDate > EndDate) || (reservation.EndDate < StartDate))
        //                continue;

        //            reservationsInRange.Add(reservation);
        //        }


        //    }

        //    return reservationsInRange;
        //}


        //private void SearchClick(object sender, RoutedEventArgs e)
        //{
        //    StartDate = dpStart.SelectedDate.Value;
        //    EndDate = dpEnd.SelectedDate.Value;

        //    if (!CheckConditions()) return;

        //    double numOfDays = Convert.ToDouble(tbReservationDays.Text);

        //    double daysBetween = (dpEnd.SelectedDate.Value - dpStart.SelectedDate.Value).TotalDays;


        //    ReservationDates.Clear();

        //    while (true)
        //    {

        //        List<Domain.Model.Reservations.AccommodationReservation> reservationsInRange = new List<Domain.Model.Reservations.AccommodationReservation>(GetReservationsInDateRange());

        //        var selectedDates = Enumerable
        //            .Range(0, int.MaxValue)
        //            .Select(index => new DateTime?(StartDate.AddDays(index)))
        //            .TakeWhile(date => date <= EndDate)
        //            .ToDictionary(date => date.Value.Date, date => true);


        //        foreach (var reservation in reservationsInRange)
        //        {
        //            var reservationDates = Enumerable
        //                .Range(0, int.MaxValue)
        //                .Select(index => new DateTime?(reservation.StartDate.AddDays(index)))
        //                .TakeWhile(date => date <= reservation.EndDate)
        //                .ToList();

        //            foreach (var date in reservationDates)
        //            {
        //                if (selectedDates.ContainsKey(date.Value.Date))
        //                {
        //                    selectedDates[date.Value.Date] = false;
        //                }
        //            }

        //        }

        //        foreach (var date in selectedDates)
        //        {
        //            if (date.Value == false)
        //            {
        //                continue;
        //            }

        //            if (date.Key.AddDays(numOfDays) > EndDate)
        //            {
        //                break;
        //            }

        //            if (selectedDates[date.Key.AddDays(numOfDays)] == false)
        //            {
        //                continue;
        //            }

        //            //AccommodationReservation reservation =
        //            //   new(SelectedAccommodation, LoggedUser);

        //           // ReservationDates.Add(reservation);
        //        }

        //        if (ReservationDates.Count == 0)
        //        {
        //            StartDate = EndDate.AddDays(1);
        //            EndDate = StartDate.AddDays(daysBetween);
        //            recursion++;

        //        }
        //        else if (ReservationDates.Count > 0 && recursion > 0)
        //        {
        //            tbNotFound.Text = $"We have not been able to find free dates. Here are some alternatives in the next {(recursion + 1) * (int)daysBetween} days:";
        //            recursion = 0;
        //            break;
        //        }
        //        else
        //        {
        //            tbNotFound.Text = string.Empty;
        //            break;
        //        }
        //    }

        //}


        //private void ReserveClick(object sender, RoutedEventArgs e)
        //{
        //    if (SelectedDate == null)
        //    {
        //        string sMessageBoxText = $"Choose a reservation first!";
        //        string sCaption = "Reservation not chosen";

        //        MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //        MessageBoxImage icnMessageBox = MessageBoxImage.Warning;


        //        MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //        return;
        //    }

        //    /* var reservation = _accommodationReservationRepository.GetAll().Where(r => (r.AccommodationId == SelectedDate.) &&
        //                                             (r.StartDate == SelectedDate.StartDate) &&
        //                                             (r.EndDate == SelectedDate.EndDate));
        //     if (reservation != null)
        //     {
        //         string sMessageBoxText = $"You have already made this reservation!";
        //         string sCaption = "Reservation already exists";

        //         MessageBoxButton btnMessageBox = MessageBoxButton.OK;
        //         MessageBoxImage icnMessageBox = MessageBoxImage.Error;


        //         MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        //         return;
        //     }

        //     MessageBoxResult result = MessageBox.Show("Are you sure you want to reserve this accommodation at chosen date?", "Confirm reservation",
        //             MessageBoxButton.YesNo, MessageBoxImage.Question);

        //     if (result == MessageBoxResult.Yes)
        //     {
        //         //SelectedDate.Guest = Controller.Guest;
        //         //SelectedDate.Accommodation = Accommodation;
        //         //Controller.AddReservation(SelectedDate);
        //         int guestNumber = int.Parse(tbGuestNumber.Text);
        //         _accommodationReservationRepository.SaveReservation(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value, LoggedUser, SelectedAccommodation, guestNumber);
        //     }*/
        //    int guestNumber = int.Parse(tbGuestNumber.Text);
        //    _accommodationReservationRepository.SaveReservation(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value, LoggedUser, SelectedAccommodation, guestNumber);
        //    MessageBox.Show("Reservation successful!");
        //}
    }
}
