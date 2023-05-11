using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.RenovationServices
{
    public class RenovationService
    {

        private readonly IRenovationRepository _renovationRepository;
        private readonly AccommodationService _accommodationService;


        public RenovationService()
        {
            _renovationRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
            _accommodationService = new AccommodationService();
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
           List<Renovation> renovationByOwnerId = new List<Renovation>();
           BindAccommodationToRenovation(allRenovations, ownerId);
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
            return new Renovation
            {
                Accommodation = accommodation,
                StartDate = renovationStartDate,
                EndDate = renovationStartDate.AddDays(renovationLength),
                LengthInDays = renovationLength,
                Description = description
            };
        }


        public void Delete(Renovation renovation)
        {
            _renovationRepository.Delete(renovation);
        }


        public List<DateTime> FindAvailableDates(List<AccommodationReservation> reservations, DateTime renovationStartDate, DateTime endDate, int renovationDays)
        {
            if (renovationStartDate >= endDate)
            {
                return new List<DateTime>();
            }

            List<DateTime> datesInRange = CreateDateRange(renovationStartDate, endDate);

            List<DateTime> availableDates = CheckAvailableDates(reservations, datesInRange, renovationDays);

            return availableDates;
        }

        private List<DateTime> CreateDateRange(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();
        }

        private List<DateTime> CheckAvailableDates(List<AccommodationReservation> reservations, List<DateTime> datesInRange, int renovationDays)
        {
            List<DateTime> availableDates = new List<DateTime>();

            foreach (DateTime date in datesInRange)
            {
                bool isAvailable = IsDateAvailable(reservations, date, renovationDays);

                if (isAvailable)
                {
                    availableDates.Add(date);
                }
            }

            return availableDates;
        }

        private bool IsDateAvailable(List<AccommodationReservation> reservations, DateTime date, int renovationDays)
        {
            bool isAvailable = true;

            foreach (AccommodationReservation reservation in reservations)
            {
                if (date >= reservation.StartDate && date <= reservation.EndDate)
                {
                    isAvailable = false;
                    break;
                }
            }


            if (isAvailable)
            {
                DateTime renovationEndDate = date.AddDays(renovationDays - 1);

                foreach (AccommodationReservation reservation in reservations)
                {
                    if (reservation.StartDate <= renovationEndDate && reservation.EndDate >= date)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            return isAvailable;
        }

    }
}
