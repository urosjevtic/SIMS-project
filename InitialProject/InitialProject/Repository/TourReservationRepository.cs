using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservation.csv";

        private readonly Serializer<ТоurReservation> _serializer;

        private List<ТоurReservation> _reservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<ТоurReservation>();
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
        public void Save(ТоurReservation reservation)
        {
            reservation.IdReservation = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public List<ТоurReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
