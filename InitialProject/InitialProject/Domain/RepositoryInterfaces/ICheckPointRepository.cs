using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ICheckPointRepository
    {
        public CheckPoint FindTourLastCheckPoint(Tour tour);
        public List<CheckPoint> GetAll();

        public CheckPoint GetById(int id);


        public CheckPoint Save(CheckPoint checkPoint);

        public int NextId();


        public void Delete(CheckPoint checkPoint);



        public CheckPoint ReturnUpdated(CheckPoint checkPoint);






        public void SaveAll(List<CheckPoint> entities);


        public void Update(CheckPoint checkPoint);
       
    }
}
