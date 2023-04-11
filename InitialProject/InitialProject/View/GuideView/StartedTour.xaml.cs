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
using InitialProject.Domain.Model;
using InitialProject.Model;

namespace InitialProject.View
{

    public partial class StartedTour : Window
    {
               
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly TourGuestRepository _tourGuestsRepository;
        private readonly TourRepository _tourRepository;
      
        
        public List<CheckPoint> CheckedCheckPoints { get; set; }
        public List<User> TourReservations { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public List<TourGuest> TourGuests { get; set; }
        public List<User> Guests { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        
        public StartedTour(Tour selectedTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _notificationRepository = new NotificationRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourGuestsRepository = new TourGuestRepository(); 
            _checkPointRepository = new CheckPointRepository();

            CheckedCheckPoints = new List<CheckPoint>();
            Notifications = _notificationRepository.GetAll();
            User = new User();
            SelectedTour = selectedTour;
            TourGuests = new List<TourGuest>();

            CheckPoints = SelectedTour.CheckPoints;
            Guests = _tourReservationRepository.GetReservationGuest(SelectedTour);  // svi gosti na ovoj turi
            MakeGuestsFirst();
        }

     

        public void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckPoints = SelectedTour.CheckPoints;
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;
            CheckCheckPoint(checkedCheckPoint);
            SendNotification(checkedCheckPoint);
            // sada treba da azuriram tabelu za prikaz podataka
             //MakeGuests(checkedCheckPoint);
            if (CheckPoints.Last().Id == checkedCheckPoint.Id)
            {
                EndTour();
            } 
        }

        private void SendNotification(CheckPoint checkedCheckPoint)
        {
            foreach (User guest in Guests)
            {
                foreach(TourGuest tourGuest in _tourGuestsRepository.GetAll())
                {
                    if(tourGuest.Id == guest.Id && !(tourGuest.Presence == UserPresence.Yes))
                    {
                        Notification notification = new Notification();
                        notification.TourId = SelectedTour.Id;
                        notification.CheckPointId = checkedCheckPoint.Id;
                        notification.GuestId = guest.Id;
                        Notifications.Add(notification);
                        _notificationRepository.Save(notification);
                    }
                }
               
            }
        }
        
 
        public void MakeGuestsFirst()
        {
            foreach (User user in Guests)
            {
                foreach (TourGuest guest in _tourGuestsRepository.GetAll())
                {
                    if (user.Id == guest.Id)
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

        private void EndTourClick(object sender, RoutedEventArgs e)
        {
            EndTour();
        }
        private void EndTour()
        {
            SelectedTour.IsActive = false;
            _tourRepository.Update(SelectedTour);
            foreach (TourGuest guest in TourGuests)
            {
                guest.Presence = UserPresence.Unknown;
                guest.CheckPointName = "";
                _tourGuestsRepository.Update(guest);
            }
            _notificationRepository.DeleteAll();
            UncheckCheckPoint(SelectedTour);
            MessageBox.Show("Tura je uspjesno zavrsena!","", MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
