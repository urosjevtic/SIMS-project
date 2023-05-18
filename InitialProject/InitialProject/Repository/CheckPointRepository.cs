using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class CheckPointRepository : ICheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkPoints.csv";

        private readonly Serializer<CheckPoint> _serializer;

        private List<CheckPoint> _checkPoints;

        public CheckPointRepository()
        {
            _serializer = new Serializer<CheckPoint>();
            _checkPoints = _serializer.FromCSV(FilePath);
        }

        public List<CheckPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public CheckPoint GetById(int id)
        {
            CheckPoint checkPoint = new CheckPoint();
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            checkPoints = GetAll();
            foreach (CheckPoint point in checkPoints)
            {
                if (point.Id == id)
                {
                    checkPoint = point;
                }
            }
            return checkPoint;  
        }

        public CheckPoint Save(CheckPoint checkPoint)
        {
            checkPoint.Id = NextId();
            _checkPoints = _serializer.FromCSV(FilePath);
            _checkPoints.Add(checkPoint);
            _serializer.ToCSV(FilePath, _checkPoints);
            return checkPoint;
        }

        public int NextId()
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            if (_checkPoints.Count < 1)
            {
                return 1;
            }
            return _checkPoints.Max(c => c.Id) + 1;
        }

        public void Delete(CheckPoint checkPoint)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckPoint founded = _checkPoints.Find(c => c.Id == checkPoint.Id);
            _checkPoints.Remove(founded);
            _serializer.ToCSV(FilePath, _checkPoints);
        }

        public CheckPoint FindTourLastCheckPoint(Tour tour)
        {
            int max = 0;
            List<CheckPoint> checkPoints = tour.CheckPoints;
            foreach(CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint.SerialNumber > max)
                {
                    max = checkPoint.SerialNumber;
                }
            }
            return checkPoints.Find(c => c.SerialNumber == max);
        }
        public CheckPoint ReturnUpdated(CheckPoint checkPoint)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckPoint current = _checkPoints.Find(c => c.Id == checkPoint.Id);
            int index = _checkPoints.IndexOf(current);
            _checkPoints.Remove(current);
            _checkPoints.Insert(index, checkPoint);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _checkPoints);
            return checkPoint;
        }

        

        

        public void SaveAll(List<CheckPoint> entities)
        {
            
           _serializer.ToCSV(FilePath, entities);
            
        }

        public void Update(CheckPoint checkPoint)
        {
                CheckPoint newCheckPoint = _checkPoints.Find(p1 => p1.Id == checkPoint.Id);
                newCheckPoint.Id = checkPoint.Id;
                newCheckPoint.Name = checkPoint.Name;
                newCheckPoint.IsChecked = checkPoint.IsChecked;
                newCheckPoint.SerialNumber = checkPoint.SerialNumber;
                newCheckPoint.CurrentGuests = checkPoint.CurrentGuests;
               

                SaveAll(_checkPoints);
            
        }

    }
}
