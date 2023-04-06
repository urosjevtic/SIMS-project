using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Repository
{
    public interface ICheckPointRepository : IRepository<CheckPoint>
    {
        public CheckPoint FindTourLastCheckPoint(Tour tour);
    }
}
