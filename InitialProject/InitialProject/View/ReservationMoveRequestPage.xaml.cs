using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.ViewModels;
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
    /// Interaction logic for ReservationMoveRequestPage.xaml
    /// </summary>
    public partial class ReservationMoveRequestPage : Page
    {
        //public AccommodationReservation SelectedReservation;
        public ReservationMoveRequestPage(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
           // SelectedReservation = selectedReservation;
            this.DataContext = new MoveRequestViewModel(selectedReservation);
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.DataContext = new MoveRequestViewModel(selectedReservation, this.NavigationService);
        //}
    }
}
