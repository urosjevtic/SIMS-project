using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model.Notifications;
using InitialProject.Domain.RepositoryInterfaces.INotificationRepo;
using InitialProject.Service.ForumServices;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.NotificationServices
{
    public class OwnerNotificationService
    {
        private readonly IOwnerNotificationRepository _notificationRepository;
        private readonly AccommodationReservationRescheduleRequestService _reservationRescheduleService;
        private readonly UnratedGuestService _unratedGuestService;
        private readonly RenovationRecommendationService _renovationRecommendationService;
        private readonly ForumService _forumService;

        public OwnerNotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<IOwnerNotificationRepository>();
            _reservationRescheduleService = new AccommodationReservationRescheduleRequestService();
            _unratedGuestService = new UnratedGuestService();
            _renovationRecommendationService = new RenovationRecommendationService();
            _forumService = new ForumService();
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
            int currentRenovationSugestionsCount = _renovationRecommendationService.GetByOwnerId(ownerId).Count;
            int currentForumCount = _forumService.GetByOwnerId(ownerId).Count;

            return (currentRescheduleCount > notification.ReservationReschedulingCount ||
                    currentUnratedGuestsCount > notification.UnradtedGuestsCount ||
                    currentRenovationSugestionsCount > notification.RenovationRecommendationCount ||
                    currentForumCount > notification.ForumNotificationCount);
        }

        public void UpdateNewNotifications(int ownerId)
        {
            int currentRescheduleCount = _reservationRescheduleService.GetAllByOwnerId(ownerId).Count;
            int currentUnratedGuestsCount = _unratedGuestService.GetUnratedGuestsByOwnerId(ownerId).Count;
            int currentRenovationSugestionsCount = _renovationRecommendationService.GetByOwnerId(ownerId).Count;
            int currentForumCount = _forumService.GetByOwnerId(ownerId).Count;

            OwnerNotification notification = new OwnerNotification();
            notification.Owner.Id = ownerId;
            notification.ReservationReschedulingCount = currentRescheduleCount;
            notification.UnradtedGuestsCount = currentUnratedGuestsCount;
            notification.RenovationRecommendationCount = currentRenovationSugestionsCount;
            notification.ForumNotificationCount = currentForumCount;


            _notificationRepository.Update(notification);
        }
    }
}
