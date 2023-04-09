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
using InitialProject.Service;
using InitialProject.ViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TourStatistic : Window
    {
        public TourService _tourService;
        public TourStatisticViewModel TourStatisticViewModel { get; set; }
        
        public TourStatistic()
        {
            InitializeComponent();
            TourStatisticViewModel = new TourStatisticViewModel();
            this.DataContext = TourStatisticViewModel;
            _tourService = new TourService();
            mostVisitedTourTextBlock.Text =  "Najposjećenija tura ikad je:  " + _tourService.FindMostVisited().Name + "."; ;
        }

        private void GodineComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                string year = ((ComboBoxItem)YearsComboBox.SelectedItem).Content.ToString();

                string Name = _tourService.GetMostVisitedInYear(year).Name;

                mostVisitedTourInYearTextBlock.Text = "Najposjećenija tura za " + year + " je " + Name + ".";
         
        }

        private void ShowOneTourStatisticClick(object sender, RoutedEventArgs e)
        {
            TourStatisticViewModel.ShowStatistics();
        }
    }
}
