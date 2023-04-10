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
using InitialProject.ViewModels.RatingsViewModel;

namespace InitialProject.View.OwnerView.Ratings
{
    /// <summary>
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {
        public GuestRatingForm(User logedInUser, UnratedGuest unratedGuest)
        {
            InitializeComponent();
            DataContext = new GuestRatingFormViewModel(logedInUser, unratedGuest);
        }
    }
}
