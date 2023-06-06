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
using InitialProject.ViewModels.PopupViewModel;

namespace InitialProject.View.OwnerView.PopupWindows
{
    /// <summary>
    /// Interaction logic for SuccessfullGuestRatingView.xaml
    /// </summary>
    public partial class SuccessfullGuestRatingView : Window
    {
        public SuccessfullGuestRatingView()
        {
            InitializeComponent();
            DataContext = new SuccessfullGuestRatingViewModel();
        }
    }
}
