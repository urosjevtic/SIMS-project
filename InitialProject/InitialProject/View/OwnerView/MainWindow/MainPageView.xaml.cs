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
using InitialProject.Utilities;
using InitialProject.ViewModels.MainVeiwModel;

namespace InitialProject.View.OwnerView.MainWindow
{
    /// <summary>
    /// Interaction logic for MainPageView.xaml
    /// </summary>
    public partial class MainPageView : Page
    {
        public MainPageView(User logedInUser, NavigationService navigatorService)
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(logedInUser, navigatorService);
        }
    }
}
