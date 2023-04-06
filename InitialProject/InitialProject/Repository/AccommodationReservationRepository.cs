using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservation.csv";
        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _reservations;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }
        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(c => c.Id) + 1;
        }
        public void Save(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void SaveReservation(DateTime startDate, DateTime endDate, User LoggedUser, Accommodation accommodation, int guestNumber)
        {
            AccommodationReservation reservation= new AccommodationReservation();
            reservation.StartDate= startDate;
            reservation.EndDate= endDate;
            reservation.UserId=LoggedUser.Id;
            reservation.Id=accommodation.Id;
            reservation.GuestNumber= guestNumber;
            Save(reservation);
        }

        public void Delete(AccommodationReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _reservations.Find(c => c.Id == reservation.Id);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }


    }
}
