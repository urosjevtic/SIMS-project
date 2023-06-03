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
using InitialProject.ViewModels.HelpViewModel;

namespace InitialProject.View.OwnerView.Help
{
    /// <summary>
    /// Interaction logic for OwnerHelpView.xaml
    /// </summary>
    public partial class OwnerHelpView : Page
    {
        public OwnerHelpView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new OwnerHelpViewModel(navigationService);
        }
    }
}
