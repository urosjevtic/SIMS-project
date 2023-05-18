using InitialProject.Domain.Model.Reservations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;

namespace InitialProject.Repository.ReservationRepo
{
    public class DeclinedAccommodationReservationRescheduleRequestRepository : IDeclinedAccommodationReservationRescheduleRequestRepository
    {

        private const string FilePath = "../../../Resources/Data/declinedAccommodationReservationRescheduleRequest.csv";
        private readonly Serializer<DeclinedAccommodationReservationRescheduleRequest> _serializer;

        private List<DeclinedAccommodationReservationRescheduleRequest> _declinedRescheduleRequests;


        public DeclinedAccommodationReservationRescheduleRequestRepository()
        {
            _serializer = new Serializer<DeclinedAccommodationReservationRescheduleRequest>();
            _declinedRescheduleRequests = _serializer.FromCSV(FilePath);
        }

        private int NextId()
        {
            _declinedRescheduleRequests = _serializer.FromCSV(FilePath);
            if (_declinedRescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _declinedRescheduleRequests.Max(c => c.Id) + 1;
        }


        public void Save(DeclinedAccommodationReservationRescheduleRequest request)
        {
            request.Id = NextId();
            _declinedRescheduleRequests = _serializer.FromCSV(FilePath);
            _declinedRescheduleRequests.Add(request);
            _serializer.ToCSV(FilePath, _declinedRescheduleRequests);
        }

        public List<DeclinedAccommodationReservationRescheduleRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

       

    }
}
