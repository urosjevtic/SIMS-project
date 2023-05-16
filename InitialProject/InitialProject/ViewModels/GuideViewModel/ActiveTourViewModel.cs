using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service;
using InitialProject.Repository;
using InitialProject.Model;
using System.Windows.Input;
using InitialProject.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace InitialProject.ViewModels.GuideViewModel
{
   
    public class ActiveTourViewModel : BaseViewModel
    {
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourGuestRepository _tourGuestsRepository;
        private readonly NotificationService _notificationService;
        private readonly CheckPointService _tourCheckPointService;
        private readonly TourService _tourService;


        public List<CheckPoint> CheckedCheckPoints { get; set; }
        public List<User> TourReservations { get; set; }
        public List<Notification> Notifications { get; set; }
        public ObservableCollection<CheckPoint> CheckPoints { get; set; }
        public ObservableCollection<TourGuest> TourGuests { get; set; }
        public string TourName { get; set; }    
        public string TourLocation { get; set; }    
        public List<User> Guests { get; set; }
        public Tour ActiveTour { get; set; }
        public User User { get; set; }

        public ActiveTourViewModel(User user)
        {
            _tourReservationRepository = new TourReservationRepository();
            _tourGuestsRepository = new TourGuestRepository();
            _tourCheckPointService = new CheckPointService();
            _notificationService = new NotificationService();
            _tourService = new TourService();
            CheckedCheckPoints = new List<CheckPoint>();
            Notifications = _notificationService.GetAll();
            User = user;
            TourGuests = new ObservableCollection<TourGuest>();
            ActiveTour = new Tour();
            ActiveTour = FindActiveTour();
            
            CheckPoints = new ObservableCollection<CheckPoint>(ActiveTour.CheckPoints);
            TourName = ActiveTour.Name;
            TourLocation = ActiveTour.Location.ToString();
            Guests = _tourReservationRepository.GetReservationGuest(ActiveTour);  // svi gosti na ovoj turi
            MakeGuestsFirst();
        }

       
        public Tour FindActiveTour()
        {
            Tour tour = new Tour();
            List<Tour> allTours = _tourService.FindActiveTours(User);
            tour = allTours.FirstOrDefault();
            return tour;
        }


        public void SendNotification(CheckPoint checkedCheckPoint)
        {
            foreach (User guest in Guests)
            {
                foreach (TourGuest tourGuest in _tourGuestsRepository.GetAll())
                {
                    if (tourGuest.Id == guest.Id && !(tourGuest.Presence == UserPresence.Yes))
                    {
                        Notification notification = new Notification();
                        notification.TourId = ActiveTour.Id;
                        notification.CheckPointId = checkedCheckPoint.Id;
                        notification.GuestId = guest.Id;
                        Notifications.Add(notification);
                        _notificationService.Save(notification);
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
             checkPoint.IsChecked = true;
             _tourCheckPointService.Update(checkPoint);
         
        }
        public void UncheckCheckPoint(Tour tour)
        {
            foreach (CheckPoint check in tour.CheckPoints)
            {
                check.IsChecked = false;
                _tourCheckPointService.Update(check);
            }
        }

      
        public ICommand EndTourCommand => new RelayCommand(EndTour);
        public void EndTour()
        {
            ActiveTour.IsActive = false;
            _tourService.Update(ActiveTour);
            foreach (TourGuest guest in TourGuests)
            {
                guest.Presence = UserPresence.Unknown;
                guest.CheckPointName = "";
                _tourGuestsRepository.Update(guest);
            }
            _notificationService.DeleteAll();
            UncheckCheckPoint(ActiveTour);
            MessageBox.Show("Tura je uspjesno zavrsena!", "", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
