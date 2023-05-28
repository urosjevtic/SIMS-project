
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class ShortTourRequestService
    {
        private readonly IShortTourRequestRepository _shortRequestRepository;
        private readonly LocationService _locationService;  
        public ShortTourRequestService()
        {
            _shortRequestRepository = Injector.Injector.CreateInstance<IShortTourRequestRepository>();
            _locationService = new LocationService();
        }
        public void SaveShortRequest(User LoggedUser, string country, string city, string language, string number, string description, DateTime from, DateTime to)
        {
            ShortTourRequest shortRequest = Create(LoggedUser,country,city,language,number,description,from,to);
            _shortRequestRepository.Save(shortRequest);
        }
        public ShortTourRequest Create(User LoggedUser, string country, string city, string language, string number, string description, DateTime from, DateTime to)
        {
            ShortTourRequest shortRequest = new ShortTourRequest();
            shortRequest.IdRequest = _shortRequestRepository.NextId();
            shortRequest.IdUser = LoggedUser.Id;
            shortRequest.Country = country;
            shortRequest.City = city;
            shortRequest.Language = language;
            shortRequest.NumberOfPeople = int.Parse(number);
            shortRequest.Description = description;
            shortRequest.From = from;
            shortRequest.To = to;
            shortRequest.Status = RequestStatus.Active;

            return shortRequest;
        }
        public List<ShortTourRequest> GetAll()
        {
            return _shortRequestRepository.GetAll();    
        }
        public void Update(ShortTourRequest request)
        {
            _shortRequestRepository.Update(request);
        }
        public void CheckValidation()
        {
            List<ShortTourRequest> shortTourRequests = _shortRequestRepository.GetAll();
            TimeSpan ts = new TimeSpan(48, 0, 0);

            foreach (ShortTourRequest shortRequest in shortTourRequests)
            {
                if (shortRequest.From.Subtract(ts) <= DateTime.Now && shortRequest.Status == RequestStatus.Active)
                {
                    _shortRequestRepository.Invalidate(shortRequest);
                }
            }
        }
        public List<ShortTourRequest> GetAcceptedRequests()
        {
            List<ShortTourRequest> accepted = new();
            foreach (ShortTourRequest shortRequest in _shortRequestRepository.GetAll())
            {
                if(shortRequest.Status == RequestStatus.Accepted)
                {
                    accepted.Add(shortRequest);
                }
            }
            return accepted;
        }



        public int FindSearchMonthRequestNumber(int month, int year, List<ShortTourRequest> requests)
        {
            int number = 0;
            foreach (ShortTourRequest request in requests)
            {
                if (request.From.Year == year && request.From.Month <= month && request.To.Month >= month)
                {
                    ++number;
                }
            }
            return number;
        }

        public int FindMonthRequestNumber(int month, int year)
        {
            int number = 0;
            foreach (ShortTourRequest request in GetAll())
            {
                if (request.From.Year == year && request.From.Month <= month && request.To.Month >= month)
                {
                    ++number;
                }
            }
            return number;
        }


        public Location GetMostWantedLocation()
        {
            Dictionary<Location, int> locationCounts = new Dictionary<Location, int>();
            foreach (ShortTourRequest request in _shortRequestRepository.GetAll())
            {
                foreach(Location location in _locationService.GetLocations())
                {
                    if(location.Country == request.Country && location.City == request.City)
                    {
                        if (locationCounts.ContainsKey(location))
                        {
                            locationCounts[location]++;
                        }
                        else
                        {
                            locationCounts[location] = 1;
                        }
                    }
                   
                }
                
            }
            return locationCounts.OrderByDescending(x => x.Value).First().Key;
          
        }

        public string GetMostWantedLanguage()
        {
            Dictionary<string, int> languageCounts = new Dictionary<string, int>();
            foreach (ShortTourRequest request in _shortRequestRepository.GetAll())
            {
                if (languageCounts.ContainsKey(request.Language))
                {
                    languageCounts[request.Language]++;
                }
                else
                {
                    languageCounts[request.Language] = 1;
                }
            }
            return languageCounts.OrderByDescending(x=> x.Value).First().Key;
        }

    }
}
