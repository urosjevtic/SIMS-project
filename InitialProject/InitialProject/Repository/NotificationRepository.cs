using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Serializer;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";

        private readonly Serializer<Domain.Model.Notification> _serializer;

        private List<Domain.Model.Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Domain.Model.Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public List<Domain.Model.Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Domain.Model.Notification FindById(int id)
        {
            Domain.Model.Notification notification = new Domain.Model.Notification();
            List<Domain.Model.Notification> notifications = new List<Domain.Model.Notification>();
            notifications = GetAll();
            foreach (Domain.Model.Notification not in notifications)
            {
                if (not.Id == id)
                {
                    notification = not;
                    break;
                }
            }
            return notification;
        }
        public List<Domain.Model.Notification> GetAllById(int id)
        {
            List<Domain.Model.Notification> notifications = new List<Domain.Model.Notification>();
            foreach (Domain.Model.Notification not in GetAll())
            {
                if (not.GuestId == id && not.IsChecked == false)
                {
                    notifications.Add(not);
                }
            }
            return notifications;
        }

        public void Save(Domain.Model.Notification notification)
        {
            notification.Id = NextId();
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(c => c.Id) + 1;
        }

        public void Delete(Domain.Model.Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Domain.Model.Notification founded = _notifications.Find(c => c.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }
        public void SaveAll(List<Domain.Model.Notification> notifications)
        {
            _serializer.ToCSV(FilePath, notifications);
        }
        public void Update(Domain.Model.Notification notification)
        {
            Domain.Model.Notification newNotifcation = _notifications.Find(p1 => p1.Id == notification.Id);
            newNotifcation.Id = notification.Id;
            newNotifcation.GuestId = notification.GuestId;
            newNotifcation.TourId = notification.TourId;
            newNotifcation.IsGoing = notification.IsGoing;
            newNotifcation.IsChecked = notification.IsChecked;
            SaveAll(_notifications);

        }
        public void DeleteAll()
        {
            _notifications = _serializer.FromCSV(FilePath);
            foreach(Domain.Model.Notification notification in _notifications)
            {
                Delete(notification);
            }
        }
    }
}
