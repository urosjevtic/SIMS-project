using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model;
using InitialProject.ViewModels.Guest2ViewModel;

namespace InitialProject.Service
{

    public class NotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private ShortTourRequestService _shortTourRequestService;
        private TourService _tourService;
        private readonly LocationService _locationService;
        public NotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<INotificationRepository>();
            _shortTourRequestService = new ShortTourRequestService();
            _tourService = new TourService();
            _locationService = new LocationService();
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }
        public List<Notification> GetAllById(int id)
        {
            return _notificationRepository.GetAllById(id);
        }
        public Notification FindById(int id)
        {
            return _notificationRepository.FindById(id);
        }

        public void Save(Notification notification)
        {
            _notificationRepository.Save(notification);
        }

        public int NextId()
        {
            return _notificationRepository.NextId();
        }

        public void Delete(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }
        public void SaveAll(List<Notification> notifications)
        {
            _notificationRepository.SaveAll(notifications);
        }
        public void Update(Notification notification)
        {
            _notificationRepository.Update(notification);
        }
        public void DeleteAll()
        {
            _notificationRepository.DeleteAll();
        }
        public void SendNotifications(Tour tour)
        {
            int i = 0;
            foreach(ShortTourRequest shortTour in _shortTourRequestService.GetAll())
            {
                if(((shortTour.Country == tour.Location.Country && shortTour.City == tour.Location.City) || shortTour.Language == tour.Language) && shortTour.Status != RequestStatus.Accepted)
                {
                    if(i == 0)
                    {
                        MakeNotification(tour.Id, shortTour.IdUser);
                        i++;
                    }
                }
            }
        }
        public void MakeNotification(int tourId, int IdUser)
        {
            Notification notification = new Notification();
            notification.GuestId = IdUser;
            notification.TourId = tourId;
            notification.CheckPointId = -1;
            notification.IsGoing = false;
            Save(notification);
        }
        public List<TourNotification> GetToursForNotifications(int id)
        {
            List<TourNotification> list = new List<TourNotification>();
            foreach(Notification notification in _notificationRepository.GetAllById(id))
            {
                foreach(Tour tour in _tourService.GetAll())
                {
                    foreach(Location location in _locationService.GetLocations())
                    {
                        if(location.Id == tour.Location.Id)
                        {
                            tour.Location.Country = location.Country;
                            tour.Location.City = location.City;
                            break;
                        }
                    }
                    if(notification.TourId == tour.Id)
                    {
                        TourNotification tourNotification = new TourNotification(tour, notification);
                        list.Add(tourNotification);
                    }
                }
            }
            return list;
        }
    }
}