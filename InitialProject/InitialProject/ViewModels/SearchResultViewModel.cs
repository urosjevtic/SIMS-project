using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class SearchResultViewModel
    {
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        public List<Tour> tours { get; set; }
        public List<Voucher> vouchers { get; set; }
        public User LoggedUser { get; set; }
        public ICommand ReserveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

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
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (value != _tours)
                {
                    _tours = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        public SearchResultViewModel(User user, Tour tour)
        {
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            ReserveCommand = new RelayCommand(Reserve);
            CancelCommand = new RelayCommand(Cancel);
            vouchers = _voucherService.GetAllCreated();
            LoggedUser = user;
            Vouchers = new ObservableCollection<Voucher>(vouchers);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void Cancel()
        {
            Window currentWindow = Application.Current.Windows.OfType<SearchResult>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
        }
        private void Reserve()
        {

            if (SelectedTour == null || _numberOfPeople == null || _averageAge == null)
            {
                if (SelectedTour == null)
                {
                    MessageBox.Show("You did not select any tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (_numberOfPeople == null)
                {
                    MessageBox.Show("You did not type number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (_averageAge == null)
                {
                    MessageBox.Show("You did not type average age!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                int numberOfPeople = int.Parse(_numberOfPeople);
                int freeSeats = SelectedTour.MaxGuests - _tourReservationService.CountUnreservedSeats(SelectedTour);
                double age = double.Parse(_averageAge);

                if (numberOfPeople <= freeSeats)
                {
                    _tourReservationService.SaveReservation(SelectedTour, numberOfPeople, LoggedUser, _voucherService.IsSelectedVoucher(SelectedVoucher), age);
                    MessageBox.Show("Successfully reserved!", "Announcement", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    if (SelectedVoucher != null)
                    {
                        _voucherService.ChangeToUsed(SelectedVoucher);
                    }
                    Window currentWindow = Application.Current.Windows.OfType<SearchResult>().SingleOrDefault(w => w.IsActive);
                    currentWindow?.Close();
                }
                else if (SelectedTour.MaxGuests == _tourReservationService.CountUnreservedSeats(SelectedTour))
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
            foreach(var t in tours)
            {
                if(t != tour)
                {
                    Tours.Add(t);
                }
            }
        }
    }
}

