using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Domain.RepositoryInterfaces.IReservationsRepo
{
    public interface IAccommodationReservationRescheduleRequestRepository
    {
        AccommodationReservationRescheduleRequest Save(AccommodationReservationRescheduleRequest rescheduleRequest);

        List<AccommodationReservationRescheduleRequest> GetAll();

        void Delete(AccommodationReservationRescheduleRequest request);

        void Update(AccommodationReservationRescheduleRequest request);
    }
}
