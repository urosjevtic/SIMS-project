using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRepo;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Domain.RepositoryInterfaces.IUsersRepo;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRenovationRepo;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Repository.StatisticRepo;
using InitialProject.Repository.UserRepo;

namespace InitialProject.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            {typeof(IImageRepository), new ImageRepository()},
            {typeof(IUserRepository), new UserRepository()}, 
            {typeof(IAccommodationRepository), new AccommodationRepository()}, 
            {typeof(ILocationRepository), new LocationRepository()},
            {typeof(IRatedGuestRepository), new RatedGuestRepository()},
            {typeof(IUnratedGuestRepository), new UnratedGuestRepository()},
            {typeof(IDeclinedAccommodationReservationRescheduleRequestRepository), new DeclinedAccommodationReservationRescheduleRequestRepository()},
            {typeof(IRatedOwnerRepository), new RatedOwnerRepository()},
            {typeof(IOwnerRepository), new OwnerRepository()},
            {typeof(IRatedGuideTourRepository), new RatedGuideTourRepository()},
            {typeof(IVoucherRepository), new VoucherRepository()},
            {typeof(ITourRepository), new TourRepository()},
            {typeof(ITourGuestRepository), new TourGuestRepository()},
            {typeof(ITourReservationRepository), new TourReservationRepository()},
            {typeof(INotificationRepository), new NotificationRepository()},
            {typeof(ICheckPointRepository), new CheckPointRepository()},
            {typeof(IGuestsCheckPointRepository), new GuestsCheckPointRepository()},
            {typeof(IAccommodationReservationRescheduleRequestRepository), new AccommodationReservationRescheduleRequestRepository()},
            {typeof(INotesRepository), new NotesRepository()},
            {typeof(IYearlyAccommodationStatisticsRepository), new YearlyAccommodationStatisticRepository()},
            {typeof(IMonthlyAccommodationStatisticRepository), new MonthlyAccommodationStatisticRepository()},
            {typeof(IRenovationRepository), new RenovationRepository()}

            // Add more implementations here
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
