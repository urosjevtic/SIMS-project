using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service.RatingServices;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service
{
    public class UnratedGuestService
    {
        private readonly IUnratedGuestRepository _unratedGuestRepository;
        private readonly AccommodationReservationService _reservationService;
        private readonly UserService _userService;
        private readonly UnratedGuestCleanupService _unratedGuestCleanupService;
        public UnratedGuestService()
        {
            _unratedGuestRepository = Injector.Injector.CreateInstance<IUnratedGuestRepository>();
            _reservationService = new AccommodationReservationService();
            _userService = new UserService();
            _unratedGuestCleanupService = new UnratedGuestCleanupService(_unratedGuestRepository);
        }

        public List<UnratedGuest> GetAll()
        {
            return _unratedGuestRepository.GetAll();
        }

        public List<UnratedGuest> GetAllUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = GetUnratedGuests();
            _unratedGuestCleanupService.RemoveUnratedGuestAfterFiveDays(unratedGuests);
            return unratedGuests;
        }

        public List<UnratedGuest> GetUnratedGuestsByOwnerId(int id)
        {
            List<UnratedGuest> allUnratedGuests = GetUnratedGuests();
            List<UnratedGuest> unratedGuestsByOwnerId = FilterUnratedGuestByOwner(id, allUnratedGuests);
            _unratedGuestCleanupService.RemoveUnratedGuestAfterFiveDays(unratedGuestsByOwnerId);
            return unratedGuestsByOwnerId;
        }

        private List<UnratedGuest> GetUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = new List<UnratedGuest>();
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();
            foreach (UnratedGuest unratedGuest in allUnratedGuests)
            {
                ConnectReservationToUnratedGuest(unratedGuest);
                unratedGuests.Add(unratedGuest);
            }
            return unratedGuests;
        }

        private List<UnratedGuest> FilterUnratedGuestByOwner(int id, List<UnratedGuest> allUnratedGuests)
        {
            if(allUnratedGuests == null)
                return new List<UnratedGuest>();

            List<UnratedGuest> unratedGuestsByOwnerId = new List<UnratedGuest>();
            foreach (var unratedGuest in allUnratedGuests)
            {
                if (unratedGuest.Reservation.Accommodation.Owner.Id == id)
                {
                    unratedGuestsByOwnerId.Add(unratedGuest);
                }
            }
            return unratedGuestsByOwnerId;
        }


        private void ConnectReservationToUnratedGuest(UnratedGuest unratedGuest)
        {
            unratedGuest.Reservation = _reservationService.GetAll().FirstOrDefault(r => r.Id == unratedGuest.Reservation.Id);
        }



        public UnratedGuest GetById(int id)
        {
            List<UnratedGuest> unratedGuests = GetAll();
            return unratedGuests.FirstOrDefault(r => r.Id == id);
        }

        public void Remove(UnratedGuest unratedGuest)
        {
            _unratedGuestRepository.Remove(unratedGuest);
        }
    }
}
