using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Notifications;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.INotificationRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.NotificationRepo
{
    public class OwnerNotificationRepositorty : IOwnerNotificationRepository
    {

        private const string FilePath = "../../../Resources/Data/ownerNotifications.csv";

        private readonly Serializer<OwnerNotification> _serializer;

        private List<OwnerNotification> _notifications;

        public OwnerNotificationRepositorty()
        {
            _serializer = new Serializer<OwnerNotification>();
            _notifications = _serializer.FromCSV(FilePath);
        }
        public List<OwnerNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public OwnerNotification GetByOwnerId(int ownerId)
        {
            _notifications = _serializer.FromCSV(FilePath);
            return _notifications.FirstOrDefault(n => n.Owner.Id == ownerId);
        }

        public void Save(OwnerNotification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public void Delete(OwnerNotification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Remove(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public void Update(OwnerNotification notification)
        {
            _notifications = _serializer.FromCSV(FilePath); 
            foreach (var note in _notifications)
            {
                if (note.Owner.Id == notification.Owner.Id)
                {
                    _notifications.Remove(note);
                    _notifications.Add(notification);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _notifications);
        }
    }
}
