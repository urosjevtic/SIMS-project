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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Utilities;

namespace InitialProject.View.OwnerView.Ratings
{
    /// <summary>
    /// Interaction logic for AccommodationReviewsSelectionView.xaml
    /// </summary>
    public partial class AccommodationReviewsSelectionView : Page
    {
        public AccommodationReviewsSelectionView(User logedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new AccommodationReviewsSelectionViewModel(logedInUser, navigationService);
        }
    }
}
