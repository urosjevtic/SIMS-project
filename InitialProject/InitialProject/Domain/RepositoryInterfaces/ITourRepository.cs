using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {

        public int NextId();

        public void Save(Tour tour);

        public List<Tour> GetAll();

        public CheckPoint GetTourFirstCheckPoint(Tour tour);

        public void Delete(Tour tour);

        public void SaveAll(List<Tour> tours);
        
        public void Update(Tour tour);


        public Tour GetById(int id);
      
    }
}
