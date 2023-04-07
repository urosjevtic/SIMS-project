using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Domain.RepositoryInterfaces.IReservationsRepo
{
    internal interface IAccommodationReservationRepository
    {
        void Save(AccommodationReservation reservation);
        List<AccommodationReservation> GetAll();
        void SaveReservation(DateTime startDate, DateTime endDate, User LoggedUser, Accommodation accommodation, int guestNumber);
        void Delete(AccommodationReservation reservation);
        void Update(AccommodationReservation reservation);
    }
}
