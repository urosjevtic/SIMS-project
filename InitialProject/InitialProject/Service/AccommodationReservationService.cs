using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservationRepository;
        private AccommodationService _accommodationService;
        private UserService _userService;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationService = new AccommodationService();
            _userService = new UserService();
        }

        public List<AccommodationReservation> GetAll()
        {
            List< AccommodationReservation > accommodationReservations = new List < AccommodationReservation >();
            accommodationReservations = _accommodationReservationRepository.GetAll();
            BindAccommodationToReservations(accommodationReservations);
            BindUserToReservations(accommodationReservations);
            return accommodationReservations;
        }

        public void Save(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Save(reservation);
        }

        public AccommodationReservation Create(int Id, DateTime startDate, DateTime endDate, int userId, int accommodationId, int guestNumber)
        {
            AccommodationReservation newReservation = new AccommodationReservation();
            newReservation.Id = Id;
            newReservation.StartDate = startDate;
            newReservation.EndDate = endDate;
            newReservation.UserId = userId;
            newReservation.GuestNumber = guestNumber;
            newReservation.AccommodationId = accommodationId;

            return newReservation;
        }
        public List<AccommodationReservation> GetReservationsByAccommodationId(int id)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAll();
            foreach (AccommodationReservation accommodationReservation in allReservations)
            {
                if (accommodationReservation.AccommodationId == id)
                    reservations.Add(accommodationReservation);
            }

            return reservations;
        }

        private void BindAccommodationToReservations(List<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                foreach (var accommodation in _accommodationService.GetAll())
                {
                    if (accommodation.Id == reservation.AccommodationId)
                    {
                        reservation.Accommodation = accommodation;
                        break;
                    }
                }
            }
        }

        private void BindUserToReservations(List<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                foreach (var user in _userService.GetAll())
                {
                    if (user.Id == reservation.UserId)
                    {
                        reservation.User = user;
                        break;
                    }
                }
            }
        }

        public void Delete(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Delete(reservation);
        }
    }
}
