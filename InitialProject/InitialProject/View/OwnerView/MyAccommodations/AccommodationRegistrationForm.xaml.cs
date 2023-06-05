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
using InitialProject.ViewModels;

namespace InitialProject.View.OwnerView.MyAccommodations
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationForm.xaml
    /// </summary>
    public partial class AccommodationRegistrationForm : Page
    {
        public AccommodationRegistrationForm(User logedInUser, NavigationService navigationService, string country = "", string city = "")
        {
            InitializeComponent();
            DataContext = new AccommodationRegistrationViewModel(logedInUser, navigationService, country, city);
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
