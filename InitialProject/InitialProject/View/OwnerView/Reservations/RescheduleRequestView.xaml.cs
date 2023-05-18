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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.ViewModels.ReservationsViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleRequestView.xaml
    /// </summary>
    public partial class RescheduleRequestView : Page
    {

        public RescheduleRequestView(User loggedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RescheduleRequestViewModel(loggedInUser, navigationService);
        }

    }
}
