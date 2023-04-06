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
using InitialProject.Domain.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        private readonly TourRepository _tourRepository;

        private readonly LocationRepository _locationRepository;
        public List<Tour> tours { get; set; }
        public List<Location> locations { get; set; }
        public User LoggedUser { get; set; }

        public TourSearch(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            tours = _tourRepository.GetAll();
            locations = _locationRepository.GetAll();
            LoggedUser = user;
        }
        private void AddTourLocation(List<Tour> tours, List<Location> locations)
        {
            foreach (Tour tour in tours)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;
                        break;
                    }
                }
            }
        }
        public List<Tour> Search()
        {
            AddTourLocation(tours, locations);

            List<Tour> searchResults = tours.ToList();

            RemoveByLocation(searchResults);
            RemoveByNumbers(searchResults);

            return searchResults;   
        }

        public List<Tour> RemoveByLocation(List<Tour> searchResults)
        {
            string[] searchValues = { stateTextBox.Text, cityTextBox.Text, languageTextBox.Text };
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.Concatenate().ToLower().Contains(value.ToLower()));
            return searchResults;
        }

        public List<Tour> RemoveByNumbers(List<Tour> searchResults)
        {
            int searchDuration = durationTextBox.Text == "" ? -1 : Convert.ToInt32(durationTextBox.Text);
            int searchMaxGuests = numberTextBox.Text == "" ? -1 : Convert.ToInt32(numberTextBox.Text);
            if (searchDuration > 0) searchResults.RemoveAll(x => x.Duration != searchDuration);
            if (searchMaxGuests > 0) searchResults.RemoveAll(x => x.MaxGuests < searchMaxGuests);

            return searchResults;
        }
        public void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            List<Tour> filteredTours = Search();
            SearchResult searchResult = new SearchResult(filteredTours, LoggedUser);
            searchResult.Show();
            this.Close();
        }

    }
}
