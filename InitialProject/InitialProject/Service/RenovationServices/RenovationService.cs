using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.RenovationServices
{
    public class RenovationService
    {

        private readonly IRenovationRepository _renovationRepository;
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly RenovationAvailabilityService _renovationAvailabilityService;


        public RenovationService()
        {
            _renovationRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _renovationAvailabilityService = new RenovationAvailabilityService();
        }


        public List<Renovation> GetAll()
        {
            return _renovationRepository.GetAll();
        }

        public List<Renovation> GetByAccommodationId(int accommodationId)
        {
           return _renovationRepository.GetByAccommodationId(accommodationId);
        }

        public List<Renovation> GetByOwnerId(int ownerId)
        {
           List<Renovation> allRenovations = _renovationRepository.GetAll();
           BindAccommodationToRenovation(allRenovations, ownerId);


           return FindRenovationsByOwnerId(allRenovations, ownerId);
        }

        private List<Renovation> FindRenovationsByOwnerId(List<Renovation> allRenovations, int ownerId)
        {
            List<Renovation> renovationByOwnerId = new List<Renovation>();
            foreach (var renovation in allRenovations)
            {
                if (renovation.Accommodation.Owner.Id == ownerId)
                {
                    renovationByOwnerId.Add(renovation);
                }
            }

            return renovationByOwnerId;
        }


        private void BindAccommodationToRenovation(List<Renovation> renovations, int ownerId)
        {
            List<Accommodation> accommodations = _accommodationService.GetAllAccommodationByOwnerId(ownerId);

            foreach (var renovation in renovations)
            {
                Accommodation accommodation = accommodations.FirstOrDefault(a => a.Id == renovation.Accommodation.Id);

                if (accommodation != null)
                {
                    renovation.Accommodation = accommodation;
                }
            }
        }

        public void Save(Renovation renovation)
        {
            _renovationRepository.Save(renovation);
        }

        public void ScheduleRenovation(Accommodation accommodation, DateTime renovationStartDate, int renovationLength, string description)
        {
            Renovation renovation = CreateRenovation(accommodation, renovationStartDate, renovationLength, description);
            _renovationRepository.Save(renovation);
        }

        private Renovation CreateRenovation(Accommodation accommodation, DateTime renovationStartDate, int renovationLength, string description)
        {
            DateTime endDate = renovationStartDate.AddDays(renovationLength);
            return new Renovation(accommodation, renovationStartDate, endDate, renovationLength, description);
        }


        public void Delete(Renovation renovation)
        {
            _renovationRepository.Delete(renovation);
        }


        public List<DateTime> FindAvailableDates(int accommodationId, DateTime renovationStartDate, DateTime endDate, int renovationDays)
        {

            if (renovationStartDate >= endDate)
            {
                return new List<DateTime>();
            }
            List<AccommodationReservation> reservations = _accommodationReservationService.GetReservationsByAccommodationId(accommodationId);


            List<DateTime> datesInRange = CreateDateRange(renovationStartDate, endDate);

            List<DateTime> availableDates = _renovationAvailabilityService.FindDates(reservations, datesInRange, renovationDays);

            return availableDates;
        }

        private List<DateTime> CreateDateRange(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();
        }



    }
}
