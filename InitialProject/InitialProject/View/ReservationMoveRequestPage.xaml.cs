using InitialProject.Domain.Model.Reservations;
using InitialProject.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ReservationMoveRequestPage.xaml
    /// </summary>
    public partial class ReservationMoveRequestPage : Page
    {
        public AccommodationReservation SelectedReservation;
        public ReservationMoveRequestPage(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            SelectedReservation = selectedReservation;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MoveRequestViewModel(SelectedReservation, this.NavigationService);
        }
    }
}
