using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service.ReservationServices;

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

        public List<RatedOwner> GetByAccommodationId(int id)
        {
            List<RatedOwner> allRatings = _ratedOwnerRepository.GetAll();
            List<RatedOwner> ratings = new List<RatedOwner>();
            BindReservationToRating(allRatings);
            foreach (var rating in allRatings)
            {
                if(rating.Reservation.Accommodation.Id == id && CheckIfGuestRated(rating))
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
                        break;
                    }
                }
            }
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

        public void Save(RatedOwner owner)
        {
            _ratedOwnerRepository.Save(owner);
        }
    }
}