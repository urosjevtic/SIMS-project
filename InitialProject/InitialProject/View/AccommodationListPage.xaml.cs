using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationListPage.xaml
    /// </summary>
    public partial class AccommodationListPage : Page
    {
        public AccommodationListPage()
        {
            InitializeComponent();
        }

        private void AccommodationsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new AccommodationListViewModel(this.NavigationService);
        }
    }
}
