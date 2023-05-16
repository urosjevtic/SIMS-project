using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
        }
        public List<Location> GetLocations()
        {
            return _locationRepository.GetAll();
        }

        public int GetLocationId(string country, string city)
        {
            List<Location> allLocations = _locationRepository.GetAll();
            foreach (Location location in allLocations)
            {
                if (location.City == city && location.Country == country)
                {
                    return location.Id;
                }
            }
            throw new Exception("Error has occurred");
        }

        public Dictionary<string, List<string>> GetCountriesAndCities()
        {
            Dictionary<string, List<string>> locations = new Dictionary<string, List<string>>();
            List<Location> allLocations = _locationRepository.GetAll();

            foreach (Location location in allLocations)
            {
                if (!locations.ContainsKey(location.Country))
                {
                    locations.Add(location.Country, new List<string>());
                }

                locations[location.Country].Add(location.City);
            }

            return locations;
        }

        public Location GetById(int id)
        {
           return _locationRepository.GetById(id);
        }
        public void Save(Location location)
        {
            _locationRepository.Save(location);
        }
    }
}
