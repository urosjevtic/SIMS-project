using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
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
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.Service.ReservationServices;
using System.ComponentModel;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AnywhereWheneverPage.xaml
    /// </summary>
    public partial class AnywhereWheneverPage : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;

        public Accommodation SelectedAccommodation { get; set; }
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;

        public List<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> reservations;
        public ObservableCollection<Accommodation> accommodations { get; set; }
        public ObservableCollection<AccommodationReservation> ReservationDates { get; set; }


    
        private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _reservations;
        public ObservableCollection<Domain.Model.Reservations.AccommodationReservation> Reservations
        {
            get
            {
                return _reservations;
            }

            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }

        public AnywhereWheneverPage()
        {
            InitializeComponent();
            _accommodationRepository = new AccommodationRepository();
            Accommodations = _accommodationRepository.GetAll();
        }

        private DateTime _startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("StartDate");
            }
        }


        private DateTime _endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("EndDate");
            }
        }

        private int _reservationDays = 0;

        public int ReservationDays
        {
            get { return _reservationDays; }
            set
            {
                _reservationDays = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("ReservationDays");
            }
        }

        private int _guestNumber = 0;

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("GuestNumber");
            }
        }
    }
}
