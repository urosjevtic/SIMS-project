using InitialProject.Domain.Model;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository;
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
using Microsoft.VisualBasic.ApplicationServices;
using InitialProject.ViewModels;
using System.Globalization;
using System.Windows.Controls.DataVisualization.Charting;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationSearchPage.xaml
    /// </summary>
    public partial class AccommodationSearchPage : Page
    {
        public AccommodationSearchPage()
        {
            InitializeComponent();
        }

        private void AccommodationsSearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new AccommodationSearchViewModel(this.NavigationService); 
        }
    }
}
