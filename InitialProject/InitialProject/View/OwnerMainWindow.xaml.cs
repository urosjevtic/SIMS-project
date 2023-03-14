using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly UnratedGuestRepository _unratedGuestRepository;
        private readonly UserRepository _userRepository;

        public List<Accommodation> accommodations;
        public List<Location> locations;
        public List<UnratedGuest> unratedGuests;
        public List<User> users;

        public UnratedGuest SelectedUnratedGuest { get; set; }
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedInUser = user;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _unratedGuestRepository = new UnratedGuestRepository();
            _userRepository = new UserRepository();

            loadData();
            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
            unratedGuestsDataGrid.ItemsSource = new ObservableCollection<UnratedGuest>(unratedGuests);

            if (unratedGuests.Count > 0)
            {
                MessageBox.Show("You have unrated guest!", "Unrated guests", MessageBoxButton.OK);
            }

        }

        private void loadData()
        {
            loadLocations();
            loadAccommodations();
            loadUnratedGuests();
        }

        private List<Location> loadLocations()
        {
            locations = new List<Location>();
            locations = _locationRepository.GetAll();

            return locations;
        }

        private List<Accommodation> loadAccommodations()
        {
            List<Accommodation> allAccommodations = new List<Accommodation>();
            allAccommodations = _accommodationRepository.GetAll();
            accommodations = new List<Accommodation>();

            foreach(Accommodation accommodation in allAccommodations)
            {
                if(accommodation.Owner.Id == LoggedInUser.Id)
                {
                    accommodations.Add(accommodation);
                }
            }
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
            return accommodations;
        }

        private List<UnratedGuest> loadUnratedGuests()
        {
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();
            unratedGuests = new List<UnratedGuest>();
            List<User> allUsers = _userRepository.GetAll();
            foreach(UnratedGuest unratedGuest in allUnratedGuests)
            {
                foreach(Accommodation accommodation in accommodations)
                {
                    if(accommodation.Id == unratedGuest.ReservedAccommodation.Id)
                    {
                        unratedGuest.ReservedAccommodation = accommodation;
                        break;
                    }
                }
                
                unratedGuest.User = _userRepository.GetById(unratedGuest.User.Id);
                unratedGuests.Add(unratedGuest);
            }


            return unratedGuests;
        }


        public void UpdateDataGrid()
        {
            var locations = _locationRepository.GetAll();
            var accommodations = loadAccommodations();
            

            foreach (var accommodation in accommodations)
            {
                foreach (var location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }

            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }


        private void ButtonClickAddAccommodation(object sender, RoutedEventArgs e)
        {
            AccommodationRegistration accommodationRegistration = new AccommodationRegistration(this, LoggedInUser);
            accommodationRegistration.Show();
        }

        private void ButtonClikRateGuest(object sender, RoutedEventArgs e)
        {
            GuestRatingForm guestRatingForm = new GuestRatingForm(SelectedUnratedGuest);
            guestRatingForm.Show();
        }

    }
}
