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
using InitialProject.ViewModels.GuideViewModel;

namespace InitialProject.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TodayToursPage.xaml
    /// </summary>
    public partial class TodayToursPage : Page
    {
        public TodayToursPage(User user,MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new TodayToursViewModel(user,mainWindow);
        }
    }
}
