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
    public class UnratedOwnerService
    {
        private readonly IUnratedOwnerRepository _unratedOwnerRepository;
        private readonly AccommodationReservationService _reservationService;
        private readonly UserService _userService;
        public UnratedOwnerService()
        {
            _unratedOwnerRepository = Injector.Injector.CreateInstance<IUnratedOwnerRepository>();
            _reservationService = new AccommodationReservationService();
            _userService = new UserService();
        }

        public List<UnratedOwner> GetAll()
        {
            return _unratedOwnerRepository.GetAll();
        }

        public List<UnratedOwner> GetAllUnratedOwners()
        {
            List<UnratedOwner> unratedOwners = new List<UnratedOwner>();
            List<UnratedOwner> allUnratedOwners = _unratedOwnerRepository.GetAll();
            foreach (UnratedOwner unratedOwner in allUnratedOwners)
            {
                ConnectReservationToUnratedOwner(unratedOwner);
                unratedOwners.Add(unratedOwner);
            }

            RemoveUnratedOwnerAfterFiveDays(unratedOwners);

            return unratedOwners;
        }

        public List<UnratedOwner> GetUnratedOwnerByGuestId(int id)
        {
            List<UnratedOwner> unratedOwnersByGuestId = new List<UnratedOwner>();
            List<UnratedOwner> allUnratedOwners = _unratedOwnerRepository.GetAll();

            foreach (UnratedOwner unratedOwner in allUnratedOwners)
            {
                ConnectReservationToUnratedOwner(unratedOwner);
            }

            foreach (var unratedOwner in allUnratedOwners)
            {
                if (unratedOwner.Reservation.Accommodation.Owner.Id == id)
                    unratedOwnersByGuestId.Add(unratedOwner);

            }

            RemoveUnratedOwnerAfterFiveDays(unratedOwnersByGuestId);
            return unratedOwnersByGuestId;
        }

        private void BindResrvationToUnratedOwner(List<UnratedOwner> owners)
        {
            var reservationsById = _reservationService.GetAll().ToDictionary(a => a.Id);

            foreach (var owner in owners)
            {
                if (reservationsById.TryGetValue(owner.Reservation.Id, out var reservation))
                {
                    owner.Reservation = reservation;
                }
            }
        }

        private void ConnectReservationToUnratedOwner(UnratedOwner unratedOwner)
        {
            foreach (AccommodationReservation reservation in _reservationService.GetAll())
            {
                if (reservation.Id == unratedOwner.Reservation.Id)
                {
                    unratedOwner.Reservation = reservation;
                    //break;
                }
            }
        }

        private List<UnratedOwner> RemoveUnratedOwnerAfterFiveDays(List<UnratedOwner> unratedOwners)
        {
            var today = DateTime.Now;
            for (int i = unratedOwners.Count - 1; i >= 0; i--)
            {
                UnratedOwner unratedOwner = unratedOwners[i];
                TimeSpan dateDifference = today - unratedOwner.Reservation.EndDate;
                if (dateDifference.TotalDays > 5)
                {
                    _unratedOwnerRepository.Remove(unratedOwner);
                    unratedOwners.Remove(unratedOwner);
                }
            }
            return unratedOwners;
        }

        public UnratedOwner GetById(int id)
        {
            List<UnratedOwner> unratedOwners = GetAll();
            return unratedOwners.FirstOrDefault(r => r.Id == id);
        }

        public void Remove(UnratedOwner unratedOwner)
        {
            _unratedOwnerRepository.Remove(unratedOwner);
        }
    }
}