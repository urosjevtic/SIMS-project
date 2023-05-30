using InitialProject.ViewModels.RenovationsViewModel;
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

namespace InitialProject.View.OwnerView.Renovations
{
    /// <summary>
    /// Interaction logic for RenovationReportFormView.xaml
    /// </summary>
    public partial class RenovationReportFormView : Window
    {
        public RenovationReportFormView(User logedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RenovationReportSelectionViewModel(logedInUser, navigationService);
        }
    }
}
