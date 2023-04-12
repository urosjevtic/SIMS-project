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
using InitialProject.ViewModels.ReservationsViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleRequestWindow.xaml
    /// </summary>
    public partial class RescheduleRequestWindow : Window
    {

        public RescheduleRequestWindow(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new RescheduleRequestViewModel(loggedInUser);
        }

    }
}
