using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        Location GetById(int id);
        Location ReturnSaved(Location location);
        int NextId();
        void Delete(Location location);
        Location Update(Location location);
        int GetLocationId(string location);
        void Save(Location location);
        void SaveAll(List<Location> entities);
    }
}
