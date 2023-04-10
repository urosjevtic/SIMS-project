using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.ReservationServices;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Window
    {
        public List<AccommodationReservation> reservation;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        public User LoggedUser { get; set; }
        public UnratedOwner unratedOwner { get; set; }
        public MyReservations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationRepository = new AccommodationReservationRepository();
        //   reservation = new List<AccommodationReservation>();
            _accommodationReservationRepository.GetAll();
            //reservation = _accommodationReservationRepository;
        }

        private void RatedButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRatingForm accommodationRating = new AccommodationRatingForm(unratedOwner);
            accommodationRating.Show();
        }
    }
}
