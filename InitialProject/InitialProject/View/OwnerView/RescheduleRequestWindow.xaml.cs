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
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.ViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleRequestWindow.xaml
    /// </summary>
    public partial class RescheduleRequestWindow : Window
    {
        public RescheduleRequestViewModel ViewModel { get; set; }

        public RescheduleRequestWindow(User loggedInUser)
        {
            InitializeComponent();

            ViewModel = new RescheduleRequestViewModel(loggedInUser);
            ViewModel.CloseAction = () => Close();
            DataContext = ViewModel;
        }

        private void RescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rescheduleRequest = button?.DataContext as AccommodationReservationRescheduleRequest;
            if (rescheduleRequest != null)
            {
                ViewModel.ApproveReschedule(rescheduleRequest);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoBack();
        }

        private void DeclineRescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rescheduleRequest = button?.DataContext as AccommodationReservationRescheduleRequest;
            if (rescheduleRequest != null)
            {
                ViewModel.DeclineReschedule(rescheduleRequest);
            }
        }
    }
}
