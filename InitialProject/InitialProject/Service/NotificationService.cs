using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model;

namespace InitialProject.Service
{

    public class NotificationService
    {

        public INotificationRepository _notificationRepository;


        public NotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<INotificationRepository>();

        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
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
        public bool Create(Tour tour, User user)
        {
            Notification notification = new Notification(tour, user);
            notification.Id = _notificationRepository.NextId();
            if (!IsNotificationSent(notification))
            {
                _notificationRepository.Save(notification);
                return true;
            }
            return false;
        }
        public bool IsNotificationSent(Notification n)
        {
            List<Notification> notificationList = _notificationRepository.GetAll();
            foreach (Notification notification in notificationList)
            {
                if (notification.TourId == n.TourId && notification.GuestId == n.GuestId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
