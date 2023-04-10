using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        public int NextId();

        public void Save(TourReservation reservation);

        public int CountUnreservedSeats(Tour tour);

        public void SaveReservation(Tour tour, int numberOfPeople, User LoggedUser);


        public List<TourReservation> GetAll();

        public List<User> GetReservationGuest(Tour tour);
        
    }
}
