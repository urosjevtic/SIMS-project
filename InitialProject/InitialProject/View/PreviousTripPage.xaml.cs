using InitialProject.Domain.Model.Reservations;
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
    /// Interaction logic for PreviousTripPage.xaml
    /// </summary>
    public partial class PreviousTripPage : Page
    {
        public PreviousTripPage()
        {
            InitializeComponent();
            this.DataContext = this;
            //User = LoggedUser;
            _accommodationReservationService = new AccommodationReservationService();
            //  _accommodationReservationRepository = new AccommodationReservationRepository();
            LoadAllReservations();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        // private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _trip;

        public AccommodationReservation SelectedAccommodation { get; set; }
        public Domain.Model.Reservations.AccommodationReservation SelectedReservation { get; set; }

        public ObservableCollection<Domain.Model.Reservations.AccommodationReservation> Trips
        {
            get
            {
                return _trip;
            }

            set
            {
                if (value != _trip)
                {
                    _trip = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }
        public UnratedOwner UnratedOwner { get; set; }

        private void LoadAllReservations()
        {
            Trips = new ObservableCollection<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationService.GetPastReservations());
        }

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRatingForm rating = new AccommodationRatingForm(UnratedOwner);
            rating.Show();
        }
    }
}
