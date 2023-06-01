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

        public List<TourReservation> GetAll();

        public List<User> GetReservationGuest(Tour tour);

        public void DeleteList(List<TourReservation> reservationList);

        public void Delete(TourReservation reservation);
        
    }
}
