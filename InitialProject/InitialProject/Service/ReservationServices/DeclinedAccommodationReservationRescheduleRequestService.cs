using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.ReservationServices
{
    public class DeclinedAccommodationReservationRescheduleRequestService
    {
        private DeclinedAccommodationReservationRescheduleRequestRepository _declined;
        private AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private readonly IDeclinedAccommodationReservationRescheduleRequestRepository _repository;

        public DeclinedAccommodationReservationRescheduleRequestService()
        {
            _repository =
                Injector.Injector.CreateInstance<IDeclinedAccommodationReservationRescheduleRequestRepository>();
        }

        public List<DeclinedAccommodationReservationRescheduleRequest> GetAll()
        {
            List<DeclinedAccommodationReservationRescheduleRequest> rejected = new List<DeclinedAccommodationReservationRescheduleRequest>();
            rejected=_repository.GetAll();
            //BindDeclinedToRescheduled(rejected);
            return rejected;
        }

        public void Save(DeclinedAccommodationReservationRescheduleRequest request)
        {
            _repository.Save(request);
        }

        private void BindDeclinedToRescheduled(List<DeclinedAccommodationReservationRescheduleRequest> declined)
        {
            foreach (var d in declined)
            {
                foreach (var r in _accommodationReservationRescheduleRequestService.GetAll())
                {
                    if (r.Id == d.RescheduleRequest.Id)
                    {
                        d.RescheduleRequest = r;
                        break;
                    }
                }
            }
        }

    }
}
