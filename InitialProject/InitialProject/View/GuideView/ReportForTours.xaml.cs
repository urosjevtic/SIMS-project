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
using InitialProject.ViewModels.GuideViewModel;

namespace InitialProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for ReportForTours.xaml
    /// </summary>
    public partial class ReportForTours : Window
    {
        public ReportForTours()
        {
            InitializeComponent();
            DataContext = new GuideTourReport();
        }
    }
}
