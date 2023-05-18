using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class TourRepository : ITourRepository 
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
        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour founded = _tours.Find(c => c.Id == tour.Id);
            _tours.Remove(founded);
            _serializer.ToCSV(FilePath, _tours);
        }
        

        public void SaveAll(List<Tour> tours)
        {
            _serializer.ToCSV(FilePath, tours);
        }
        public void Update(Tour tour)
        {
            Tour newTour = _tours.Find(p1 => p1.Id == tour.Id);
            newTour.Id = tour.Id;
            newTour.Name = tour.Name;
            newTour.Guide.Id = tour.Guide.Id;
            newTour.Location = tour.Location;
            newTour.Description = tour.Description;
            newTour.Language = tour.Language;
            newTour.MaxGuests = tour.MaxGuests;
            newTour.StartDates = tour.StartDates;
            newTour.Duration = tour.Duration;
            newTour.CoverImageUrl.Id = tour.CoverImageUrl.Id;
            newTour.IsActive = tour.IsActive;
            newTour.IsRated = tour.IsRated;

            SaveAll(_tours);
            
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(i => i.Id == id);
        }
    }
}
