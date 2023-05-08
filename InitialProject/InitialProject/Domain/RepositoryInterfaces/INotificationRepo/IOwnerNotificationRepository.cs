using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Notifications;

namespace InitialProject.Domain.RepositoryInterfaces.INotificationRepo
{
    public interface IOwnerNotificationRepository
    {
        List<OwnerNotification> GetAll();
        OwnerNotification GetByOwnerId(int ownerId);

        void Save(OwnerNotification notification);

        void Delete(OwnerNotification notification);

        void Update(OwnerNotification notification);
    }
}
