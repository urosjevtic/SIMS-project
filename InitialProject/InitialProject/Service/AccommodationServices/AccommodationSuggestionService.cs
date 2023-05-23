using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.AccommodationServices
{
    public class AccommodationSuggestionService
    {

        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationSuggestionService()
        {
            _accommodationReservationService = new AccommodationReservationService();
            SortAccommodationByLocation();
        }


        private Dictionary<Location, List<Accommodation>> SortAccommodationByLocation()
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            reservations = _accommodationReservationService.GetAll();
            Dictionary<Location, List<Accommodation>> accommdoationsByLocation = new Dictionary<Location, List<Accommodation>>();

            foreach (var reservation in reservations)
            {
                if (!accommdoationsByLocation.ContainsKey(reservation.Accommodation.Location))
                {
                    accommdoationsByLocation[reservation.Accommodation.Location] = new List<Accommodation>()
                    {
                        reservation.Accommodation
                    };
                }
                else
                {
                    accommdoationsByLocation[reservation.Accommodation.Location].Add(reservation.Accommodation);
                }

            }

            return accommdoationsByLocation;
        }
    }
}
