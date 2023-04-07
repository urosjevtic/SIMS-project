using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;

namespace InitialProject.Repository.ReservationRepo
{
    public class AccommodationReservationRescheduleRequestRepository : IAccommodationReservationRescheduleRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/rescheduleRequests.csv";

        private readonly Serializer<AccommodationReservationRescheduleRequest> _serializer;

        private List<AccommodationReservationRescheduleRequest> _rescheduleRequests;

        public AccommodationReservationRescheduleRequestRepository()
        {
            _serializer = new Serializer<AccommodationReservationRescheduleRequest>();
            _rescheduleRequests = _serializer.FromCSV(FilePath);
        }

        private int NextId()
        {
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            if (_rescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _rescheduleRequests.Max(c => c.Id) + 1;
        }


        public AccommodationReservationRescheduleRequest Save(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            rescheduleRequest.Id = NextId();
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            _rescheduleRequests.Add(rescheduleRequest);
            _serializer.ToCSV(FilePath, _rescheduleRequests);

            return rescheduleRequest;
        }


        public List<AccommodationReservationRescheduleRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Delete(AccommodationReservationRescheduleRequest request)
        {
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            AccommodationReservationRescheduleRequest founded = _rescheduleRequests.Find(c => c.Id == request.Id);
            _rescheduleRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _rescheduleRequests);
        }

        public void Update(AccommodationReservationRescheduleRequest request)
        {
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            foreach (var rescheduleRequest in _rescheduleRequests)
            {
                if (rescheduleRequest.Id == request.Id)
                {
                    _rescheduleRequests.Remove(rescheduleRequest);
                    _rescheduleRequests.Add(request);
                    break;;
                }
            }
            _serializer.ToCSV(FilePath, _rescheduleRequests);
        }

    }
}
