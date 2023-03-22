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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ShowTour.xaml
    /// </summary>
    public partial class ShowTour : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        public List<Tour> tours;
        public List<Location> locations;
        public Tour SelectedTour { get; set; }
        public User LoggedUser { get;set; }

        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
       
        public List<Notification> Notifications { get; set; }

        public ShowTour(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _tourReservationRepository = new TourReservationRepository();
            LoggedUser = user;

            SelectedTour = null;
            
            _notificationRepository = new NotificationRepository();
            _userRepository = new UserRepository();
            Notifications = new List<Notification>(_notificationRepository.GetAll());
            loadData();
            tourDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);

        }
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }
        private void loadData()
        {
            tours = _tourRepository.GetAll();
            locations = LoadLocations();
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


        private void OpenSearchButtonClick(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }

        private void ReserveButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            SelectedTour = (Tour)tourDataGrid.SelectedItem;
            InitialProject.Model.TourReservation reservation = new InitialProject.Model.TourReservation();

            if (SelectedTour == null)
            {
                MessageBox.Show("You did not select any tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (nrOfPeopleTextBox.Text == String.Empty)
            {
                MessageBox.Show("You did not type number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (int.Parse(nrOfPeopleTextBox.Text) > SelectedTour.MaxGuests - 1)
            {
                MessageBox.Show("There is no enough free seats! Change number of people!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                GetNotification();
            }));
        }
        private void GetNotification()
        {
            foreach (Notification notification in Notifications)
            {
                if (notification.GuestId == LoggedUser.Id && notification.IsChecked == true)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste prisutni na turi?", "Potvrda prisustva", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        LoggedUser.Presence = UserPresence.Yes;
                        _userRepository.Update(LoggedUser);
                        notification.IsGoing = true;
                        _notificationRepository.Update(notification);
                    }
                    else
                    {
                        LoggedUser.Presence = UserPresence.No;
                        _userRepository.Update(LoggedUser);
                        notification.IsGoing = false;
                        _notificationRepository.Update(notification);
                    }

                }
            }
        }
    }
}
