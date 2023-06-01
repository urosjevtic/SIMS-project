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
using InitialProject.ViewModels.Guest2ViewModel;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowNotificationsPage.xaml
    /// </summary>
    public partial class ShowNotificationsPage : Page
    {
        public ShowNotificationsPage(NavigationService nav)
        {
            InitializeComponent();
            this.DataContext = new ShowNotificationsViewModel(nav);
        }
    }
}
