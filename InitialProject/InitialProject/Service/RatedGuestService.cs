using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class RatedGuestService
    {
        private readonly RatedGuestRepository _ratedGuestRepository;
        public RatedGuestService() 
        {
            _ratedGuestRepository = new RatedGuestRepository();
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
