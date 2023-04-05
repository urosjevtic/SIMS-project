using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class UnratedGuestService
    {
        private readonly UnratedGuestRepository _unratedGuestRepository;
        private readonly AccommodationService _accommodationService;
        private readonly UserService _userService;
        public UnratedGuestService()
        {
            _unratedGuestRepository = new UnratedGuestRepository();
            _accommodationService = new AccommodationService();
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
                ConnectAccommodationToUnratedGuest(unratedGuest);
                ConnectUserToUnratedGuest(unratedGuest);
                unratedGuests.Add(unratedGuest);
            }

            RemoveUnratedGuestAfterFiveDays(unratedGuests);

            return unratedGuests;
        }

        private void ConnectAccommodationToUnratedGuest(UnratedGuest unratedGuest)
        {
            foreach (Accommodation accommodation in _accommodationService.GetAll())
            {
                if (accommodation.Id == unratedGuest.ReservedAccommodation.Id)
                {
                    unratedGuest.ReservedAccommodation = accommodation;
                    break;
                }
            }
        }
        private void ConnectUserToUnratedGuest(UnratedGuest unratedGuest)
        {
            unratedGuest.User = _userService.GetUserById(unratedGuest.User.Id);
        }
        private List<UnratedGuest> RemoveUnratedGuestAfterFiveDays(List<UnratedGuest> unratedGuests)
        {
            var today = DateTime.Now;
            for (int i = unratedGuests.Count - 1; i >= 0; i--)
            {
                UnratedGuest unratedGuest = unratedGuests[i];
                TimeSpan dateDifference = today - unratedGuest.ReservationEndDate;
                if (dateDifference.TotalDays > 5)
                {
                    _unratedGuestRepository.Remove(unratedGuest);
                    unratedGuests.Remove(unratedGuest);
                }
            }
            return unratedGuests;
        }

        public UnratedGuest Get(int id)
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
