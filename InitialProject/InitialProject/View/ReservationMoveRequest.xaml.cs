using InitialProject.Domain.Model.Reservations;
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
    /// Interaction logic for ReservationMoveRequest.xaml
    /// </summary>
    public partial class ReservationMoveRequest : Window
    {
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        public AccommodationReservation SelectedReservation;
        public ReservationMoveRequest(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            SelectedReservation = selectedReservation;
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }
        
        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            string status = "pending";
         //   _accommodationReservationRescheduleRequestService.Create(SelectedReservation, dpNewStartDate.SelectedDate.Value, dpNewEndDate.SelectedDate.Value, status);
            MessageBox.Show("You have sent a move request");
          //_accommodationReservationRescheduleRequestService.Delete(SelectedReservation);
            this.Close();
        }
    }
}
