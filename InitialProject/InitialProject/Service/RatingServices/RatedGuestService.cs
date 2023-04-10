using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class RatedGuestService
    {
        private readonly IRatedGuestRepository _ratedGuestRepository;
        public RatedGuestService() 
        {
            _ratedGuestRepository = Injector.Injector.CreateInstance<IRatedGuestRepository>();
        }

        public List<RatedGuest> GetAll()
        {
            return _ratedGuestRepository.GetAll();
        } 

        public RatedGuest Get(int id)
        {
            List<RatedGuest> ratedGuests = GetAll();
            return ratedGuests.FirstOrDefault(r => r.Id == id);
        }

        public void Save(RatedGuest guest)
        {
            _ratedGuestRepository.Save(guest);
        }

    }
}
