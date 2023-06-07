using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRepository; 
        public ComplexTourRequestService()
        {
            _complexTourRepository = Injector.Injector.CreateInstance<IComplexTourRequestRepository>();
        }
        public List<ComplexTourRequest> GetAllRequests()
        {
            return _complexTourRepository.GetAll();
        }
        public void SaveComplexRequest(User LoggedUser, List<ShortTourRequest> list)
        {
            ComplexTourRequest complexRequest = new ComplexTourRequest();
            complexRequest.IdRequest = _complexTourRepository.NextId();
            complexRequest.IdUser = LoggedUser.Id;
            complexRequest.Status = RequestStatus.Active;
            complexRequest.Requests = list;
            _complexTourRepository.NextIdForParts(list);
            _complexTourRepository.Save(complexRequest);
        }
        /*public ComplexTourRequest CheckValidation(ComplexTourRequest request)
        {
            TimeSpan ts = new TimeSpan(48, 0, 0);

            foreach (ShortTourRequest shortRequest in request.Requests)
            {
                if (shortRequest.From.Subtract(ts) <= DateTime.Now && shortRequest.Status == RequestStatus.Active)
                {
                    _complexTourRepository.Invalidate(shortRequest);
                }
            }
            return request;
        }*/
    }
}
