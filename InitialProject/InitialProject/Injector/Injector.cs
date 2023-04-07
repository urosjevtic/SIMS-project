﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Repository;
using InitialProject.Repository.ReservationRepo;

namespace InitialProject.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IImageRepository), new ImageRepository() },
            { typeof(IUserRepository), new UserRepository()}, 
            {typeof(IAccommodationRepository), new AccommodationRepository()}, 
            {typeof(ILocationRepository), new LocationRepository()},
            {typeof(IRatedGuestRepository), new RatedGuestRepository()},
            {typeof(IAccommodationReservationRescheduleRequestRepository), new AccommodationReservationRescheduleRequestRepository()},
            {typeof(IUnratedGuestRepository), new UnratedGuestRepository()},
            {typeof(IDeclinedAccommodationReservationRescheduleRequestRepository), new DeclinedAccommodationReservationRescheduleRequestRepository()}

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
