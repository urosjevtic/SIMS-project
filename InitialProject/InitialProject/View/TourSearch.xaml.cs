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
        public List<Tour> filteredTours { get; set; }
        public User LoggedUser { get; set; }

        public ShowTour showTour;

        public TourSearch(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            tours = _tourRepository.GetAll();
            locations = _locationRepository.GetAll();
            filteredTours = new List<Tour>();
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
            /*List<Location> locations = new List<Location>();
            
            tours = _tourRepository.GetAll();
            locations = _locationRepository.GetAll();*/
            AddTourLocation(tours, locations);

            /*filteredTours = tours.Where(t =>
            (string.IsNullOrEmpty(stateTextBox.Text) || t.Location.City.ToLower().Contains(stateTextBox.Text.ToLower())) &&
            (string.IsNullOrEmpty(cityTextBox.Text) || t.Location.City.ToLower().Contains(cityTextBox.Text.ToLower())) &&
            (string.IsNullOrEmpty(durationTextBox.Text) || (t.Duration >= int.Parse(durationTextBox.Text.ToLower())) &&
            (string.IsNullOrEmpty(languageTextBox.Text) || t.Language.ToLower().Contains(languageTextBox.Text.ToLower())) &&
            (string.IsNullOrEmpty(numberTextBox.Text) || t.Duration >= int.Parse(numberTextBox.Text.ToLower())))).ToList();*/

            /*
            if (stateTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Location.Country.ToLower().Contains(stateTextBox.Text.ToLower()))
                    {
                        if (tour.Location.City.ToLower().Contains(cityTextBox.Text.ToLower()))
                        {
                            if (isLocationValid(tour))
                            {
                                filteredTours.Add(tour);
                            }
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
                        if (isLocationValid(tour))
                        {
                            filteredTours.Add(tour);
                        }
                    }
                }
            }
            if (durationTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Duration == int.Parse(durationTextBox.Text))
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
                    if (tour.MaxGuests >= int.Parse(numberTextBox.Text))
                    {
                        filteredTours.Add(tour);
                    }
                }
            }
            */

            foreach (Tour tour in tours) {
                if (isLocationValid(tour) && isLanguageValid(tour) && isDurationValid(tour) && isNumGuestValid(tour)) {
                    filteredTours.Add(tour);
                }
            
            }

            return filteredTours;
        }

        public bool isLocationValid(Tour tour)
        {
            if (_locationRepository.GetById(tour.Location.Id).Country == stateTextBox.Text || stateTextBox.Text == String.Empty)
            {
                if (_locationRepository.GetById(tour.Location.Id).City == cityTextBox.Text || cityTextBox.Text == String.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isDurationValid(Tour tour) {
            if (tour.Duration == Convert.ToInt32(durationTextBox.Text) || durationTextBox.Text == "")
            {
                return true;
            }
            return false ;
        }

        public bool isLanguageValid(Tour tour) {
            if (tour.Language == languageTextBox.Text || languageTextBox.Text == String.Empty)
            {
                return true;
            }
            return false;

        }

        public bool isNumGuestValid(Tour tour) {
            if (numberTextBox.Text == "")
            {
                MessageBox.Show("Enter number of guests!");
            }
            else if(tour.MaxGuests >= Convert.ToInt32(numberTextBox.Text))
            {
                return true;
            }
            return false;

        }
        public void searchButtonClick(object sender, RoutedEventArgs e)
        {
            filteredTours = Search();
            SearchResult searchResult = new SearchResult(filteredTours, LoggedUser);
            searchResult.Show();
            this.Close();
        }

    }
}
