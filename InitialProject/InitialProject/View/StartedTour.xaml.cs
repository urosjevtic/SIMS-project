using System;
using System.Collections.Generic;
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
using InitialProject.DTO;
using InitialProject.Domain.Model;
using InitialProject.Model;

namespace InitialProject.View
{

    public partial class StartedTour : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly UserRepository _userRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly TourGuestsRepository _tourGuestsRepository;
        public List<User> Guests { get; set; }
        public User User { get; set; }
        public Tour SelectedTour { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public List<CheckPoint> CheckedCheckPoints { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Guest> TourGuests { get; set; }   
        public StartedTour(Tour selectedTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _notificationRepository = new NotificationRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourGuestsRepository = new TourGuestsRepository(); 
            _userRepository = new UserRepository();
            _checkPointRepository = new CheckPointRepository();

            CheckedCheckPoints = new List<CheckPoint>();
            Notifications = _notificationRepository.GetAll();
            User = new User();
            SelectedTour = selectedTour;
            TourGuests = new List<Guest>();

            CheckPoints = SelectedTour.CheckPoints;
            Guests = _tourReservationRepository.GetReservationGuest(SelectedTour);
            MakeGuests();
        }

     

        public void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckPoints = SelectedTour.CheckPoints;
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;
            CheckCheckPoint(checkedCheckPoint);
            foreach(User guest in Guests)
            {
                Notification notification = new Notification();
                notification.TourId = SelectedTour.Id;
                notification.CheckPointId = checkedCheckPoint.Id;
                notification.GuestId = guest.Id;
                Notifications.Add(notification);
                _notificationRepository.Save(notification);
                // sada treba da azuriram tabelu za prikaz podataka
                MakeGuests();
                
            } 
        }
        

        

        public void MakeGuests()
        {
            foreach(User user in Guests)
            {
                foreach(Guest guest in _tourGuestsRepository.GetAll())
                {
                    if(user.Id == guest.Id)
                    {
                        TourGuests.Add(guest);
                    }
                }
            }
        }
        

        public void CheckCheckPoint(CheckPoint checkPoint)
        {
            foreach(CheckPoint check in CheckPoints)
            {
                if(check.Id == checkPoint.Id)
                {
                    check.IsChecked = true;
                    _checkPointRepository.Update(check);
                }
            }
        }
        public void UncheckCheckPoint(Tour tour)
        {
            foreach (CheckPoint check in tour.CheckPoints)
            {
                check.IsChecked = false;
                _checkPointRepository.Update(check);
            }
        }

        private void EndTour(object sender, RoutedEventArgs e)
        {
            SelectedTour.IsActive = false;
            _tourRepository.Update(SelectedTour);
            foreach (User guest in Guests)
            {
                _userRepository.Update(guest);
            }
            _notificationRepository.DeleteAll();
            UncheckCheckPoint(SelectedTour);
        }
    }
}
