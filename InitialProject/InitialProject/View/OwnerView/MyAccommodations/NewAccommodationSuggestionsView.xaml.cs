using InitialProject.ViewModels.AccommodationViewModel;
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

namespace InitialProject.View.OwnerView.MyAccommodations
{
    /// <summary>
    /// Interaction logic for NewAccommodationSuggestionsView.xaml
    /// </summary>
    public partial class NewAccommodationSuggestionsView : Page
    {
        public NewAccommodationSuggestionsView(User logedInUser, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new NewAccommodationSuggestionsViewModel(logedInUser, navigationService);
        }
    }
}
