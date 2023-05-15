using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AccommodationRatingFormPage.xaml
    /// </summary>
    public partial class AccommodationRatingFormPage : Page
    {
        public Domain.Model.Reservations.AccommodationReservation SelectedReservation { get; set; }
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        public AccommodationRatingFormPage(AccommodationReservation reservation)
        {
            InitializeComponent();
            
            SelectedReservation = reservation;
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }
    }
}
