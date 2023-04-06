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
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ShowTour.xaml
    /// </summary>
    public partial class ShowTour : Window
    {
        public User LoggedUser { get; set; }
        public ShowTour(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;
        }
        /*private void loadData()
        {
            tours = _tourRepository.GetAll();
            locations = _locationRepository.GetAll();
            foreach (Tour tour in tours)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;
                        break;
                    }
                }
            }
        }*/
        private void OpenSearchButtonClick(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }

        private void OpenVouchersButtonClick(object sender, RoutedEventArgs e)
        {
            ShowVouchers showVouchers = new ShowVouchers();
            showVouchers.Show();
        }

        private void OpenMyToursButtonClick(object sender, RoutedEventArgs e)
        {
            MyTours myTours = new MyTours(LoggedUser);
            myTours.Show();
        }
    }
}
