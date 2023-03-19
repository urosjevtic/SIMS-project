using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;
        private List<CheckPoint> _checkpoints;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            
            _tours = _serializer.FromCSV(FilePath);

        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(c => c.Id) + 1;
        }
        public void Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }


        public CheckPoint GetTourFirstCheckPoint(Tour tour)
        {
            foreach (var item in tour.CheckPoints)
            {
                if (item.SerialNumber == 1)
                    return item;
            }

            return null;
        }
    }
}
