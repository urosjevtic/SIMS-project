using InitialProject.Service.ReservationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Reservations
{
    public class SuperGuestService
    {
        private readonly AccommodationReservationService _accommodationReservationService;

        public SuperGuestService()
        {
            _accommodationReservationService = new AccommodationReservationService();
        }
        public bool IsSuperGuest(int guestId)
        {
            if (ReservationsCount(guestId) > 5)
                return true;
            return false;
        }
        private int ReservationsCount(int guestId)
        {
            List<AccommodationReservation> reservation = _accommodationReservationService.GetAllReservationByGuestId(guestId);
            return reservation.Count();
        }

    }
}
