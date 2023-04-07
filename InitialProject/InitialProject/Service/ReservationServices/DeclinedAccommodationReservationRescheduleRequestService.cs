using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.ReservationServices
{
    public class DeclinedAccommodationReservationRescheduleRequestService
    {
        private readonly IDeclinedAccommodationReservationRescheduleRequestRepository _repository;

        public DeclinedAccommodationReservationRescheduleRequestService()
        {
            _repository =
                Injector.Injector.CreateInstance<IDeclinedAccommodationReservationRescheduleRequestRepository>();
        }

        public List<DeclinedAccommodationReservationRescheduleRequest> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(DeclinedAccommodationReservationRescheduleRequest request)
        {
            _repository.Save(request);
        }
    }
}
