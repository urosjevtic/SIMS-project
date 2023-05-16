using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Model;

namespace InitialProject.Service
{
    public class TourGuestService
    {
        public ITourGuestRepository _tourGuestRepository;
        public TourGuestService()
        {
            _tourGuestRepository = Injector.Injector.CreateInstance<ITourGuestRepository>();
        }
        public void Update(TourGuest tourGuest)
        {
            _tourGuestRepository.Update(tourGuest);
        }
        public TourGuest GetById(int id)
        {
            return _tourGuestRepository.GetById(id);
        }
    }
}
