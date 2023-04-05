using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RescheduleRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/rescheduleRequests.csv";

        private readonly Serializer<RescheduleRequest> _serializer;

        private List<RescheduleRequest> _rescheduleRequests;

        public RescheduleRequestRepository()
        {
            _serializer = new Serializer<RescheduleRequest>();
            _rescheduleRequests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            if (_rescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _rescheduleRequests.Max(c => c.Id) + 1;
        }
        public RescheduleRequest Save(RescheduleRequest rescheduleRequest)
        {
            rescheduleRequest.Id = NextId();
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            _rescheduleRequests.Add(rescheduleRequest);
            _serializer.ToCSV(FilePath, _rescheduleRequests);

            return rescheduleRequest;
        }

        public List<RescheduleRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Delete(RescheduleRequest request)
        {
            _rescheduleRequests = _serializer.FromCSV(FilePath);
            RescheduleRequest founded = _rescheduleRequests.Find(c => c.Id == request.Id);
            _rescheduleRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _rescheduleRequests);
        }
    }
}
