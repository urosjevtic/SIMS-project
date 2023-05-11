using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Notifications;
using InitialProject.Domain.RepositoryInterfaces.INotificationRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.NotificationServices
{
    public class OwnerNotificationService
    {
        private readonly IOwnerNotificationRepository _notificationRepository;
        private readonly AccommodationReservationRescheduleRequestService _reservationRescheduleService;
        private readonly UnratedGuestService _unratedGuestService;

        public OwnerNotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<IOwnerNotificationRepository>();
            _reservationRescheduleService = new AccommodationReservationRescheduleRequestService();
            _unratedGuestService = new UnratedGuestService();
        }

        public List<OwnerNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public OwnerNotification GetByOwnerId(int ownerId)
        {
            return _notificationRepository.GetByOwnerId(ownerId);
        }

        public void Save(OwnerNotification notification)
        {
            _notificationRepository.Save(notification);
        }

        public void Delete(OwnerNotification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public void Update(OwnerNotification notification)
        {
            _notificationRepository.Update(notification);
        }

        public bool HasNewNotifications(int ownerId)
        {
            OwnerNotification notification = _notificationRepository.GetByOwnerId(ownerId);
            int currentRescheduleCount = _reservationRescheduleService.GetAllByOwnerId(ownerId).Count;
            int currentUnratedGuestsCount = _unratedGuestService.GetUnratedGuestsByOwnerId(ownerId).Count;

            return (currentRescheduleCount > notification.ReservationReschedulingCount ||
                    currentUnratedGuestsCount > notification.UnradtedGuestsCount);
        }

        public void UpdateNewNotifications(int ownerId)
        {
            int currentRescheduleCount = _reservationRescheduleService.GetAllByOwnerId(ownerId).Count;
            int currentUnratedGuestsCount = _unratedGuestService.GetUnratedGuestsByOwnerId(ownerId).Count;

            OwnerNotification notification = new OwnerNotification
            {
                ReservationReschedulingCount = currentRescheduleCount,
                UnradtedGuestsCount = currentUnratedGuestsCount
            };

            _notificationRepository.Update(notification);
        }
    }
}
