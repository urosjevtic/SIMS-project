using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRepo;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.View;


namespace InitialProject.Service.ReservationServices
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservationRepository;
        private AccommodationService _accommodationService;
        private UserService _userService;

        public AccommodationReservationService()
        {
           // _accommodationReservationRepository = (AccommodationReservationRepository?)Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationService = new AccommodationService();
            _userService = new UserService();
        }

        public List<Domain.Model.Reservations.AccommodationReservation> GetAll()
        {
            List<Domain.Model.Reservations.AccommodationReservation> accommodationReservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            accommodationReservations = _accommodationReservationRepository.GetAll();
            BindAccommodationToReservations(accommodationReservations);
            BindUserToReservations(accommodationReservations);
            return accommodationReservations;
        }

        public void Save(Domain.Model.Reservations.AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Save(reservation);
        }

        public void Update(Domain.Model.Reservations.AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Update(reservation);
        }
        public Domain.Model.Reservations.AccommodationReservation Create(int Id, DateTime startDate, DateTime endDate, int userId, int accommodationId, int guestNumber)
        {
            Domain.Model.Reservations.AccommodationReservation newReservation = new Domain.Model.Reservations.AccommodationReservation();
            newReservation.Id = Id;
            newReservation.StartDate = startDate;
            newReservation.EndDate = endDate;
            newReservation.UserId = userId;
            newReservation.GuestNumber = guestNumber;
            newReservation.AccommodationId = accommodationId;

            return newReservation;
        }
        public List<Domain.Model.Reservations.AccommodationReservation> GetReservationsByAccommodationId(int id)
        {
            List<Domain.Model.Reservations.AccommodationReservation> reservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            List<Domain.Model.Reservations.AccommodationReservation> allReservations = _accommodationReservationRepository.GetAll();
            foreach (Domain.Model.Reservations.AccommodationReservation accommodationReservation in allReservations)
            {
                if (accommodationReservation.AccommodationId == id)
                    reservations.Add(accommodationReservation);
            }

            return reservations;
        }

        public List<Domain.Model.Reservations.AccommodationReservation>GetReservationByOwnerId(int id)
        {
            List<Domain.Model.Reservations.AccommodationReservation> reservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            List<Domain.Model.Reservations.AccommodationReservation> allreservations = _accommodationReservationRepository.GetAll();

            BindAccommodationToReservations(allreservations);
            BindUserToReservations(allreservations);

            reservations = allreservations.Where(r => r.Accommodation.Owner.Id == id).ToList();

            return reservations;
        }

        public List<Domain.Model.Reservations.AccommodationReservation> GetAllReservationByGuestId(int guestId)
        {
            List<Domain.Model.Reservations.AccommodationReservation> allReservation = GetAll();
            List<Domain.Model.Reservations.AccommodationReservation> reservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            foreach (Domain.Model.Reservations.AccommodationReservation reservation in allReservation)
            {
                if (reservation.User.Id == guestId)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }


        private void BindAccommodationToReservations(List<Domain.Model.Reservations.AccommodationReservation> reservations)

        {
            var accommodationsById = _accommodationService.GetAll().ToDictionary(a => a.Id);

            foreach (var reservation in reservations)
            {
                if (accommodationsById.TryGetValue(reservation.AccommodationId, out var accommodation))
                {
                    reservation.Accommodation = accommodation;
                }
            }
        }



        private void BindUserToReservations(List<Domain.Model.Reservations.AccommodationReservation> reservations)
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

        public void Delete(Domain.Model.Reservations.AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Delete(reservation);
        }

        public List<Domain.Model.Reservations.AccommodationReservation> GetPastReservations()
        {
            List<Domain.Model.Reservations.AccommodationReservation> pastReservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            List<Domain.Model.Reservations.AccommodationReservation> allAccommodationReservations = GetAll();
            BindAccommodationToReservations(allAccommodationReservations);

            foreach (Domain.Model.Reservations.AccommodationReservation reservation in allAccommodationReservations)
            {
                if (reservation.EndDate < DateTime.Now)
                {
                    pastReservations.Add(reservation);
                }
            }

            return pastReservations;
        }
        
        public List<Domain.Model.Reservations.AccommodationReservation> GetFutureReservations()
        {
            List<Domain.Model.Reservations.AccommodationReservation> futureReservations = new List<Domain.Model.Reservations.AccommodationReservation>();
            List<Domain.Model.Reservations.AccommodationReservation> allAccommodationReservations = GetAll();

            foreach (Domain.Model.Reservations.AccommodationReservation reservation in allAccommodationReservations)
            {
                if (reservation.StartDate > DateTime.Now)
                {
                    futureReservations.Add(reservation);
                }
            }

            return futureReservations;
        }
    }
}
