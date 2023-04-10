using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service
{
    public class UnratedGuestService
    {
        private readonly IUnratedGuestRepository _unratedGuestRepository;
        private readonly AccommodationReservationService _reservationService;
        private readonly UserService _userService;
        public UnratedGuestService()
        {
            _unratedGuestRepository = Injector.Injector.CreateInstance<IUnratedGuestRepository>();
            _reservationService = new AccommodationReservationService();
            _userService = new UserService();
        }

        public List<UnratedGuest> GetAll()
        {
            return _unratedGuestRepository.GetAll();
        }

        public List<UnratedGuest> GetAllUnratedGuests()
        {
            List<UnratedGuest> unratedGuests = new List<UnratedGuest>();
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();
            foreach (UnratedGuest unratedGuest in allUnratedGuests)
            {
                ConnectReservationToUnratedGuest(unratedGuest);
                unratedGuests.Add(unratedGuest);
            }

            RemoveUnratedGuestAfterFiveDays(unratedGuests);

            return unratedGuests;
        }

        public List<UnratedGuest> GettUnratedGuestsByOwnerId(int id)
        {
            List<UnratedGuest> unratedGuestsByOwnerId = new List<UnratedGuest>();
            List<UnratedGuest> allUnratedGuests = _unratedGuestRepository.GetAll();

            foreach (UnratedGuest unratedGuest in allUnratedGuests)
            {
                ConnectReservationToUnratedGuest(unratedGuest);
            }

            foreach (var unratedGuest in allUnratedGuests)
            {
                if(unratedGuest.Reservation.Accommodation.Owner.Id == id)
                    unratedGuestsByOwnerId.Add(unratedGuest);

            }

            RemoveUnratedGuestAfterFiveDays(unratedGuestsByOwnerId);
            return unratedGuestsByOwnerId;
        }

        private void ConnectReservationToUnratedGuest(UnratedGuest unratedGuest)
        {
            foreach (AccommodationReservation reservation in _reservationService.GetAll())
            {
                if (reservation.Id == unratedGuest.Reservation.Id)
                {
                    unratedGuest.Reservation = reservation;
                    break;
                }
            }
        }

        private List<UnratedGuest> RemoveUnratedGuestAfterFiveDays(List<UnratedGuest> unratedGuests)
        {
            var today = DateTime.Now;
            for (int i = unratedGuests.Count - 1; i >= 0; i--)
            {
                UnratedGuest unratedGuest = unratedGuests[i];
                TimeSpan dateDifference = today - unratedGuest.Reservation.EndDate;
                if (dateDifference.TotalDays > 5)
                {
                    _unratedGuestRepository.Remove(unratedGuest);
                    unratedGuests.Remove(unratedGuest);
                }
            }
            return unratedGuests;
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
