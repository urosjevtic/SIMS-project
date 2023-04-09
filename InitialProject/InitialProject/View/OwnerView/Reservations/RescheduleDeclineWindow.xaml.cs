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
using InitialProject.Domain.Model.Reservations;
using InitialProject.ViewModels;

namespace InitialProject.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RescheduleDeclineWindow.xaml
    /// </summary>
    public partial class RescheduleDeclineWindow : Window
    {
        public RescheduleDeclineViewModel ViewModel { get; set; }
        public RescheduleDeclineWindow(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            InitializeComponent();
            DataContext = new RescheduleDeclineViewModel(rescheduleRequest);
        }


    }
}
