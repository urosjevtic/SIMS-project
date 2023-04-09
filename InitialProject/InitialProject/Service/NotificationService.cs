using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService()
        {
            _notificationRepository = new NotificationRepository();
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
            foreach(Notification notification in notificationList)
            {
                if(notification.TourId == n.TourId && notification.GuestId == n.GuestId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
