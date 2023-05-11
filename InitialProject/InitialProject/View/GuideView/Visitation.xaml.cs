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
using InitialProject.Domain.Model;
using LiveCharts.Charts;
using InitialProject.Service;

namespace InitialProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for Visitation.xaml
    /// </summary>
    public partial class Visitation : Window
    {
        public Visitation(Tour tour)
        {
            InitializeComponent();
            DataContext = new VisitationViewModel(tour,MyPieChart);

            
        }
    }
}
