using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service.RatingServices
{
    public class UnratedGuestCleanupService
    {
        private readonly IUnratedGuestRepository _unratedGuestRepository;

        public UnratedGuestCleanupService(IUnratedGuestRepository unratedGuestRepository)
        {
            _unratedGuestRepository = unratedGuestRepository;
        }

        public List<UnratedGuest> RemoveUnratedGuestAfterFiveDays(List<UnratedGuest> unratedGuests)
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
    }
}

