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
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourRepository _tourRepository;
        public List<Tour> tours;
        public ShowTour()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OpenTourSearch(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new TourSearch();
            tourSearch.Show();
        }
    }
}
