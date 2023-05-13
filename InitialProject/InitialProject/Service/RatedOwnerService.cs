using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service.ReservationServices;
using System.Collections.Generic;
using System.Linq;


namespace InitialProject.Service
{
    public class RatedOwnerService
    {
        private readonly IRatedOwnerRepository _ratedOwnerRepository;

        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly RatedGuestService _ratedGuestService;
        public RatedOwnerService()
        {
            _ratedOwnerRepository = Injector.Injector.CreateInstance<IRatedOwnerRepository>();
            _accommodationReservationService = new AccommodationReservationService();
            _ratedGuestService = new RatedGuestService();
        }

        public List<RatedOwner> GetAll()
        {
            return _ratedOwnerRepository.GetAll();
        }

        public RatedOwner Get(int id)
        {
            List<RatedOwner> ratedOwners = GetAll();
            return ratedOwners.FirstOrDefault(r => r.Id == id);
        }

        public List<RatedOwner> GetFilteredRatingsByAccommodationId(int accommodationId)
        {
            return FilterRatedGuestRatings(accommodationId);
        }

        private List<RatedOwner> FilterRatedGuestRatings(int accommodationId)
        {
            List<RatedOwner> filteredRatings = new List<RatedOwner>();
            List<RatedOwner> allRatings = _ratedOwnerRepository.GetAll();
            BindReservationToRating(allRatings);
            foreach (var rating in allRatings)
            {
                if (rating.Reservation.Accommodation.Id == accommodationId && CheckIfGuestRated(rating))
                    filteredRatings.Add(rating);
            }

            return filteredRatings;
        }


        private bool CheckIfGuestRated(RatedOwner rating)
        {
            foreach (var ratedGuest in _ratedGuestService.GetAll())
            {
                if (ratedGuest.Reservation.Id == rating.Reservation.Id)
                    return true;
            }
            return false;
        }


        public List<RatedOwner> GetByOwnersId(int id)
        {
            List<RatedOwner> allRatings = _ratedOwnerRepository.GetAll();
            List<RatedOwner> ratings = new List<RatedOwner>();
            BindReservationToRating(allRatings);
            foreach (var rating in allRatings)
            {
                if (rating.Reservation.Accommodation.Owner.Id == id)
                    ratings.Add(rating);
            }

            return ratings;
        }

        private void BindReservationToRating(List<RatedOwner> ratings)
        {
            foreach (var rating in ratings)
            {
                foreach (var reservation in _accommodationReservationService.GetAll())
                {
                    if (rating.Reservation.Id == reservation.Id)
                    {
                        rating.Reservation = reservation;
                        break; //mozda ovo treba izbrisati
                    }
                }
            }
        }

        public void Save(RatedOwner owner)
        {
            _ratedOwnerRepository.Save(owner);
        }
    }
}