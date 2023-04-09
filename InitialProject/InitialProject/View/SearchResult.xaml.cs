using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourService _tourService;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly VoucherRepository _voucherRepository;
        private readonly VoucherService _voucherService;
        public List<Tour> tours { get; set; }
        private List<Voucher> vouchers { get; set; }
        public Tour SelectedTour { get;set; }
        public Voucher SelectedVoucher { get; set; }
        public User LoggedUser { get; set; }
        public SearchResult(List<Tour> filteredTours, User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourService = new TourService();
            _tourReservationRepository = new TourReservationRepository();
            _voucherRepository = new VoucherRepository();
            _voucherService = new VoucherService();
            tours = filteredTours;
            vouchers = _voucherRepository.GetAllCreated();
            LoggedUser = user;
            resultDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
            vouchersComboBox.ItemsSource = new ObservableCollection<Voucher>(vouchers);
        }  
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            SelectedTour = (Tour)resultDataGrid.SelectedItem;
            SelectedVoucher = (Voucher)vouchersComboBox.SelectedItem;

            if(SelectedTour == null || nrOfPeopleTextBox.Text == String.Empty)
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
            else
            {
                int numberOfPeople = int.Parse(nrOfPeopleTextBox.Text);
                int freeSeats = SelectedTour.MaxGuests - _tourReservationRepository.CountUnreservedSeats(SelectedTour);
                double age = double.Parse(averageAge.Text);

                if (numberOfPeople <= freeSeats)
                {
                    _tourReservationRepository.SaveReservation(SelectedTour, numberOfPeople, LoggedUser, _voucherService.IsSelectedVoucher(SelectedVoucher), age);
                    MessageBox.Show("Successfully reserved!", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    if (SelectedVoucher != null)
                    {
                        _voucherRepository.ChangeToUsed(SelectedVoucher);    
                    }
                    this.Close();
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
        }
        public void FindAlternatives(Tour tour)
        {
            List<Tour> tours = _tourService.FindAllAlternatives(tour);
            resultDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
    }
}

