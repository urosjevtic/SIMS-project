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
        public ShortTourRequestService()
        {
            _shortRequestRepository = Injector.Injector.CreateInstance<IShortTourRequestRepository>();
        }
        public void SaveShortRequest(User LoggedUser, string country, string city, string language, string number, string description, DateTime from, DateTime to)
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

            _shortRequestRepository.Save(shortRequest);
        }
    }
}
