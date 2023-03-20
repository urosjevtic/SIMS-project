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
        private readonly LocationRepository _locationRepository;
        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
            _locationRepository = new LocationRepository();
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
        public List<Tour> FindAllAlternatives(Tour tour) 
        {
            List<Tour> alternative = new List<Tour>();
            //TourRepository _tourRepository = new TourRepository();
           // LocationRepository _locationRepository = new LocationRepository();
            List<Tour> tours = GetAll();
            var locations = _locationRepository.GetAll();

            foreach (Tour t in tours)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;
                        break;
                    }
                }
            }
            foreach (Tour t in tours)
            {
                if(t.Location.City.Equals(tour.Location.City))
                {
                    alternative.Add(t);
                }
            }
            return alternative;
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
    }
}
