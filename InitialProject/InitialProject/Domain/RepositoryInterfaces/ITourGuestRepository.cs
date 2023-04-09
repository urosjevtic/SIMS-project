using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourGuestRepository
    {
        public int NextId();

        public void Save(TourGuest guest);


        public List<TourGuest> GetAll();


        public TourGuest GetById(int id);

        public void Delete(TourGuest entity);


        public void SaveAll(List<TourGuest> entities);


        public void Update(TourGuest entity);


        public TourGuest GetGuest(User user);
       
    }
}
