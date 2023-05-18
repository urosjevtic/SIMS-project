using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location ReturnSaved(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }

        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(c => c.Id) + 1;
        }

        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location founded = _locations.Find(c => c.Id == location.Id);
            _locations.Remove(founded);
            _serializer.ToCSV(FilePath, _locations);
        }
        public Location GetById(int id)
        {
            Location location = _locations.Find(c => c.Id == id);
            return location;
        }
        public Location Update(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(c => c.Id == location.Id);
            int index = _locations.IndexOf(current);
            _locations.Remove(current);
            _locations.Insert(index, location);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }
        private string[] SplitString(string s)
        {
            return s.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
        public int GetLocationId(string location)
        {
            string[] splitedLocation = SplitString(location);
            List<Location> locations = GetAll();
            foreach (Location loc in locations)
            {
                if (loc.Country == splitedLocation[0])
                {
                    if (loc.City == splitedLocation[1])
                        return loc.Id;
                }
            }
            Location newLocation = new Location();
            newLocation.Country = splitedLocation[0];
            newLocation.City = splitedLocation[1];
            return ReturnSaved(newLocation).Id;
        }

        public void Save(Location location)
        {
                location.Id = NextId();
                _locations = _serializer.FromCSV(FilePath);
                _locations.Add(location);
                _serializer.ToCSV(FilePath, _locations);            
        }

        public void SaveAll(List<Location> entities)
        {
            throw new NotImplementedException();
        }
    }
}
