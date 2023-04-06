using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = new LocationRepository();
        }
        public List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }
    }
}
