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

        public void Save(Guest guest);


        public List<Guest> GetAll();


        public Guest GetById(int id);

        public void Delete(Guest entity);


        public void SaveAll(List<Guest> entities);


        public void Update(Guest entity);


        public Guest GetGuest(User user);
       
    }
}
