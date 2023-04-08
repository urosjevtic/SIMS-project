using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.Repository;
using System.Data;
using InitialProject.Service;
using InitialProject.Domain.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        private readonly TourService _tourService;
        public User LoggedUser { get; set; }

        public TourSearch(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourService = new TourService();
            LoggedUser = user;
        }
        public void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            List<Tour> filteredTours = _tourService.Search(stateTextBox.Text, cityTextBox.Text, languageTextBox.Text, durationTextBox.Text, numberTextBox.Text);
            SearchResult searchResult = new SearchResult(filteredTours, LoggedUser);
            searchResult.Show();
            this.Close();
        }

    }
}
