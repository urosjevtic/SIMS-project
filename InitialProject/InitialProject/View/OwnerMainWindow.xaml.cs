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

        private List<Accommodation> _accommodations;
        private List<Location> _locations;
        private List<UnratedGuest> _unratedGuests;

        public ObservableCollection<UnratedGuest> UnratedGuests { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

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
            Accommodations = new ObservableCollection<Accommodation>(_accommodations);
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuests);


        }

        private void loadData()
        {
            _locations = loadLocations();
            _accommodations = loadAccommodations();
            _unratedGuests = loadUnratedGuests();
        }

        private List<Location> loadLocations()
        {
            List<Location> locations = new List<Location>();
            locations = _locationRepository.GetAll();

            return locations;
        }

        private List<Accommodation> loadAccommodations()
        {
            List<Accommodation> allAccommodations = new List<Accommodation>();
            allAccommodations = _accommodationRepository.GetAll();
            List<Accommodation> accommodations = new List<Accommodation>();

            foreach (Accommodation accommodation in allAccommodations)
            {
                if (accommodation.Owner.Id == LoggedInUser.Id)
                {
                    accommodations.Add(accommodation);
                }
            }
            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Location location in _locations)
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
            List<UnratedGuest> unratedGuests = new List<UnratedGuest>();
            unratedGuests = getAllUnratedGuests();

            unratedGuests = RemoveUnratedGuestsNotBelongingToCurrentUser(unratedGuests);
            
            unratedGuests = removeUnratedGuestAfterFiveDays(unratedGuests);

            return unratedGuests;
        }

        private List<UnratedGuest> getAllUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = new List<UnratedGuest>();
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();
            foreach (UnratedGuest unratedGuest in allUnratedGuests)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.Id == unratedGuest.ReservedAccommodation.Id)
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

        private List<UnratedGuest> RemoveUnratedGuestsNotBelongingToCurrentUser(List<UnratedGuest> unratedGuests)
        {
            for (int i = unratedGuests.Count - 1; i >= 0; i--)
            {
                UnratedGuest unratedGuest = unratedGuests[i];
                if (LoggedInUser.Id != unratedGuest.ReservedAccommodation.Owner.Id)
                {
                    unratedGuests.RemoveAt(i);
                }
            }

            return unratedGuests;
        }
        public List<UnratedGuest> removeUnratedGuestAfterFiveDays(List<UnratedGuest> unratedGuests)
        {
            var today = DateTime.Now;
            TimeSpan dateDifference;
            for (int i = unratedGuests.Count - 1; i >= 0; i--)
            {
                UnratedGuest unratedGuest = unratedGuests[i];
                dateDifference = today - unratedGuest.ReservationEndDate;
                if (dateDifference.TotalDays > 5)
                {
                    _unratedGuestRepository.Remove(unratedGuest);
                    unratedGuests.Remove(unratedGuest);
                }
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
            if (SelectedUnratedGuest != null)
            {
                GuestRatingForm guestRatingForm = new GuestRatingForm(SelectedUnratedGuest, this);
                guestRatingForm.Show();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_unratedGuests.Count > 0)
            {
                UnratedGuestNotification unratedGuestNotification = new UnratedGuestNotification(_unratedGuests);
                unratedGuestNotification.Owner = this;
                unratedGuestNotification.ShowDialog();
            }
        }
    }
}
