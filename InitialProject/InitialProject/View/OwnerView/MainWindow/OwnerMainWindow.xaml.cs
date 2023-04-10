using InitialProject.Domain.Model;
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
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {

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

        public User LoggedInUser { get; set; }
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedInUser = user;

            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _unratedGuestRepository = new UnratedGuestRepository();
            _userRepository = new UserRepository();

            LoadData();
            Accommodations = new ObservableCollection<Accommodation>(_accommodations);
            UnratedGuests = new ObservableCollection<UnratedGuest>(_unratedGuests);


        }

        private void LoadData()
        {
            _locations = LoadLocations();
            _accommodations = LoadAccommodations();
            _unratedGuests = LoadUnratedGuests();
        }

        private List<Location> LoadLocations()
        {
            List<Location> locations = new List<Location>();
            locations = _locationRepository.GetAll();

            return locations;
        }

        private List<Accommodation> LoadAccommodations()
        {
            List<Accommodation>  accommodations = LoadOwnersAccommodation();
            ConnectLocationToAccommodation(accommodations);
            return accommodations;
        }

        private List<Accommodation> LoadOwnersAccommodation()
        {
            List<Accommodation> allAccommodations = _accommodationRepository.GetAll();
            List<Accommodation> accommodations = new List<Accommodation>();

            foreach (Accommodation accommodation in allAccommodations)
            {
                if (accommodation.Owner.Id == LoggedInUser.Id)
                {
                    accommodations.Add(accommodation);
                }
            }

            return accommodations;
        }

        private void ConnectLocationToAccommodation(List<Accommodation> accommodations)
        {
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

        }

        private List<UnratedGuest> LoadUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = GetAllUnratedGuests();


            return unratedGuests;
        }

        private List<UnratedGuest> GetAllUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = new List<UnratedGuest>();
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();
            foreach (UnratedGuest unratedGuest in allUnratedGuests)
            {
                ConnectAccommodationToUnratedGuest(unratedGuest);
                ConnectUserToUnratedGuest(unratedGuest);
                unratedGuests.Add(unratedGuest);
            }
            return unratedGuests;
        }

        private void ConnectAccommodationToUnratedGuest(UnratedGuest unratedGuest)
        {

        }

        private void ConnectUserToUnratedGuest(UnratedGuest unratedGuest)
        {
            
        }

        private void RemoveUnratedGuestsNotBelongingToCurrentUser(List<UnratedGuest> unratedGuests)
        {

        }

        public void RemoveUnratedGuestAfterFiveDays(List<UnratedGuest> unratedGuests)
        {

        }

        public void UpdateAccommodations()
        {
            _accommodations = LoadAccommodations();

            accommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(_accommodations);
        }


        private void ButtonClick_AddAccommodation(object sender, RoutedEventArgs e)
        {
            //AccommodationRegistration accommodationRegistration = new AccommodationRegistration(this, LoggedInUser);
            //accommodationRegistration.Show();
        }

        private void ButtonClik_RateGuest(object sender, RoutedEventArgs e)
        {
            if (SelectedUnratedGuest != null)
            {
                GuestRatingForm guestRatingForm = new GuestRatingForm(LoggedInUser, SelectedUnratedGuest);
                guestRatingForm.Show();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void RescheduleRequest_Click(object sender, RoutedEventArgs e)
        {
            RescheduleRequestWindow rescheduleRequestWindow = new RescheduleRequestWindow(LoggedInUser);
            this.Close();
            rescheduleRequestWindow.Show();

        }
    }
}
