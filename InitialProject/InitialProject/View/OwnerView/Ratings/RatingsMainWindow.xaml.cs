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
using InitialProject.Domain.Model;
using InitialProject.ViewModels;

namespace InitialProject.View.OwnerView.Ratings
{
    /// <summary>
    /// Interaction logic for RatingsMainWindow.xaml
    /// </summary>
    public partial class RatingsMainWindow : Window
    {
        public RatingsMainWindow(User logedInUser)
        {
            InitializeComponent();
            DataContext = new RatingsMainViewModel(logedInUser);
        }
    }
}
