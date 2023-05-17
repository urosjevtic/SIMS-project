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
    public class ShortTourRequestRepository : IShortTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/shortTourRequests.csv";

        private readonly Serializer<ShortTourRequest> _serializer;

        private List<ShortTourRequest> _shortRequests;
        public ShortTourRequestRepository()
        {
            _serializer = new Serializer<ShortTourRequest>();
            _shortRequests = _serializer.FromCSV(FilePath);
        }

        public List<ShortTourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _shortRequests = _serializer.FromCSV(FilePath);
            if (_shortRequests.Count < 1)
            {
                return 1;
            }
            return _shortRequests.Max(c => c.IdRequest) + 1;
        }

        public void Save(ShortTourRequest shortTourRequest)
        {
            shortTourRequest.IdRequest = NextId();
            _shortRequests = _serializer.FromCSV(FilePath);
            _shortRequests.Add(shortTourRequest);
            _serializer.ToCSV(FilePath, _shortRequests);
        }
        public void Invalidate(ShortTourRequest shortRequest)
        {
            List<ShortTourRequest> allRequests = GetAll();
            ShortTourRequest s = allRequests.Find(r => r.IdRequest == shortRequest.IdRequest);
            allRequests.Remove(s);
            s.Status = RequestStatus.Invalid;
            allRequests.Insert(s.IdRequest - 1, s);
            SaveAll(allRequests);
        }
        public void Update(ShortTourRequest request)
        {

            ShortTourRequest newRequest = _shortRequests.Find(p => p.IdRequest == request.IdRequest);
            newRequest.IdRequest = request.IdRequest;
            newRequest.IdUser = request.IdUser;
            newRequest.Country = request.Country;
            newRequest.City = request.City;
            newRequest.Language = request.Language;
            newRequest.NumberOfPeople = request.NumberOfPeople;
            newRequest.Status = request.Status;
            newRequest.Description = request.Description;
            newRequest.From = request.From;
            newRequest.To = request.To; 
            SaveAll(_shortRequests);
            
        }
        public void SaveAll(List<ShortTourRequest> list)
        {
            _serializer.ToCSV(FilePath, list);
        }
    }
}
