using GalaSoft.MvvmLight.Views;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MyReservationsPage.xaml
    /// </summary>
    public partial class MyReservationsPage : Page
    {
        public MyReservationsPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MyReservationsViewModel(this.NavigationService);
        }

    }
}
