using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Model;

namespace InitialProject.Repository
{
    public class CheckedCheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkedCheckPoint.csv";

        private readonly Serializer<CheckedCheckPoint> _serializer;

        private List<CheckedCheckPoint> _checkPoints;

        public CheckedCheckPointRepository()
        {
            _serializer = new Serializer<CheckedCheckPoint>();
            _checkPoints = _serializer.FromCSV(FilePath);
        }

        public List<CheckedCheckPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public CheckedCheckPoint FindById(int id)
        {
            CheckedCheckPoint checkPoint = new CheckedCheckPoint();
            List<CheckedCheckPoint> checkPoints = new List<CheckedCheckPoint>();
            checkPoints = GetAll();
            foreach (CheckedCheckPoint point in checkPoints)
            {
                if (point.Id == id)
                {
                    checkPoint = point;
                }
            }
            return checkPoint;
        }

        public CheckedCheckPoint Save(CheckedCheckPoint checkPoint)
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

        public void Delete(CheckedCheckPoint checkPoint)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckedCheckPoint founded = _checkPoints.Find(c => c.Id == checkPoint.Id);
            _checkPoints.Remove(founded);
            _serializer.ToCSV(FilePath, _checkPoints);
        }

        public CheckedCheckPoint Update(CheckedCheckPoint checkPoint)
        {
            _checkPoints = _serializer.FromCSV(FilePath);
            CheckedCheckPoint current = _checkPoints.Find(c => c.Id == checkPoint.Id);
            int index = _checkPoints.IndexOf(current);
            _checkPoints.Remove(current);
            _checkPoints.Insert(index, checkPoint);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _checkPoints);
            return checkPoint;
        }

        public CheckedCheckPoint Transform(CheckPoint cp)
        {
            CheckedCheckPoint ccp = new CheckedCheckPoint(cp.Id, cp.Name, cp.SerialNumber, cp.IsChecked);
            return ccp;
        }
    }
}
