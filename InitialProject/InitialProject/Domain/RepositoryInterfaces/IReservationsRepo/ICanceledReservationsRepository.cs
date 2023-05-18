using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces.IReservationsRepo
{
    internal interface ICanceledReservationsRepository
    {
        void Save(CanceledReservations reservation);
       // List<AccommodationReservation> GetAll();
        void SaveCanceledReservation(Domain.Model.Reservations.AccommodationReservation reservations);
    }
}
