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
using InitialProject.Model;
using InitialProject.Repository;
//using System.Windows.Forms;
using System.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        public ObservableCollection<Tour> toursObservable { get; set; }

        private readonly TourRepository _tourRepository;

        private readonly LocationRepository _locationRepository;
        public List<Tour> tours { get; set; }
        public List<Location> locations { get; set; }
        public User LoggedUser { get; set; }

        public ShowTour showTour;


        public User LoggedInUser { get; set; }
        public TourSearch(User user)
        {
            LoggedInUser= user;
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            //tours = _tourRepository.GetAll();
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

            string[] searchValues = { stateTextBox.Text, cityTextBox.Text, languageTextBox.Text };
            int searchDuration = durationTextBox.Text == "" ? -1 : Convert.ToInt32(durationTextBox.Text);
            int searchMaxGuests = numberTextBox.Text == "" ? -1 : Convert.ToInt32(numberTextBox.Text);

            List<Tour> searchResults = tours.ToList();

            // Removing all by location and language
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.Concatenate().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (searchDuration > 0) searchResults.RemoveAll(x => x.Duration != searchDuration);
            if (searchMaxGuests > 0) searchResults.RemoveAll(x => x.MaxGuests < searchMaxGuests);

            return searchResults;
          
        }
        public void searchButtonClick(object sender, RoutedEventArgs e)
        {
            List<Tour> filteredTours = Search();
            SearchResult searchResult = new SearchResult(filteredTours, LoggedUser);
            searchResult.Show();
            this.Close();
        }

    }
}
