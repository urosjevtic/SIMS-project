using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;

using InitialProject.Domain.Model;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class CheckPointService
    {
        public ICheckPointRepository _checkPointRepository;
        public TourService _tourService;
        public CheckPointService()
        {
            _checkPointRepository = Injector.Injector.CreateInstance<ICheckPointRepository>();
            _tourService = new TourService();
        }

        public List<CheckPoint> GetAll()
        {
            return _checkPointRepository.GetAll();
        }

        public CheckPoint GetById(int id)
        {
            return _checkPointRepository.GetById(id);
        }

        public CheckPoint Save(CheckPoint checkPoint)
        {
           return _checkPointRepository.Save(checkPoint);
        }

        public int NextId()
        {
            return _checkPointRepository.NextId();
        }

        public void Delete(CheckPoint checkPoint)
        {
           _checkPointRepository.Delete(checkPoint);
        }

        public CheckPoint FindTourLastCheckPoint(Tour tour)
        {

            return _checkPointRepository.FindTourLastCheckPoint(tour);
        }
        public CheckPoint ReturnUpdated(CheckPoint checkPoint)
        {
            return _checkPointRepository.ReturnUpdated(checkPoint);
        }


        public void SaveAll(List<CheckPoint> entities)
        {

            _checkPointRepository.SaveAll(entities);

        }

        public void Update(CheckPoint checkPoint)
        {
           _checkPointRepository.Update(checkPoint);

        }

        public List<CheckPoint> LoadCheckPoints()
        {
            return _checkPointRepository.GetAll();
        }

        public void CheckFirstCheckPoint(List<CheckPoint> checkPoints)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint.SerialNumber == 1)
                {
                    checkPoint.IsChecked = true;
                    _checkPointRepository.Update(checkPoint);
                }
            }
        }
    }
}
