using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Domain.RepositoryInterfaces.IReservationsRepo
{
    public interface IDeclinedAccommodationReservationRescheduleRequestRepository
    {
        void Save(DeclinedAccommodationReservationRescheduleRequest request);

        List<DeclinedAccommodationReservationRescheduleRequest> GetAll();


    }
}
