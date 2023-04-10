using InitialProject.ViewModels.RatingsViewModel;
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

namespace InitialProject.View.OwnerView.Ratings
{
    /// <summary>
    /// Interaction logic for AccommodationReviewsSelectionWindow.xaml
    /// </summary>
    public partial class AccommodationReviewsSelectionWindow : Window
    {
        public AccommodationReviewsSelectionWindow(User logedInUser)
        {
            InitializeComponent();
            DataContext = new AccommodationReviewsSelectionViewModel(logedInUser);
        }
    }
}
