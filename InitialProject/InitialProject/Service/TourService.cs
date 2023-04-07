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
        private readonly TourGuestsRepository _tourGuestsRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly VoucherRepository _voucherRepository;
        public LocationService _locationService { get; set; }

        public TourService()
        {
            _tourRepository = new TourRepository(); 
            _locationService = new LocationService(); 
            _tourGuestsRepository = new TourGuestsRepository(); 
            _tourReservationRepository = new TourReservationRepository();   
            _voucherRepository = new VoucherRepository();   
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

    }
}
