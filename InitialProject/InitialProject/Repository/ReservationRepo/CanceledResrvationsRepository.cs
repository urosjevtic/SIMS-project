using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository.ReservationRepo
{
    public class CanceledResrvationsRepository : ICanceledReservationsRepository
    {
        private const string FilePath = "../../../Resources/Data/canceledReservations.csv";
        private readonly Serializer.Serializer<CanceledReservations> _serializer;

        private List<CanceledReservations> _reservations;

        public CanceledResrvationsRepository()
        {
           _serializer = new Serializer<CanceledReservations>();
           _reservations = _serializer.FromCSV(FilePath);
        }
        private int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(c => c.Id) + 1;
        }
        public void Save(CanceledReservations reservation)
        {
            reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        //public List<AccommodationReservation> GetAll()
       // {
         //   return _serializer.FromCSV(FilePath);
        //}
        public void SaveCanceledReservation(Domain.Model.Reservations.AccommodationReservation reservations)
        {
            CanceledReservations reservation = new CanceledReservations();
            reservation.ReservationId = reservation.Id;
            Save(reservation);
        }
    }
}
