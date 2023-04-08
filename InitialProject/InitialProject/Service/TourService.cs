using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;
using InitialProject.Domain.Model;
using InitialProject.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class TourService
    {
        private readonly ITourRepository _tourRepository;    
        private readonly ITourGuestRepository _tourGuestsRepository;
        private readonly ITourReservationRepository _tourReservationRepository;

        private readonly VoucherRepository _voucherRepository;
        public LocationService _locationService { get; set; }

        public TourService()
        {
            _tourRepository = Injector.Injector.CreateInstance<ITourRepository>(); 
            _locationService = new LocationService(); 
            _tourGuestsRepository = Injector.Injector.CreateInstance<ITourGuestRepository>(); 
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();   
            _voucherRepository = new VoucherRepository();   
        }
        public List<Tour> GetTodayTours(User user)
        {
            var tours = LoadGuideTours(user);
            var locations = _locationService.GetLocations();
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
        public List<Tour> Search(string state, string city, string language, string duration, string number)
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Location> locations = _locationService.LoadLocations();
            AddTourLocation(tours, locations);

            List<Tour> searchResults = tours.ToList();

            RemoveByLocation(searchResults, state, city, language);
            RemoveByNumbers(searchResults, duration, number);

            return searchResults;
        }
        public List<Tour> LoadTours()
        {
            return _tourRepository.GetAll();
        }
        public List<Tour> RemoveByLocation(List<Tour> searchResults, string state, string city, string language)
        {
            string[] searchValues = { state, city, language };
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.Concatenate().ToLower().Contains(value.ToLower()));
            return searchResults;
        }

        public List<Tour> RemoveByNumbers(List<Tour> searchResults, string duration, string number)
        {
            int searchDuration = duration == "" ? -1 : Convert.ToInt32(duration);
            int searchMaxGuests = number == "" ? -1 : Convert.ToInt32(number);
            if (searchDuration > 0) searchResults.RemoveAll(x => x.Duration != searchDuration);
            if (searchMaxGuests > 0) searchResults.RemoveAll(x => x.MaxGuests < searchMaxGuests);

            return searchResults;
        }
        public List<Tour> LoadGuideTours(User user)
        {
            List<Tour> allTours = new List<Tour>();
            allTours = _tourRepository.GetAll();
            List<Tour> tours = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Guide.Id == user.Id && tour.Start.DayOfYear >= DateTime.Today.DayOfYear)
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
        public List<Tour> FindAllAlternatives(Tour tour)
        {
            List<Tour> alternative = new List<Tour>();
            List<Tour> tours = _tourRepository.GetAll();
            var locations = _locationService.LoadLocations();

            AddTourLocation(tours, locations);

            foreach (Tour t in tours)
            {
                if (t.Location.City.Equals(tour.Location.City))
                {
                    alternative.Add(t);
                }
            }
            return alternative;
        }
        public List<Tour> FindAllActiveTours()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Location> locations = _locationService.LoadLocations();
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
        public List<Tour> FindAllEndedTours()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Location> locations = _locationService.LoadLocations();
            List<Tour> ended = new List<Tour>();
            AddTourLocation(tours, locations);

            foreach(Tour tour in tours)
            {
                TimeSpan ts = new(tour.Duration, 0, 0);
                if(tour.Start.Add(ts) < DateTime.Now && tour.IsActive == false)
                {
                    ended.Add(tour);
                }
            }
            return ended;
        }
        public List<Tour> FindActiveTours(User user)
        {

            var tours = LoadGuideTours(user);
            var locations = _locationService.GetLocations();
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
        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }
        public void SendVauchers(Tour tour)
        {
            foreach (User guste in _tourReservationRepository.GetReservationGuest(tour))
            {
                Guest guest = _tourGuestsRepository.GetGuest(guste);
                MakeVaucher(guest);
            }
        }

        public void MakeVaucher(Guest guest)
        {
            Voucher voucher = new Voucher();
            voucher.IdUser = guest.Id;
            voucher.CreationDate = DateTime.Now;
            voucher.Status = VoucherStatus.Created;
            voucher.Text = "Ovaj vaucer mozete koristiti 2 godine od datuma kreiranja";
            _voucherRepository.Save(voucher);
        }
        /////////////////////////////////////////
        ///

        public int NextId()
        {
            return _tourRepository.NextId();
        }
        public List<Tour> FindAllAlternatives(Tour tour)
        {
            return _tourRepository.FindAllAlternatives(tour);
        }
        
        public void Save(Tour tour)
        {
            _tourRepository.Save(tour);
        }


        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }


        public CheckPoint GetTourFirstCheckPoint(Tour tour)
        {
            return _tourRepository.GetTourFirstCheckPoint(tour);
        }
       

        public void SaveAll(List<Tour> tours)
        {
            _tourRepository.SaveAll(tours);
        }
        public void Update(Tour tour)
        {
            _tourRepository.Update(tour);
        }

        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }

        public string GetMostVisitedEver(string year)
        {
            return "";
        }
    }
}
