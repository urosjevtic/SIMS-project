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
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowShortTourStatistics.xaml
    /// </summary>
    public partial class ShowShortTourStatistics : Window
    {
        public ShowShortTourStatisticsViewModel showShortTourStatisticsViewModel { get; set; }
        public ShowShortTourStatistics()
        {
            InitializeComponent();
            showShortTourStatisticsViewModel = new ShowShortTourStatisticsViewModel();
            this.DataContext = showShortTourStatisticsViewModel;
        }

    }
}
