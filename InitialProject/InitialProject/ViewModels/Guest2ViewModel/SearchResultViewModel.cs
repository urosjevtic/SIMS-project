using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InitialProject.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        public List<Tour> tours { get; set; }
        public List<Voucher> vouchers { get; set; }
        public User LoggedUser { get; set; } = App.LoggedUser;
        public ICommand ReserveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand UpButtonCommand { get; set; }
        public ICommand DownButtonCommand { get; set; }

        private string _numberOfPeople;
        public string NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (value != _numberOfPeople)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged(nameof(NumberOfPeople));
                }
            }
        }
        private string _averageAge;
        public string AverageAge
        {
            get => _averageAge;
            set
            {
                if (value != _averageAge)
                {
                    _averageAge = value;
                    OnPropertyChanged(nameof(AverageAge));
                }
            }
        }
        private Voucher _selectedVoucher;
        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                if (value != _selectedVoucher)
                {
                    _selectedVoucher = value;
                    OnPropertyChanged(nameof(SelectedVoucher));
                }
            }
        }
        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }
        private DateTime _selectedDateTime;
        public DateTime SelectedDateTime
        {
            get => _selectedDateTime;
            set
            {
                if (value != _selectedDateTime)
                {
                    _selectedDateTime = value;
                    OnPropertyChanged(nameof(SelectedDateTime));
                }
            }
        }
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (value != _tours)
                {
                    _tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }
        private ObservableCollection<Voucher> _vouchers;
        public ObservableCollection<Voucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (value != _vouchers)
                {
                    _vouchers = value;
                    OnPropertyChanged(nameof(Vouchers));
                }
            }
        }
        private ObservableCollection<DateTime> _dateTimes;
        public ObservableCollection<DateTime> DateTimes
        {
            get { return _dateTimes; }
            set
            {
                if (value != _dateTimes)
                {
                    _dateTimes = value;
                    OnPropertyChanged(nameof(DateTimes));
                }
            }
        }
        public NavigationService navigationService { get; }
        public SearchResultViewModel(Tour tour, NavigationService nav)
        {
            this.navigationService = nav;
            SelectedTour = tour;
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            ReserveCommand = new RelayCommand(Reserve);
            CancelCommand = new RelayCommand(GoBack);
            vouchers = _voucherService.GetAllCreated();
            Tours = new ObservableCollection<Tour>();
            Vouchers = new ObservableCollection<Voucher>(vouchers);
            DateTimes = new ObservableCollection<DateTime>(_tourService.GetById(tour.Id).StartDates);
            NumberOfPeople = "0";
            SelectedDateTime = DateTimes[0];
        }
        private void UpButton()
        {
            int currentNumber = int.Parse(NumberOfPeople);
            NumberOfPeople = (currentNumber + 1).ToString();
        }
        private void DownButton()
        {
            int currentNumber = int.Parse(NumberOfPeople);
            NumberOfPeople = (currentNumber - 1).ToString();
        }
        private void GoBack()
        {
            navigationService.Navigate(new ShowTourPage(navigationService));
        }
        private void Reserve()
        {

            if (SelectedTour == null || 0 >= int.Parse(NumberOfPeople) || _averageAge == null)
            {
                if (SelectedTour == null)
                {
                    MessageBox.Show("You did not select any tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (0 >= int.Parse(NumberOfPeople))
                {
                    MessageBox.Show("Number of people must be greater than zero!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (_averageAge == null)
                {
                    MessageBox.Show("You did not type average age!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                int numberOfPeople = int.Parse(_numberOfPeople);
                int freeSeats = SelectedTour.MaxGuests - _tourReservationService.CountUnreservedSeats(SelectedTour, SelectedDateTime);
                double age = double.Parse(_averageAge);

                if (numberOfPeople <= freeSeats)
                {
                    MessageBoxResult answer = MessageBox.Show("Do you want to make PDF document of this reservation?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(answer == MessageBoxResult.Yes)
                    {
                        PDFService.GenerateTourReservationPDF(SelectedTour, _tourReservationService.CreateReservation(SelectedTour, numberOfPeople, LoggedUser, _voucherService.IsSelectedVoucher(SelectedVoucher), age, SelectedDateTime));
                    }
                    _tourReservationService.SaveReservation(SelectedTour, numberOfPeople, LoggedUser, _voucherService.IsSelectedVoucher(SelectedVoucher), age, SelectedDateTime);
                    MessageBox.Show("Successfully reserved!", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    if (SelectedVoucher != null)
                    {
                        _voucherService.ChangeToUsed(SelectedVoucher);
                    }
                }
                else if (SelectedTour.MaxGuests == _tourReservationService.CountUnreservedSeats(SelectedTour, SelectedDateTime))
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
            tours = _tourService.FindAllAlternatives(tour);
            Tours.Clear();
            foreach(Tour t in tours)
            {
                if(t.Id != tour.Id)
                {
                    Tours.Add(t);
                }
            }
            navigationService.Navigate(new TourSearchPage(navigationService, Tours));
        }
    }
}

