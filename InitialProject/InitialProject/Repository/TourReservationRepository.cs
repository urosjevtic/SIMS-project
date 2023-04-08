using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Domain.Model;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _reservations;
        public UserRepository _userRepository { get; set; }

        public TourReservationRepository()
        {
            _userRepository = new UserRepository();
            _serializer = new Serializer<TourReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(c => c.IdReservation) + 1;
        }
        public void Save(TourReservation reservation)
        {
            reservation.IdReservation = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }
        public int CountUnreservedSeats(Tour tour)
        {
            int sum = 0;
            List<TourReservation> reservations = new List<TourReservation>();
            TourReservationRepository reservationsRepository = new TourReservationRepository(); 
            reservations = reservationsRepository.GetAll();
            foreach (TourReservation reservation in reservations)
            {
                if(reservation.IdTour == tour.Id)
                {
                    sum += reservation.NumberOfPeople;
                }
            }
            return sum;
        }
        public void SaveReservation(Tour tour, int numberOfPeople, User LoggedUser, bool IsUsingVoucher, double age)
        {
            TourReservation reservation = new TourReservation();
            reservation.IdTour = tour.Id;
            reservation.IdGuest = LoggedUser.Id;
            reservation.NumberOfPeople = numberOfPeople;
            reservation.IsUsingVoucher = IsUsingVoucher;
            reservation.AverageAge = age;
            Save(reservation);
        }


        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<User> GetReservationGuest(Tour tour)
        {
            List<User> guests = new List<User>();
            List<TourReservation> reservations = GetAll();
            foreach(TourReservation tourReservation in reservations)
            {
                if(tourReservation.IdTour == tour.Id)
                {
                    guests.Add(_userRepository.GetById(tourReservation.IdGuest));
                }
            }
            return guests;
        }
    }
}
