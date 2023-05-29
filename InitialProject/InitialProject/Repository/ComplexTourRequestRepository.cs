using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _complexRequests;
        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexRequests = _serializer.FromCSV(FilePath);
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(ComplexTourRequest request)
        {
            request.IdRequest = NextId();
            _complexRequests = _serializer.FromCSV(FilePath);
            _complexRequests.Add(request);
            _serializer.ToCSV(FilePath, _complexRequests);
        }
        public int NextId()
        {
            _complexRequests = _serializer.FromCSV(FilePath);
            if (_complexRequests.Count < 1)
            {
                return 1;
            }
            return _complexRequests.Max(c => c.IdRequest) + 1;
        }
        public List<ShortTourRequest> NextIdForParts(List<ShortTourRequest> list)
        {
            _complexRequests = _serializer.FromCSV(FilePath);
            int id = _complexRequests.Last().Requests.Last().IdRequest;
            foreach(ShortTourRequest request in list)
            {
                id++;
                request.IdRequest = id;
            }
            return list;
        }
    }
}
