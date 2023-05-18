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

        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public List<Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Notification FindById(int id)
        {
            Notification notification = new Notification();
            List<Notification> notifications = new List<Notification>();
            notifications = GetAll();
            foreach (Notification not in notifications)
            {
                if (not.Id == id)
                {
                    notification = not;
                    break;
                }
            }
            return notification;
        }
        public List<Notification> GetAllById(int id)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (Notification not in GetAll())
            {
                if (not.GuestId == id && not.IsChecked == false)
                {
                    notifications.Add(not);
                }
            }
            return notifications;
        }

        public void Save(Notification notification)
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

        public void Delete(Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification founded = _notifications.Find(c => c.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }
        public void SaveAll(List<Notification> notifications)
        {
            _serializer.ToCSV(FilePath, notifications);
        }
        public void Update(Notification notification)
        {
            Notification newNotifcation = _notifications.Find(p1 => p1.Id == notification.Id);
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
            foreach(Notification notification in _notifications)
            {
                Delete(notification);
            }
        }
    }
}
