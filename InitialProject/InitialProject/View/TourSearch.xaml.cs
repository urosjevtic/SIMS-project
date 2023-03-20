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
using System.Windows.Forms;
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
        public List<Tour> filteredTours { get; set; }


        public ShowTour showTour;

        public TourSearch()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            filteredTours = new List<Tour>();
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
            List<Location> locations = new List<Location>();
            
            tours = _tourRepository.GetAll();
            locations = _locationRepository.GetAll();
            AddTourLocation(tours, locations);

            if (stateTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Location.Country.ToLower().Contains(stateTextBox.Text.ToLower()))
                    {
                        if (!Exists(tour))
                        {
                            filteredTours.Add(tour);
                        }
                    }
                }
            }
            if (cityTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Location.City.ToLower().Contains(cityTextBox.Text.ToLower()))
                    {
                        filteredTours.Add(tour);
                    }
                }
            }
            if (durationTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Duration <= int.Parse(durationTextBox.Text))
                    {
                        filteredTours.Add(tour);
                    }
                }
            }
            if (languageTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Language.ToLower().Contains(languageTextBox.Text.ToLower()))
                    {
                        filteredTours.Add(tour);
                    }
                }
            }
            if (numberTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.MaxGuests >= int.Parse(cityTextBox.Text))
                    {
                        filteredTours.Add(tour);
                    }
                }
            }

            return filteredTours;
        }

        public bool Exists(Tour tour)
        {

            return true;
        }
        public void searchButtonClick(object sender, RoutedEventArgs e)
        {
            filteredTours = Search();
            SearchResult searchResult = new SearchResult(filteredTours);
            searchResult.Show();
            this.Close();
        }

    }
}
