using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class CheckPointService
    {
        private readonly CheckPointRepository _checkPointRepository;

        public List<CheckPoint> LoadCheckPoints()
        {
            return _checkPointRepository.GetAll();
        }
    }
}
