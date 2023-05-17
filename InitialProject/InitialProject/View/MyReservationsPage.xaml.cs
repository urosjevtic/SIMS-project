using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.ReservationServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyReservationsPage.xaml
    /// </summary>
    public partial class MyReservationsPage : Page
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly CanceledResrvationsRepository _canceledResrvationsRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _reservations;

        public AccommodationReservation SelectedAccommodation { get; set; }
        public Domain.Model.Reservations.AccommodationReservation SelectedReservation { get; set; }

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
        public MyReservationsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            // User = LoggedUser;
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _canceledResrvationsRepository = new CanceledResrvationsRepository();
            LoadAllReservations();
        }
        public UnratedOwner UnratedOwner { get; set; }

        private void LoadAllReservations()
        {
            Reservations = new ObservableCollection<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationService.GetFutureReservations());
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.StartDate.AddDays(SelectedReservation.Accommodation.CancelationPeriod) >= DateTime.Now)
                {
                    _canceledResrvationsRepository.SaveCanceledReservation(SelectedReservation);
                    _accommodationReservationRepository.Delete(SelectedReservation);
                    LoadAllReservations();
                    MessageBox.Show("You have successfully canceled your reservation");
                }
                else
                {
                    MessageBox.Show("The cancellation period has expired");
                }
            }
            else
            {
                MessageBox.Show("You must select the reservation you want to delete");
            }
        }

        private void MoveReservationButton_Click1(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                ReservationMoveRequest request = new ReservationMoveRequest(SelectedAccommodation);
                request.Show();
            }
        }
    }
}
