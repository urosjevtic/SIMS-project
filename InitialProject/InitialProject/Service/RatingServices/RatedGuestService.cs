using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service
{
    public class RatedGuestService
    {
        private readonly IRatedGuestRepository _ratedGuestRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly RatedOwnerService _ratedOwnerService;
        private readonly RatedGuestService _ratedGuestService;

        public RatedGuestService()
        {
            _ratedGuestRepository = Injector.Injector.CreateInstance<IRatedGuestRepository>();
            _accommodationReservationService = new AccommodationReservationService();
           //_ratedOwnerService = new RatedOwnerService();
        }

        public List<RatedGuest> GetAll()
        {
            return _ratedGuestRepository.GetAll();
        }
       public List<RatedGuest> GetRatedGuests()
        {
            List<RatedGuest> ratedGuests = new List<RatedGuest>();
            List<RatedGuest>allRatedGuest=_ratedGuestRepository.GetAll();
           foreach(RatedGuest ratedGuest in allRatedGuest)
            {
                //BindReservationToRating(ratedGuests);
                ConnectReservationToRating(ratedGuest);
                ratedGuests.Add(ratedGuest);
            }
            return ratedGuests;
        }

        public void ConnectReservationToRating(RatedGuest ratedGuest)
        {
            foreach (AccommodationReservation reservation in _accommodationReservationService.GetAll())
            {
                if (reservation.Id == ratedGuest.Reservation.Id)
                {
                    ratedGuest.Reservation = reservation;
                    //break;
                }
            }
        }

        public RatedGuest Get(int id)
        {
            List<RatedGuest> ratedGuests = GetAll();
            return ratedGuests.FirstOrDefault(r => r.Id == id);
        }

        //public List<RatedGuest> GetFilteredRatingsByAccommodationId()
        //{
        //    return FilterRatedOwnerRatings();
        //}

        //private List<RatedGuest> FilterRatedOwnerRatings(int accommodationId)
        //{
        //    List<RatedGuest> filteredRatings = new List<RatedGuest>();
        //    List<RatedGuest> allRatings = _ratedGuestRepository.GetAll();
        //    BindReservationToRating(allRatings);
        //    foreach (var rating in allRatings)
        //    {
        //        if (rating.Reservation.Accommodation.Id == accommodationId && CheckIfOwnerRated(rating))
        //            filteredRatings.Add(rating);
        //    }

        //    return filteredRatings;
        //}
        //private List<RatedGuest> FilterRatedOwnerRatings()
        //{
        //    List<RatedGuest> filteredRatings = new List<RatedGuest>();
        //    List<RatedGuest> allRatings = _ratedGuestRepository.GetAll();
        //    BindReservationToRating(allRatings);
        //    foreach (var rating in allRatings)
        //    {
        //        if(CheckIfOwnerRated(rating))
        //            filteredRatings.Add(rating);
        //    }

        //    return filteredRatings;
        //}

        //private bool CheckIfOwnerRated(RatedGuest rating)
        //{
        //    foreach (var ratedOwner in _ratedOwnerService.GetAll())
        //    {
        //        if (ratedOwner.Reservation.Id == rating.Reservation.Id)
        //            return true;
        //    }
        //    return false;
        //}
        //private bool CheckIfOwnerRated(List<RatedGuest> rating)
        //{
        //    foreach (var ratedOwner in _ratedOwnerService.GetAll())
        //    {
        //        foreach ()
        //        {
        //        if (ratedOwner.Reservation.Id == rating.Reservation.Id)
        //            return true;

        //        }
        //    }
        //    return false;
        //}

        public List<RatedGuest> Rating()
        {
            foreach (var owner in _ratedOwnerService.GetAll())
            {
                foreach (var guest in _ratedGuestService.GetAll())
                {
                    if (owner.Reservation.Id == guest.Reservation.Id)
                    {
                        Rating().Add(guest);
                    }
                }
            }
            return Rating();
        }


        private void BindReservationToRating(List<RatedGuest> ratings)
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

        public void Save(RatedGuest guest)
        {
            _ratedGuestRepository.Save(guest);
        }

    }
}
