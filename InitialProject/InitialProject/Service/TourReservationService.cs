using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class TourReservationService
    {
        public ITourReservationRepository _tourReservationRepository;
        private readonly VoucherService _voucherService;
        public TourReservationService()
        {
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();
            _voucherService = new VoucherService();
        }
        public void SaveReservation(Tour tour, int numberOfPeople, User LoggedUser, bool IsUsingVoucher, double age, DateTime dateTime)
        {
            TourReservation reservation = new TourReservation();
            reservation.IdTour = tour.Id;
            reservation.IdGuest = LoggedUser.Id;
            reservation.NumberOfPeople = numberOfPeople;
            reservation.IsUsingVoucher = IsUsingVoucher;
            reservation.AverageAge = age;
            reservation.DateAndTime = dateTime;
            _tourReservationRepository.Save(reservation);
        }
        public TourReservation CreateReservation(Tour tour, int numberOfPeople, User LoggedUser, bool IsUsingVoucher, double age, DateTime dateTime)
        {
            TourReservation reservation = new TourReservation();
            reservation.IdTour = tour.Id;
            reservation.IdGuest = LoggedUser.Id;
            reservation.NumberOfPeople = numberOfPeople;
            reservation.IsUsingVoucher = IsUsingVoucher;
            reservation.AverageAge = age;
            reservation.DateAndTime = dateTime;
            return reservation;
        }
        public int CountUnreservedSeats(Tour tour, DateTime dateTime)
        {
            int sum = 0;
            List<TourReservation> reservations = new List<TourReservation>();
            reservations = _tourReservationRepository.GetAll();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.IdTour == tour.Id && dateTime == reservation.DateAndTime)
                {
                    sum += reservation.NumberOfPeople;
                }
            }
            return sum;
        }
        public void CheckPresenceForNewVouchers(User user)
        {
            int sum = 0;
            List<TourReservation> countedReservations = new List<TourReservation>();
            List<TourReservation> reservations = _tourReservationRepository.GetAll();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.DateAndTime < DateTime.Now && reservation.DateAndTime > DateTime.Now.AddYears(-1) && user.Id == reservation.IdGuest)
                {
                    sum++;
                    countedReservations.Add(reservation);
                }
                if(sum == 5)
                {
                    _tourReservationRepository.DeleteList(countedReservations);
                    _voucherService.CreateVoucher(user);
                }
            }
        }
    }
}
