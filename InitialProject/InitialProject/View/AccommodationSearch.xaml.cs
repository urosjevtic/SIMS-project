using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRepo;
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
using System.Xaml.Schema;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationSearch.xaml
    /// </summary>
    public partial class AccommodationSearch : Window
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        public List<Accommodation> accommodations { get; set; }
        public List<Location> locations { get; set; }

        public User LoggedUser { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        

        public AccommodationSearch(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            LoggedUser = user;
            accommodations = _accommodationRepository.GetAll();
            locations = _locationRepository.GetAll();
            AddAccommodationLocation(accommodations, locations);

            // AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }
        private void AddAccommodationLocation(List<Accommodation> accommodations, List<Location> locations)
        {
            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }

        public List<Accommodation> Search()
        {
            AddAccommodationLocation(accommodations, locations);

            List<Accommodation> searchResults = accommodations.ToList();

            RemoveByLocation(searchResults);
            RemoveByNumbers(searchResults);

            return searchResults;
        }
        public List<Accommodation> RemoveByLocation(List<Accommodation> searchResults)
        {
            string[] searchValues = { tbAccommodationName.Text, tbCityName.Text, tbCountryName.Text, cbAccommodationType.Text };
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.Concatenate().ToLower().Contains(value.ToLower()));
            return searchResults;
        }

        public List<Accommodation> RemoveByNumbers(List<Accommodation> searchResults)
        {
            int searchGuestNumber = tbGuestNumber.Text == "" ? -1 : Convert.ToInt32(tbGuestNumber.Text);
            int searchDayNumber = tbReservationDays.Text == "" ? -1 : Convert.ToInt32(tbReservationDays.Text);
            if (searchGuestNumber > 0) searchResults.RemoveAll(x => x.MaxGuests < searchGuestNumber);
            if (searchDayNumber > 0) searchResults.RemoveAll(x => x.MinReservationDays > searchDayNumber);

            return searchResults;
        }

        private void SerchButton_Click(object sender, RoutedEventArgs e)
        {
            //accommodations = _accommodationRepository.GetAll();
            List<Accommodation> filteredAccommodation = Search();
            accommodations = filteredAccommodation;
            AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
            accommodations = _accommodationRepository.GetAll();
        }


        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccommodationReservation accommodationReservation = new AccommodationReservation(SelectedAccommodation, LoggedUser);
                accommodationReservation.Show();
            }
        }
    }
}
