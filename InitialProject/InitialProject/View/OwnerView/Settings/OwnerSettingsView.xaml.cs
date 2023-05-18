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
using InitialProject.ViewModels.SettingsViewModel;

namespace InitialProject.View.OwnerView.Settings
{
    /// <summary>
    /// Interaction logic for OwnerSettingsView.xaml
    /// </summary>
    public partial class OwnerSettingsView : Page
    {
        public OwnerSettingsView(User logedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new OwnerSettingsViewModel(logedInUser, navigationService);
        }
    }
}
