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
using InitialProject.Domain.Model;
using InitialProject.ViewModels.ReservationsViewModels;

namespace InitialProject.View.OwnerView.Reservations
{
    /// <summary>
    /// Interaction logic for ReservationsMainView.xaml
    /// </summary>
    public partial class ReservationsMainView : Page
    {
        public ReservationsMainView(User logedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new ReservationsMainViewModel(logedInUser, navigationService);
        }
    }
}
