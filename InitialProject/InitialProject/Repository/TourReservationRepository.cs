using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class TourReservationRepository : ITourReservationRepository
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
        public void DeleteList(List<TourReservation> list)
        {
            _reservations = _serializer.FromCSV(FilePath);
            foreach(TourReservation reservation in list)
            {
                Delete(reservation);
            }
        }
        public void Delete(TourReservation reservation)
        {
            TourReservation founded = _reservations.Find(c => c.IdReservation == reservation.IdReservation);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }
    }
}
