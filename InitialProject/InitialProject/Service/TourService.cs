using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;
using InitialProject.Model;

namespace InitialProject.Service
{
    public class TourService
    {
        private readonly TourRepository _tourRepository;    
        public LocationService _locationService { get; set; }

        public TourService()
        {
            _tourRepository = new TourRepository(); 
            _locationService = new LocationService();   
        }

        public List<Tour> GetTodayTours(User user)
        {
            var tours = LoadGuideTours(user);
            var locations = _locationService.LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            AddTourLocation(tours, locations);
            foreach (Tour tour in tours)
            {
                if (tour.Start.Date == DateTime.Today.Date)
                {
                    todayTours.Add(tour);
                }
            }
            return todayTours;
        }

        public List<Tour> LoadGuideTours(User user)
        {
            List<Tour> allTours = new List<Tour>();
            allTours = _tourRepository.GetAll();
            List<Tour> tours = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Guide.Id == user.Id)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public void AddTourLocation(List<Tour> tours, List<Location> locations)  //veza lokacije i ture
        {
            foreach (var tour in tours)
            {
                foreach (var location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;

                        break;
                    }
                }
            }
        }

        public List<Tour> FindActiveTours(User user)
        {

            var tours = LoadGuideTours(user);
            var locations = _locationService.LoadLocations();
            List<Tour> active = new List<Tour>();
            AddTourLocation(tours, locations);
            foreach (Tour tour in tours)
            {
                if (tour.IsActive)
                {
                    active.Add(tour);
                }
            }
            return active;
        }

    }
}
